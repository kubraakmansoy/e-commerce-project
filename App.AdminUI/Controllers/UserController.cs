using Microsoft.AspNetCore.Mvc;

namespace App.AdminUI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
