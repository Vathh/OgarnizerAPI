using System.ComponentModel.DataAnnotations;

namespace OgarnizerAPI.Models
{
    public class CreateOrderDto
    {
        [Required]
        public int UserId { get; set; }

        public DateTime CreatedDate = DateTime.Now;

        [Required]
        [Range(1,3)]
        public int Priority { get; set; }
              
        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public string? Client { get; set; }

        [Required]
        public string? Object { get; set; }


        public string? AdditionalInfo { get; set; }
    }
}
