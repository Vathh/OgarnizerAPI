using System.ComponentModel.DataAnnotations;

namespace OgarnizerAPI.Models.CreateDtos
{
    public class CreateUserDto
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; } = 1;

    }
}
