namespace MicroRabbit.Banking.Application.Models
{
    public class AccountTransferRequest
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
