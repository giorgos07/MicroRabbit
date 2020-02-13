using System;
using System.Collections.Generic;
using System.Net.Mime;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    [Route("api/banking")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService) {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpGet("accounts")]
        public ActionResult<IEnumerable<Account>> GetAccounts() {
            return Ok(_accountService.GetAccounts());
        }
    }
}
