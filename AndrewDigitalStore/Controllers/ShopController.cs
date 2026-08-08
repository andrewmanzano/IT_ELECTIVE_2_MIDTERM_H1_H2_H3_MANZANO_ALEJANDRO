using AndrewDigitalStore.Data;
using AndrewDigitalStore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PixelAndPixelStore.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductRepository.GetAll();
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(AddToCartDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid request parameters.";
                return RedirectToAction(nameof(Index));
            }

            var product = ProductRepository.GetById(dto.ProductId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }

            var currentCart = CartRepository.GetCart();
            var existingCartQty = currentCart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId)?.Quantity ?? 0;
            var requestedTotalQty = existingCartQty + dto.Quantity;

            if (requestedTotalQty > product.StockQuantity)
            {
                TempData["ErrorMessage"] = $"Cannot add items. Requested total ({requestedTotalQty}) exceeds available stock ({product.StockQuantity}).";
                return RedirectToAction(nameof(Index));
            }

            CartRepository.AddOrUpdateItem(product, dto.Quantity);
            TempData["SuccessMessage"] = $"Added {dto.Quantity} x '{product.Name}' to cart.";
            return RedirectToAction(nameof(Index));
        }
    }
}