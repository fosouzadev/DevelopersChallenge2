using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Enums;
using Domain.Models;

[assembly: InternalsVisibleTo("Tests")]
namespace Domain.Services
{
    internal class OfxService
    {
        public List<BankTransaction> ReadFile(Stream streamOfx)
        {            
            List<BankTransaction> transactions = new List<BankTransaction>();
            BankTransaction transaction = null; 

            using (StreamReader stream = new StreamReader(streamOfx))
            {
                string line = null;

                while ((line = stream.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line == "</STMTTRN>")
                    {
                        if (!transactions.Any(a => a.Type == transaction.Type &&
                                                   a.Value == transaction.Value &&
                                                   a.Date.Year == transaction.Date.Year &&
                                                   a.Date.Month == transaction.Date.Month &&
                                                   a.Date.Day == transaction.Date.Day &&
                                                   a.Description == transaction.Description))
                            transactions.Add(transaction);
                    }

                    if (line == "<STMTTRN>")
                    {
                        transaction = new BankTransaction();
                        continue;
                    }

                    if (line.Contains("<TRNTYPE>"))
                    {
                        if (Enum.TryParse<BankTransactionType>(line.Replace("<TRNTYPE>", ""), true, out BankTransactionType type))
                            transaction.Type = type;
                        else
                            transaction.ImportErrors.Add($"error converting transaction type {type}");
                        
                        continue;
                    }

                    if (line.Contains("<DTPOSTED>"))
                    {
                        line = line.Replace("<DTPOSTED>", "");
                        bool convertYear = int.TryParse(line.Substring(0, 4), out int year);
                        bool convertMonth = int.TryParse(line.Substring(4, 2), out int month);
                        bool convertDay = int.TryParse(line.Substring(6, 2), out int day);

                        if (convertYear && convertMonth && convertDay)
                            transaction.Date = new DateTime(year, month, day);
                        else
                            transaction.ImportErrors.Add($"error converting transaction date {line}");
                        
                        continue;
                    }

                    if (line.Contains("<TRNAMT>"))
                    {
                        if (decimal.TryParse(line.Replace("<TRNAMT>", ""), out decimal value))
                            transaction.Value = value;
                        else
                            transaction.ImportErrors.Add("error converting transaction amount");

                        continue;
                    }

                    if (line.Contains("<MEMO>"))
                    {
                        transaction.Description = line.Replace("<MEMO>", "");
                        continue;
                    }
                }

                stream.Close();
            }

            return transactions;
        }
    }
}