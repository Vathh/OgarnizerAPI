﻿using FluentValidation;
using OgarnizerAPI.Entities;
using OgarnizerAPI.Models.CreateDtos;

namespace OgarnizerAPI.Models.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator(OgarnizerDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Login)
                .NotEmpty()
                .MinimumLength(8)
                .Custom((value, context) =>
                {
                    var loginInUse = dbContext.Users.Any(u => u.Login == value);
                    if (loginInUse)
                    {
                        context.AddFailure("Login", "That login is taken");
                    }
                });
        }
    }
}
