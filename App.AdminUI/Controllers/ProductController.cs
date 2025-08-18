using Microsoft.AspNetCore.Mvc;

namespace App.AdminUI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
