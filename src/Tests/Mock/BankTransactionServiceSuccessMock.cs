using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;

namespace Tests.Mock
{
    public class BankTransactionServiceSuccessMock : IBankTransactionService
    {
        public IEnumerable<BankTransaction> GetAll()
        {
            return new List<BankTransaction> {
                new BankTransaction()
            };
        }

        public IEnumerable<BankTransaction> ImportOfxFile(Stream stream)
        {
            return Enumerable.Empty<BankTransaction>();
        }
    }
}