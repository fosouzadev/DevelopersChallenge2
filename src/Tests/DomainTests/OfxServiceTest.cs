using System.IO;
using Domain.Services;
using Xunit;

namespace src.Tests.DomainTests
{
    public class OfxServiceTest
    {
        [Fact]
        public void ReadFile_ReadOfxFile_ReturnTransactions()
        {
            Stream stream = new StreamReader("extrato1.ofx").BaseStream;
            
            var transactions = new OfxService().ReadFile(stream);

            Assert.NotEmpty(transactions);
        }
    }
}