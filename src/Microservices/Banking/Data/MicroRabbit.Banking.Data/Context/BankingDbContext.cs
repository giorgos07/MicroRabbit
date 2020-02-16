using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
#if DEBUG
            Database.EnsureCreated();
#endif
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().Property(x => x.Balance).HasColumnType("decimal(18,2)");
        }
    }
}
