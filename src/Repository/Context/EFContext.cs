using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Repository.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options): base(options) 
        {
            this.Database.EnsureCreated();
        }

        public DbSet<BankTransaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankTransaction>().HasKey(k => k.Id);
            modelBuilder.Entity<BankTransaction>().Property(p => p.Description).HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}