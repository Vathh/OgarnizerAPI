using OgarnizerAPI.Models;
using OgarnizerAPI.Models.CreateDtos;
using OgarnizerAPI.Models.User;

namespace OgarnizerAPI.Interfaces
{
    public interface IAccountService
    {
        void CreateUser(CreateUserDto dto);
        void DeleteUser(DeleteUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}
