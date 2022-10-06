using OgarnizerAPI.Models;

namespace OgarnizerAPI.Interfaces
{
    public interface IAccountService
    {
        void CreateUser(CreateUserDto dto);
        void DeleteUser(DeleteUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}
