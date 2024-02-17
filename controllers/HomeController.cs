using Microsoft.AspNetCore.Mvc;

namespace WebApp.controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
