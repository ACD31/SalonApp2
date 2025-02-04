using Microsoft.AspNetCore.Mvc;
using SalonApp2.Models;
using SalonApp2.DBOperations;
using Microsoft.EntityFrameworkCore; // Replace with your DbContext namespace

    public class ClientsController : Controller
    {
        private readonly SalonDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

    public ClientsController(SalonDbContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        _context = dbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: Clients/Create
    [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client newClient, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Product");
                    var extension = Path.GetExtension(file.FileName);

                    try
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                    Console.WriteLine($"Uploads directory: {uploads}");
                    Console.WriteLine($"Full path: {Path.Combine(uploads, filename + extension)}");
                    Console.WriteLine($"File length: {file.Length}");

                }
                catch (Exception ex)
                    {
                        Console.WriteLine($"Error copying file: {ex.Message}");
                        // Handle or log exceptions as needed
                    }


                newClient.Image = @"Images\Product\" + filename + extension;
            }
            _context.Clients.Add(newClient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newClient);
        }

    // GET: Clients/Index
    public IActionResult Index()
    {
        var clients = _context.Clients
            .Include(c => c.Appointments) 
            .Include(c => c.Employees)    
            .ToList();

        return View(clients);
    }

    [HttpGet]
        public IActionResult Edit(int PhoneNum)
        {
            if (PhoneNum == 0)
            {
                return NotFound();
            }
            var employeeFound = _context.Employees.Find(PhoneNum);
            if (employeeFound == null)
            {
                return NotFound();
            }
            return View(employeeFound);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Update(client);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

    // GET: Display confirmation page for Delete
    [HttpGet]
    public IActionResult Delete(string id) // Use string type for PhoneNum
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }

        return View(client); // Pass the client to the view
    }

    // POST: Perform the delete operation
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string id)
    {
        var client = _context.Clients.Find(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(client);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}


