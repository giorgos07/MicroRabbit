using System;

namespace MicroRabbit.Transfer.Domain.Models
{
    public class AccountTransferLog
    {
        public int Id { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
