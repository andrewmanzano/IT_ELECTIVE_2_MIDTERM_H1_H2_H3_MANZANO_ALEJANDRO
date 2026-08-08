using AndrewDigitalStore.Models.Entites;

namespace AndrewDigitalStore.Models.Entities
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal GrandTotal => Items.Sum(item => item.TotalPrice);
    }
}