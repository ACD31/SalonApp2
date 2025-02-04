using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalonApp2.Models;
using System.Collections.Generic;

namespace SalonApp2.ViewModels
{
    public class AppointmentViewModel
    {
        public Appointment? Appointment { get; set; }

        [BindNever] // Exclude EmployeeList from model validation
        public IEnumerable<SelectListItem>? EmployeeList { get; set; }
    }
}
