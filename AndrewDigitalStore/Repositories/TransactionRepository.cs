using AndrewDigitalStore.Models.Entities;

namespace AndrewDigitalStore.Data
{
    public static class TransactionRepository
    {
        private static readonly List<Transaction> _transactions = new();

        public static List<Transaction> GetAll() => _transactions;

        public static Transaction? GetById(Guid id) => _transactions.FirstOrDefault(t => t.TransactionId == id);

        public static void Add(Transaction transaction) => _transactions.Add(transaction);
    }
}