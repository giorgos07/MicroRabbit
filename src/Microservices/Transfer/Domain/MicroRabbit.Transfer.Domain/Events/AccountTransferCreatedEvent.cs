using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Transfer.Domain.Events
{
    public class AccountTransferCreatedEvent : Event
    {
        public AccountTransferCreatedEvent(int fromAccount, int toAccount, decimal transferAmount) {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransferAmount = transferAmount;
        }

        public int FromAccount { get; private set; }
        public int ToAccount { get; private set; }
        public decimal TransferAmount { get; private set; }
    }
}
