using AndrewDigitalStore.Models.Entities;

namespace AndrewDigitalStore.Data
{
    public static class ProductRepository
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "DualSense Wireless Controller (PS5)", Price = 69.99m, StockQuantity = 15 },
            new Product { Id = 2, Name = "Xbox Wireless Controller (Robot White)", Price = 59.99m, StockQuantity = 12 },
            new Product { Id = 3, Name = "Logitech G Pro X Headset", Price = 129.99m, StockQuantity = 8 },
            new Product { Id = 4, Name = "Razer DeathAdder V3 Mouse", Price = 69.99m, StockQuantity = 10 },
            new Product { Id = 5, Name = "SteelSeries Apex Pro TKL Keyboard", Price = 189.99m, StockQuantity = 5 },
            new Product { Id = 6, Name = "Elgato Stream Deck MK.2", Price = 149.99m, StockQuantity = 6 },
            new Product { Id = 7, Name = "Nintendo Switch Pro Controller", Price = 69.99m, StockQuantity = 9 },
            new Product { Id = 8, Name = "Corsair Vengeance 32GB DDR5 RAM", Price = 114.99m, StockQuantity = 7 }
        };

        public static List<Product> GetAll() => _products;

        public static Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public static bool DeductStock(int productId, int quantity)
        {
            var product = GetById(productId);
            if (product != null && product.StockQuantity >= quantity)
            {
                product.StockQuantity -= quantity;
                return true;
            }
            return false;
        }
    }
}