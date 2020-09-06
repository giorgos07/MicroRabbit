using System;
using System.Collections.Generic;
using System.Net.Mime;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    /// <summary>
    /// A controller responsible for banking operations.
    /// </summary>
    [Route("api/banking")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Creates a new instance of <see cref="BankingController"/>.
        /// </summary>
        /// <param name="accountService">A service that performs operations in bank accounts.</param>
        public BankingController(IAccountService accountService) {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        /// <summary>
        /// Retrieves a list of all the available accounts.
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet("accounts")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<Account>))]
        public ActionResult<IEnumerable<Account>> GetAccounts() {
            return Ok(_accountService.GetAccounts());
        }

        /// <summary>
        /// Transfers funds between two specified accounts.
        /// </summary>
        /// <param name="request">A model that contains information about the transfer of funds.</param>
        /// <response code="200">OK</response>
        [HttpPost("accounts/transfer-funds")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(void))]
        public IActionResult TransferFunds([FromBody] AccountTransferRequest request) {
            _accountService.TransferFunds(request);
            return Ok();
        }
    }
}
