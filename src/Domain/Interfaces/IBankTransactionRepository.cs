using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IBankTransactionRepository
    {
        void Save(IEnumerable<BankTransaction> transactions);
        IEnumerable<BankTransaction> GetAll();
    }
}