using Microsoft.AspNetCore.Mvc;

namespace App.AdminUI.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
