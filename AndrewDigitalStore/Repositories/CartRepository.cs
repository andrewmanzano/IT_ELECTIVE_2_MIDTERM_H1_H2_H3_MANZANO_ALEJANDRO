using AndrewDigitalStore.Models.Entites;
using AndrewDigitalStore.Models.Entities;

namespace AndrewDigitalStore.Data
{
    public static class CartRepository
    {
        private static readonly ShoppingCart _cart = new ShoppingCart();

        public static ShoppingCart GetCart() => _cart;

        public static void AddOrUpdateItem(Product product, int quantity)
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
            }
        }

        public static void SetItemQuantity(int productId, int quantity)
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity = quantity;
            }
        }

        public static void RemoveItem(int productId)
        {
            _cart.Items.RemoveAll(i => i.ProductId == productId);
        }

        public static void Clear()
        {
            _cart.Items.Clear();
        }
    }
}