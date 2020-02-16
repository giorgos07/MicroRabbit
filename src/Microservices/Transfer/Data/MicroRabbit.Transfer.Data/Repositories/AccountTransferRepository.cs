using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Transfer.Data.Repositories
{
    public class AccountTransferRepository : IAccountTransferRepository
    {
        private readonly TransferDbContext _dbContext;
        
        public AccountTransferRepository(TransferDbContext dbContext) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CreateAccountTransferLog(AccountTransferLog accountTransferLog) {
            _dbContext.AccountTransferLogs.Add(accountTransferLog);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<AccountTransferLog> GetAccountTransferLogs() {
            return _dbContext.AccountTransferLogs.AsNoTracking();
        }
    }
}
