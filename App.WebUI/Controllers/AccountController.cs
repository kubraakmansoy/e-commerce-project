using App.WebUI.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

using App.WebUI.Models.Auth;


namespace App.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _api;

        // Program.cs'de tanımladığımız named HttpClient ("api")
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _api = httpClientFactory.CreateClient("api");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var payload = new
            {
                fullName = model.FullName,
                email = model.Email,
                password = model.Password
            };


            var response = await _api.PostAsJsonAsync("api/user/register", payload);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }

            var apiError = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Kayıt başarısız: {apiError}");
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // 1) API'ye login isteği
                // API tarafında beklenen payload eğer { email, password } ise:
                var response = await _api.PostAsJsonAsync("api/auth/login", new
                {
                    email = model.Email,
                    password = model.Password
                });

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "E-posta veya şifre hatalı.");
                    return View(model);
                }

                // 2) API başarılı döndüyse kullanıcı bilgilerini al (örnek)
                //    API’nden nasıl dönüyorsa ona göre shape’i ayarla:
                //    { userId, fullName, email, role, token? } gibi
                var loginResult = await response.Content.ReadFromJsonAsync<LoginSuccessDto>();
                if (loginResult == null)
                {
                    ModelState.AddModelError("", "Giriş sonucu okunamadı.");
                    return View(model);
                }

                // 3) Claims oluştur
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, loginResult.UserId.ToString()),
            new Claim(ClaimTypes.Name, loginResult.FullName ?? model.Email),
            new Claim(ClaimTypes.Email, loginResult.Email ?? model.Email),
        };

                if (!string.IsNullOrWhiteSpace(loginResult.Role))
                    claims.Add(new Claim(ClaimTypes.Role, loginResult.Role));

                // 4) ClaimsIdentity + Cookie sign-in
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProps = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProps);

                // 5) Yönlendirme
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError("", "Giriş sırasında bir hata oluştu.");
                return View(model);
            }
        }
        public IActionResult Login() => View();
    }
}
