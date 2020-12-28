using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Models;

namespace Tests.Mock
{
    public class BankTransactionRepositorySuccessMock : IBankTransactionRepository
    {
        public IEnumerable<BankTransaction> GetAll()
        {
            return new List<BankTransaction> {
                new BankTransaction()
            };
        }

        public void Save(IEnumerable<BankTransaction> transactions)
        {
            return;
        }
    }
}