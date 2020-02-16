using System;
using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.EventHandlers
{
    public class AccountTransferCreatedEventHandler : IEventHandler<AccountTransferCreatedEvent>
    {
        private readonly IAccountTransferRepository _accountTransferRepository;

        public AccountTransferCreatedEventHandler(IAccountTransferRepository accountTransferRepository) {
            _accountTransferRepository = accountTransferRepository ?? throw new ArgumentNullException(nameof(accountTransferRepository));
        }

        public async Task Handle(AccountTransferCreatedEvent @event) {
            await _accountTransferRepository.CreateAccountTransferLog(new AccountTransferLog {
                Timestamp = DateTimeOffset.UtcNow,
                FromAccount = @event.FromAccount,
                ToAccount = @event.ToAccount,
                TransferAmount = @event.TransferAmount
            });
        }
    }
}
