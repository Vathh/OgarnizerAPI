using OgarnizerAPI.Models;
using OgarnizerAPI.Models.CreateDtos;

namespace OgarnizerAPI.Interfaces
{
    public interface IAccountService
    {
        void CreateUser(CreateUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}
