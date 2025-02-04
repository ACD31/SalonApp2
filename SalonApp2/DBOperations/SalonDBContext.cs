using Microsoft.EntityFrameworkCore;
using SalonApp2.Models;

namespace SalonApp2.DBOperations
{
    public class SalonDbContext : DbContext
    {
        public SalonDbContext(DbContextOptions<SalonDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Client -> Employee relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Client)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.PhoneNum)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade

            // Configure Client -> Appointment relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.PhoneNum)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade

            // Configure Employee -> Appointment relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade is fine here
        }
    }
}
