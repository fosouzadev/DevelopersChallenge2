using System.Collections.Generic;
using System.IO;
using Api.Controllers;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tests.Mock;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Tests.ApiTests
{
    public class BankTransactionControllerTest
    {
        [Fact]
        public void GetAll_GetTransactions_ReturnOk()
        {
            BankTransactionController controller = new BankTransactionController(new BankTransactionServiceSuccessMock());

            OkObjectResult result = controller.GetAll() as OkObjectResult;

            var transactions = Assert.IsType<List<BankTransaction>>(result.Value);
            Assert.NotEmpty(transactions);
        }

        [Fact]
        public void GetAll_GetTransactions_ReturnNoContent()
        {
            BankTransactionController controller = new BankTransactionController(new BankTransactionServiceErrorMock());

            IActionResult result = controller.GetAll();

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Upload_SendOfxFile_ReturnOk()
        {
            Stream stream = new StreamReader("extrato1.ofx").BaseStream;
            FormFile file = new FormFile(stream, 0, 0, "teste1", "extrato1.ofx");

            BankTransactionController controller = new BankTransactionController(new BankTransactionServiceSuccessMock());

            IActionResult result = controller.Upload(file);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Upload_SendOfxFile_ReturnBadRequest()
        {
            Stream stream = new StreamReader("extrato1.ofx").BaseStream;
            FormFile file = new FormFile(stream, 0, 0, "teste2", "extrato1.ofx");
            
            BankTransactionController controller = new BankTransactionController(new BankTransactionServiceErrorMock());

            BadRequestObjectResult result = controller.Upload(file) as BadRequestObjectResult;

            var transactions = Assert.IsType<List<BankTransaction>>(result.Value);
            Assert.NotEmpty(transactions);
        }
    }
}