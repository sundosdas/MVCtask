using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        public AdminController (ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.numberOfCustomers = _context.Customers.Count();
            ViewBag.Sale=_context.Products.Sum(x => x.Sale);
            ViewBag.MinPrice = _context.Products.Min(x => x.Price); 
            ViewBag.MaxPrice = _context.Products.Max(x => x.Price); 
            ViewBag.Customer = _context.Customers.ToList();
            return View();
        }
    }
}
