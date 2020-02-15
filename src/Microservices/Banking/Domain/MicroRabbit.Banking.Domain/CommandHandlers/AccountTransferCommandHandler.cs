using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Events;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Domain.CommandHandlers
{
    public class AccountTransferCommandHandler : IRequestHandler<AccountTransferCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public AccountTransferCommandHandler(IEventBus eventBus) {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task<bool> Handle(AccountTransferCommand request, CancellationToken cancellationToken) {
            // Publish event to RabbitMQ.
            _eventBus.Publish(new AccountTransferCreatedEvent(request.FromAccount, request.ToAccount, request.TransferAmount));
            return Task.FromResult(true);
        }
    }
}
