using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonApp2.Models
{
    public class Client
    {
        [Key]
        [StringLength(15)]
        public required string PhoneNum { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Email { get; set; }

        public string? Image { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
