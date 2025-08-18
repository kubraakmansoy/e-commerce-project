using Microsoft.AspNetCore.Mvc;
using App.Core.Interfaces;

namespace App.AdminUI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private readonly HttpClient _api;

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> List()
        {
            var users = await _userService.GetAllAsync();
            return View(users); // Views/User/List.cshtml
        }

        

        // Örn: Satıcı onay
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var resp = await _api.PostAsync($"api/user/{id}/approve", null);
            TempData["Message"] = resp.IsSuccessStatusCode ? "Kullanıcı onaylandı." : "İşlem başarısız.";
            return RedirectToAction(nameof(List));
        }
    }

    public class UserListItemVm
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        public bool Enabled { get; set; }
    }



}

