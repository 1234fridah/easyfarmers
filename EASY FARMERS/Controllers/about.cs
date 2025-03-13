using Microsoft.AspNetCore.Mvc;

namespace easy_farmers.Controllers
{
    public class About : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
