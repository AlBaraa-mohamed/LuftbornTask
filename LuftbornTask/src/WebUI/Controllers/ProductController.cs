using LuftbornTask.src.Application.Services;
using LuftbornTask.src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuftbornTask.src.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var products = await _service.GetAllByUserAsync(userId);

            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Strategies = GetStrategies();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, string strategyName)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Strategies = GetStrategies();
                return View(product);
            }
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            product.UserId = userId;

            await _service.AddAsync(product, strategyName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Strategies = GetStrategies();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, string strategyName)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Strategies = GetStrategies();
                return View(product);
            }
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            product.UserId = userId;

            await _service.UpdateAsync(product, strategyName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private List<string> GetStrategies()
        {
            return new List<string> { "Regular", "Discount", "Premium" };
        }
    }
}
