using DatabaseContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext
{
    public class Context : Microsoft.EntityFrameworkCore.DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<BinanceUser> Users { get; set; }

        public virtual DbSet<Coin> Coins { get; set; }

        public virtual DbSet<Portfolio> Portfolios { get; set; }

        public virtual DbSet<PortfolioCoin> PortfolioCoins{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BinanceUser>()
                .HasIndex(u => new { u.Id, u.Password })
                .IsUnique()
                .HasDatabaseName("IX_BinanceUser_Username_Password");

            modelBuilder.Entity<BinanceUser>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_BinanceUser_Email");
            modelBuilder.Entity<PortfolioCoin>().HasKey(sc => new { sc.PortfolioId, sc.CoinId });

            modelBuilder.Entity<Coin>()
                .HasIndex(c => c.Name)
                .IsUnique()
                .HasDatabaseName("IX_Coin_Id");
        }
    }
}
