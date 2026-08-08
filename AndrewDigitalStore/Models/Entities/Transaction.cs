using AndrewDigitalStore.Models.Entites;

namespace AndrewDigitalStore.Models.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItem> PurchasedItems { get; set; } = new List<CartItem>();
    }
}