using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using App.WebUI.Models; 
using System.Threading.Tasks;
using System.Collections.Generic;
using App.WebUI.Models;


namespace App.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> List()
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductViewModel>>("https://localhost:5001/api/Product");
            return View(products);
        }
    }
}
