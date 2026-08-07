using Microsoft.AspNetCore.Mvc;

namespace PixelAndPixelStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Shop");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}