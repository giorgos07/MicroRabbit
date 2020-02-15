using System;
using System.Collections.Generic;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _eventBus;

        public AccountService(IAccountRepository accountRepository, IEventBus eventBus) {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public IEnumerable<Account> GetAccounts() {
            return _accountRepository.GetAccounts();
        }

        public void TransferFunds(AccountTransferRequest accountTransfer) {
            var accountTransferCommand = new AccountTransferCommand {
                FromAccount = accountTransfer.FromAccount,
                ToAccount = accountTransfer.ToAccount,
                TransferAmount = accountTransfer.TransferAmount
            };
            _eventBus.SendCommand(accountTransferCommand);
        }
    }
}
