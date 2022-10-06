using OgarnizerAPI.Entities;

namespace OgarnizerAPI.Models
{
    public class ClosedJobDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Priority { get; set; }

        public string? Description { get; set; }

        public string? Place { get; set; }

        public string? Object { get; set; }

        public string? AdditionalInfo { get; set; }

        public string? UpdateInfo { get; set; }
        public DateTime UpdateDate { get; set; }

        public bool IsDone { get; set; }

        public DateTime ClosedDate { get; set; }

        public int CloseUserId { get; set; }
    }
}
