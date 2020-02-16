using System;
using System.Collections.Generic;
using System.Net.Mime;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Transfer.Api.Controllers
{
    /// <summary>
    /// A controller responsible for banking operations.
    /// </summary>
    [Route("api/transfers")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class AccountTransferController : ControllerBase
    {
        private readonly IAccountTransferService _accountTransferService;

        /// <summary>
        /// Creates a new instance of <see cref="AccountTransferController"/>.
        /// </summary>
        /// <param name="accountTransferService">A service that performs logging in operations between bank accounts.</param>
        public AccountTransferController(IAccountTransferService accountTransferService) {
            _accountTransferService = accountTransferService ?? throw new ArgumentNullException(nameof(accountTransferService));
        }

        /// <summary>
        /// Retrieves a list of all the available account tranfers logs.
        /// </summary>
        /// <response code="200">OK</response>
        [HttpGet("logs")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<AccountTransferLog>))]
        public ActionResult<IEnumerable<AccountTransferLog>> GetAccounts() {
            return Ok(_accountTransferService.GetAccountTransferLogs());
        }
    }
}
