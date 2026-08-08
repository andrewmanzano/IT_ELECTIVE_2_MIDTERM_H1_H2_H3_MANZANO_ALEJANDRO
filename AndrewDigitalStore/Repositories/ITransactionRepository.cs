using AndrewDigitalStore.Models.Entities;

namespace AndrewDigitalStore.Data
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();
        Transaction? GetById(Guid id);
        void Add(Transaction transaction);
    }
}