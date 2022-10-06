using System.ComponentModel.DataAnnotations;

namespace OgarnizerAPI.Models
{
    public class UpdateJobDto
    {
        [Required]
        [MaxLength(100)]
        public string? UpdateInfo { get; set; }
    }
}
