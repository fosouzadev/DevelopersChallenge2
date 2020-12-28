using System.IO;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Tests.Mock;
using Xunit;

namespace Tests.DomainTests
{
    public class BankTransactionServiceTest
    {
        [Fact]
        public void GetAll_GetTransactions_ReturnTransactions()
        {
            BankTransactionService service = new BankTransactionService(new BankTransactionRepositorySuccessMock());

            var transactions = service.GetAll();

            Assert.NotEmpty(transactions);
        }

        [Fact]
        public void GetAll_GetTransactions_ReturnEmptyList()
        {
            BankTransactionService service = new BankTransactionService(new BankTransactionRepositorySuccessMock());

            var transactions = service.GetAll();

            Assert.NotEmpty(transactions);
        }

        [Fact]
        public void ImportOfxFile_ImportTransactionsFromOfxFile_SaveTransactions()
        {
            BankTransactionService service = new BankTransactionService(new BankTransactionRepositorySuccessMock());
            Stream stream = new StreamReader("extrato1.ofx").BaseStream;

            try
            {
                service.ImportOfxFile(stream);
                Assert.True(true);
            }
            catch (System.Exception)
            {
                Assert.True(false);   
            }
        }

        [Fact]
        public void ImportOfxFile_ImportTransactionsFromOfxFile_ThrowException()
        {
            BankTransactionService service = new BankTransactionService(new BankTransactionRepositoryErrorMock());
            Stream stream = new StreamReader("extrato1.ofx").BaseStream;

            Assert.Throws<DbUpdateException>(() => service.ImportOfxFile(stream));
        }
    }
}