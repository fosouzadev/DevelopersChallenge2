using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services
{
    public class BankTransactionService : IBankTransactionService
    {
        private readonly OfxService _ofxService = new OfxService();
        private readonly IBankTransactionRepository _transactionRepository;

        public BankTransactionService(IBankTransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public virtual IEnumerable<BankTransaction> GetAll()
        {
            return _transactionRepository.GetAll();
        }

        public virtual IEnumerable<BankTransaction> ImportOfxFile(Stream stream)
        {
            List<BankTransaction> transactions = _ofxService.ReadFile(stream);

            if (!transactions.Any() || transactions.Any(a => a.ImportErrors.Any()))
                return transactions.Where(w => w.ImportErrors.Any());
            
            _transactionRepository.Save(transactions);
            return Enumerable.Empty<BankTransaction>();
        }
    }
}