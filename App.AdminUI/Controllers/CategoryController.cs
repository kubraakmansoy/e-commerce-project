using Microsoft.AspNetCore.Mvc;

namespace App.AdminUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
