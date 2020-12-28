using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using Repository.Context;

namespace Repository
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly EFContext _efContext;

        public BankTransactionRepository(EFContext efContext)
        {
            _efContext = efContext;
        }

        public IEnumerable<BankTransaction> GetAll()
        {
            return _efContext.Transactions;
        }

        public void Save(IEnumerable<BankTransaction> transactions)
        {
            if (!transactions.Any())
                return;
            
            bool commit = false;

            foreach (var trans in transactions)
            {
                if (_efContext.Transactions.Any(a => a.Type == trans.Type &&
                                                     a.Value == trans.Value &&
                                                     a.Date.Year == trans.Date.Year &&
                                                     a.Date.Month == trans.Date.Month &&
                                                     a.Date.Day == trans.Date.Day &&
                                                     a.Description == trans.Description))
                    continue;
                
                _efContext.Transactions.Add(trans);
                commit = true;
            }

            if (commit)
                _efContext.SaveChanges();
        }
    }
}