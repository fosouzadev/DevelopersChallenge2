using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;

namespace Tests.Mock
{
    public class BankTransactionServiceErrorMock : IBankTransactionService
    {
        public IEnumerable<BankTransaction> GetAll()
        {
            return Enumerable.Empty<BankTransaction>();
        }

        public IEnumerable<BankTransaction> ImportOfxFile(Stream stream)
        {
            return new List<BankTransaction> {
                new BankTransaction()
            };
        }
    }
}
