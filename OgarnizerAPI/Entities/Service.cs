namespace OgarnizerAPI.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Priority { get; set; }
        public string? Description { get; set; }
        public string? Object { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? UpdateInfo { get; set; }
    }
}
