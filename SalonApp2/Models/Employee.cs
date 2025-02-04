using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonApp2.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; } // Primary key

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Specialization { get; set; }

        // Foreign key for Client
        [ForeignKey("Client")]
        public string? PhoneNum { get; set; }
        public Client? Client { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
