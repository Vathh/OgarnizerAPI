using System.ComponentModel.DataAnnotations;

namespace OgarnizerAPI.Models
{
    public class UpdateClosedJobDto
    {
        [Required]
        [MaxLength(100)]
        public string? UpdateInfo { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
