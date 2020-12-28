using System.Collections.Generic;
using System.IO;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IBankTransactionService
    {
        IEnumerable<BankTransaction> ImportOfxFile(Stream stream);
        IEnumerable<BankTransaction> GetAll();
    }
}