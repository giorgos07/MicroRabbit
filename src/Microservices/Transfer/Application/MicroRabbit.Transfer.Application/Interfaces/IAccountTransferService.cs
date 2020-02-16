using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.Interfaces
{
    public interface IAccountTransferService
    {
        Task CreateAccountTransferLog(AccountTransferLog accountTransferLog);
        IEnumerable<AccountTransferLog> GetAccountTransferLogs();
    }
}
