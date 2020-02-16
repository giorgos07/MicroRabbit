using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.Services
{
    public class AccountTransferService : IAccountTransferService
    {
        private readonly IAccountTransferRepository _accountTransferRepository;
        private readonly IEventBus _eventBus;

        public AccountTransferService(IAccountTransferRepository accountTransferRepository, IEventBus eventBus) {
            _accountTransferRepository = accountTransferRepository ?? throw new ArgumentNullException(nameof(accountTransferRepository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task CreateAccountTransferLog(AccountTransferLog accountTransferLog) {
            return _accountTransferRepository.CreateAccountTransferLog(accountTransferLog);
        }

        public IEnumerable<AccountTransferLog> GetAccountTransferLogs() {
            return _accountTransferRepository.GetAccountTransferLogs();
        }
    }
}
