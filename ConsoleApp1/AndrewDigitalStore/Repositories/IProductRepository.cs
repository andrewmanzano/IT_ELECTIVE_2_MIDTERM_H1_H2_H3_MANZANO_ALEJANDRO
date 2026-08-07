using AndrewDigitalStore.Models.Entities;

namespace AndrewDigitalStore.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product? GetById(int id);
        void DeductStock(int productId, int quantity);
    }
}