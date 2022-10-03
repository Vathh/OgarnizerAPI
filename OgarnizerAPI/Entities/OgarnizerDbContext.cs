using Microsoft.EntityFrameworkCore;

namespace OgarnizerAPI.Entities
{
    public class OgarnizerDbContext : DbContext
    {
        private readonly string _connectionString = "Server=localhost;Database=OgarnizerDb;Trusted_Connection=True;";

        public DbSet<User>? Users { get; set; }

        public DbSet<Job>? Jobs { get; set; }

        public DbSet<ClosedJob>? ClosedJobs { get; set; }

        public DbSet<Service>? Services { get; set; }

        public DbSet<ClosedService>? ClosedServices { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public DbSet<ClosedOrder>? ClosedOrders { get; set; }

        public DbSet<Role>? Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
