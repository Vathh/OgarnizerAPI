using System.ComponentModel.DataAnnotations;

namespace OgarnizerAPI.Models
{
    public class UpdateJobDto
    {
        [Required]
        [MaxLength(100)]
        public string? UpdateInfo { get; set; }

        public DateTime UpdateDate = DateTime.Now;

        public bool ToInvoice { get; set; }
    }
}
