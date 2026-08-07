using Microsoft.AspNetCore.Mvc;
using AndrewDigitalStore.Data;
using AndrewDigitalStore.Models.Entities;

namespace AndrewDigitalStore.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            var transactions = TransactionRepository.GetAll();
            return View(transactions);
        }

        public IActionResult Details(Guid id)
        {
            var transaction = TransactionRepository.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
    }
}