using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        

        public AuthController (ModelContext context,
        IWebHostEnvironment _hostEnvironment)

        {

            this._hostEnvironment = _hostEnvironment;

            _context = context;

        }
        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult>
        Register([Bind("Id,Fname,Lname,Imagepath,ImageFile")] Customer customer, string username, string password)
        {

            if (ModelState.IsValid)

            {

                string wwwRootPath = _hostEnvironment.WebRootPath;

                string fileName = Guid.NewGuid().ToString() + "_" + customer.ImageFile.FileName;
                string extension = Path.GetExtension(customer.ImageFile.FileName);

                customer.ImagePath = fileName;

                _context.Add(customer);

                await _context.SaveChangesAsync();

                UserLogin login = new Models.UserLogin();

                login.RoleId = 1;

                login.Passwordd = password;

                login.UserName = username;

                login.CustomerId = customer.Id;

                _context.Add(login);

                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "AuthController");

            }

            return View(customer);
        }

    }
}
