﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Exceptions;
using OgarnizerAPI.Interfaces;
using OgarnizerAPI.Models;
using OgarnizerAPI.Models.CreateDtos;

namespace OgarnizerAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly OgarnizerDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(OgarnizerDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;   
        }
        public void CreateUser(CreateUserDto dto)
        {
            var newUser = new User() {
                Name = dto.Name,
                Login = dto.Login,
                RoleId = dto.RoleId,
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == dto.Login);

            if(user is null)
            {
                throw new BadRequestException("Invalid login or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid login or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name.ToString())
                // new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
            };

            /*if (!string.IsNullOrEmpty(user.Nationality){
                claims.Add(
                    new Claim("Nationality", user.Nationality));
            }*/

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
