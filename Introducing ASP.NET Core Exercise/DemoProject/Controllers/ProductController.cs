using DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DemoProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Bread",
                Price = 1.80,
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Ham",
                Price = 17.80,
            },
            new ProductViewModel()
            {
                Id = 3,
                Name = "Cheese",
                Price = 10.80,
            },
        };

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult GetById(int id)
        {
            var model = products.FirstOrDefault(p => p.Id == id);

            if (model == null)
            {
                TempData["Error"] = "No such product!";

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            return Json(products, options);
        }

        [ActionName("My-Products")]
        public IActionResult AllAsPlainText()
        {
            return Content(GetAllProductsAsString());
        }

        public IActionResult DownloadFile()
        {
            string content = GetAllProductsAsString();
            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(content), "text/plain");
        }

        public IActionResult Filtered(string? keyword)
        {
            if (keyword == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = products
                .Where(p => p.Name.ToLower().Contains(keyword.ToLower()));

            return View(nameof(Index), model);
        }

        private string GetAllProductsAsString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine($"Product {product.Id}. {product.Name} - {product.Price} lv.");
            }

            return sb.ToString();
        }
    }
}
