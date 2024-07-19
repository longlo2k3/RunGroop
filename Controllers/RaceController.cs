using Microsoft.AspNetCore.Mvc;

namespace Object.Controllers
{
    public class RaceController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Welcome = "Hello World";
            return View();
        }
    }
}
