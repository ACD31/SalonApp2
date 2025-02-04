using Microsoft.AspNetCore.Mvc;
using SalonApp2.Models; // Ensure this matches your namespace
using SalonApp2.DBOperations; // Replace with your DbContext namespace
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;



public class EmployeesController : Controller
{
    private readonly SalonDbContext _context;

    public EmployeesController(SalonDbContext context)
    {
        _context = context;
    }

    // GET: Employees
    public IActionResult Index()
    {
        var employees = _context.Employees.ToList();
        return View(employees);
    }

    // GET: Employees/Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.EmployeeList = new SelectList(_context.Employees.ToList(), "EmployeeId", "Name");
        return View();
    }

    // POST: Employees/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee newEmployee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(newEmployee);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var employeeFound = _context.Employees.Find(id);
        if (employeeFound == null)
        {
            return NotFound();
        }
        return View(employeeFound);
    }

    [HttpPost]
    public IActionResult Edit(Employee newEmployee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Update(newEmployee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(newEmployee);
    }

    // GET: Display confirmation page for Delete
    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var employeeFound = _context.Employees.Find(id);
        if (employeeFound == null)
        {
            return NotFound();
        }

        return View(employeeFound); // Pass the employee object to the view
    }

    // POST: Perform the delete operation
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var employeeFound = _context.Employees.Find(id);
        if (employeeFound == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employeeFound); // Remove the employee from the database
        _context.SaveChanges();
        return RedirectToAction(nameof(Index)); // Redirect to the Index page
    }

}


