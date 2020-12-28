using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using System.Net;
using System.Collections.Generic;
using Domain.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionService _transactionService;

        public BankTransactionController(IBankTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Upload the OFX file to import transactions
        /// </summary>
        [HttpPost("[action]/ofx")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<BankTransaction>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Upload([FromForm] IFormFile ofxFile)
        {
            var transactionErrors = _transactionService.ImportOfxFile(ofxFile.OpenReadStream());

            if (transactionErrors.Any())
                return BadRequest(transactionErrors);
            
            return Ok();
        }

        /// <summary>
        /// Return all imported transactions
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<BankTransaction>))]
        public IActionResult GetAll()
        {
            var transactions = _transactionService.GetAll();

            if (transactions.Any())
                return Ok(transactions);
            
            return NoContent();
        }
    }
}