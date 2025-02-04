using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonApp2.Models;
using SalonApp2.ViewModels;
using SalonApp2.DBOperations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalonApp2.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly SalonDbContext _DbContext;

        public AppointmentsController(SalonDbContext context)
        {
            _DbContext = context;
        }

        public IActionResult Index()
        {
            var appointments = _DbContext.Appointments
                .Include(a => a.Employee)
                .Include(a => a.Client)   
                .ToList();

            return View(appointments);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new AppointmentViewModel
            {
                Appointment = new Appointment(),
                EmployeeList = _DbContext.Employees.Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = e.Name
                }).ToList()
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppointmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate the dropdown list in case of validation errors
                viewModel.EmployeeList = _DbContext.Employees.Select(e => new SelectListItem
                {
                    Value = e.EmployeeId.ToString(),
                    Text = e.Name
                }).ToList();
                return View(viewModel);
            }

            // Save the appointment to the database
            _DbContext.Appointments.Add(viewModel.Appointment);
            _DbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
