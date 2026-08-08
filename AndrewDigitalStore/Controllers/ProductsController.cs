using Microsoft.AspNetCore.Mvc;
using AndrewDigitalStore.Models.DTOs;
using AndrewDigitalStore.Repositories;

namespace AndrewDigitalStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICartRepository _cartRepo;

        public ProductsController(IProductRepository productRepo, ICartRepository cartRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetAll();
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(AddToCartDTO dto)
        {
            var product = _productRepo.GetById(dto.ProductId);
            if (product == null) return NotFound();

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid quantity specified.";
                return RedirectToAction(nameof(Index));
            }

            var cart = _cartRepo.GetCart();
            var existingCartItem = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
            int currentCartQty = existingCartItem?.Quantity ?? 0;

            if (dto.Quantity + currentCartQty > product.StockQuantity)
            {
                TempData["ErrorMessage"] = $"Cannot add item. Requested quantity exceeds stock ({product.StockQuantity - currentCartQty} available).";
                return RedirectToAction(nameof(Index));
            }

            _cartRepo.AddOrUpdateItem(product, dto.Quantity);
            TempData["SuccessMessage"] = $"Added {product.Name} to cart!";
            return RedirectToAction(nameof(Index));
        }
    }
}