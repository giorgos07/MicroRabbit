namespace MicroRabbit.Banking.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}
