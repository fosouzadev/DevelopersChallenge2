using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests.Mock
{
    public class BankTransactionRepositoryErrorMock : IBankTransactionRepository
    {
        public IEnumerable<BankTransaction> GetAll()
        {
            return Enumerable.Empty<BankTransaction>();
        }

        public void Save(IEnumerable<BankTransaction> transactions)
        {
            throw new DbUpdateException();
        }
    }
}