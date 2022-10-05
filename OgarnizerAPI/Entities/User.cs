using System.ComponentModel.DataAnnotations.Schema;

namespace OgarnizerAPI.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Login { get; set; }   
        public string? PasswordHash { get; set; }
        public int? RoleId { get; set; }
        public virtual Role? Role { get; set; }

        public virtual List<Job>? CreatedJobs { get; set; }

        [ForeignKey("FK_ClosedJobs_Users_CloseUserId")]
        public virtual List<ClosedJob>? ClosedJobs { get; set; }
       
        public virtual List<Service>? CreatedServices { get; set; }
       
        public virtual List<ClosedService>? ClosedServices { get; set; }
        
        public virtual List<Order>? CreatedOrders { get; set; }
        
        public virtual List<ClosedOrder>? ClosedOrders { get; set; } 

    }
}
