using MicroRabbit.Transfer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
#if DEBUG
            Database.EnsureCreated();
#endif
        }

        public DbSet<AccountTransferLog> AccountTransferLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccountTransferLog>().Property(x => x.TransferAmount).HasColumnType("decimal(18,2)");
        }
    }
}
