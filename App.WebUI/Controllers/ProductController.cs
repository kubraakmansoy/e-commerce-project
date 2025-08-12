using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;               // <-- Select için
using App.Core.DTOs;            // <-- ProductDto okumak için
using App.WebUI.Models;         // <-- ProductViewModel’e map için

namespace App.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _api;

        // Program.cs'de eklediğimiz named HttpClient ("api") ile çalışıyoruz
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _api = httpClientFactory.CreateClient("api");
        }

        // GET: /Product/List
        public async Task<IActionResult> List()
        {
            // BaseAddress Program.cs'de; burada sadece relative path veriyoruz
            var dtos = await _api.GetFromJsonAsync<List<ProductDto>>("api/product");

            // Güvenli map (dto null gelirse boş liste)
            var models = dtos?.Select(d => new ProductViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                Details = d.Details,
                StockAmount = d.StockAmount,
                Enabled = d.Enabled,
            }).ToList() ?? new List<ProductViewModel>();

            return View(models);
        }
    }
}
