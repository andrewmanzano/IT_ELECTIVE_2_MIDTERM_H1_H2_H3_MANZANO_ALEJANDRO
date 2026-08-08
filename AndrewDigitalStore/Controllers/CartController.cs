using AndrewDigitalStore.Data;
using AndrewDigitalStore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PixelAndPixelStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = CartRepository.GetCart();
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(UpdateCartDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid quantity specified.";
                return RedirectToAction(nameof(Index));
            }

            var product = ProductRepository.GetById(dto.ProductId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction(nameof(Index));
            }

            if (dto.Quantity > product.StockQuantity)
            {
                TempData["ErrorMessage"] = $"Cannot set quantity to {dto.Quantity}. Only {product.StockQuantity} available in stock.";
                return RedirectToAction(nameof(Index));
            }

            CartRepository.SetItemQuantity(dto.ProductId, dto.Quantity);
            TempData["SuccessMessage"] = $"Updated quantity for '{product.Name}'.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            CartRepository.RemoveItem(productId);
            TempData["SuccessMessage"] = "Item removed from shopping cart.";
            return RedirectToAction(nameof(Index));
        }
    }
}