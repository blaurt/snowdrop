using Microsoft.EntityFrameworkCore;
using Snowdrop.Data;
using Snowdrop.Data.Entities;

namespace Snowdrop.DAL.Context
{
    internal sealed class SnowdropContext : DbContext
    {
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public SnowdropContext(DbContextOptions options) : base(options)
        {
        }
        
        
    }
}