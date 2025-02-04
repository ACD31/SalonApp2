using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonApp2.Models
{
    public class Appointment
    {
        [Key]
        public int AppID { get; set; }

        [Required(ErrorMessage = "Date and Time is required")]
        public DateTime DateAndTime { get; set; }

        [StringLength(50)]
        public string? Type { get; set; }

        // Foreign key for Employee
        [Required(ErrorMessage = "Employee ID is required")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }


        public Employee? Employee { get; set; }

        // Foreign key for Client
        [Required(ErrorMessage = "Client Phone Number is required")]
        [ForeignKey("Client")]
        public string? PhoneNum { get; set; }

        public Client? Client { get; set; }
    }
}
