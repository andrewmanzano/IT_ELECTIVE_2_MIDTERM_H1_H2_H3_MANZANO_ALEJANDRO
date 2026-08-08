using AndrewDigitalStore.Data;
using AndrewDigitalStore.Models.DTOs;
using AndrewDigitalStore.Models.Entites;
using AndrewDigitalStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AndrewDigitalStore.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            var cart = CartRepository.GetCart();
            ViewBag.Cart = cart;
            return View(new CheckoutFormDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Process(CheckoutFormDTO dto)
        {
            var cart = CartRepository.GetCart();

            if (!cart.Items.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot process checkout: Shopping cart is empty.");
            }

            foreach (var item in cart.Items)
            {
                var prod = ProductRepository.GetById(item.ProductId);
                if (prod == null || item.Quantity > prod.StockQuantity)
                {
                    ModelState.AddModelError(string.Empty, $"Stock issue with '{item.ProductName}'. Requested: {item.Quantity}, Available: {prod?.StockQuantity ?? 0}.");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Cart = cart;
                return View("Index", dto);
            }

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Date = DateTime.Now,
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                TotalAmount = cart.GrandTotal,
                PurchasedItems = cart.Items.Select(i => new CartItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };

            foreach (var item in cart.Items)
            {
                ProductRepository.DeductStock(item.ProductId, item.Quantity);
            }

            TransactionRepository.Add(transaction);
            CartRepository.Clear();

            return RedirectToAction("Details", "History", new { id = transaction.TransactionId });
        }
    }
}