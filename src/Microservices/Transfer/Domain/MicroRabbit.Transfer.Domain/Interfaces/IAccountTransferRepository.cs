using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.Interfaces
{
    public interface IAccountTransferRepository
    {
        Task CreateAccountTransferLog(AccountTransferLog accountTransferLog);
        IEnumerable<AccountTransferLog> GetAccountTransferLogs();
    }
}
