using AndrewDigitalStore.Models.Entities;

namespace AndrewDigitalStore.Repositories
{
    public interface ICartRepository
    {
        ShoppingCart GetCart();
        void AddOrUpdateItem(Product product, int quantity);
        void UpdateItemQuantity(int productId, int quantity);
        void RemoveItem(int productId);
        void ClearCart();
    }
}