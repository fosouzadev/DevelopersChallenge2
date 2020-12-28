using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models
{
    public class BankTransaction
    {
        public BankTransaction()
        {
            ImportErrors = new List<string>();
        }

        public int Id { get; set; }
        public BankTransactionType Type { get; set; }
        [NotMapped]
        public string TypeDescription { get => Type.ToString(); }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public List<string> ImportErrors { get; private set;}

    }
}