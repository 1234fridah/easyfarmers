using Microsoft.AspNetCore.Mvc;

namespace easy_farmers.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
