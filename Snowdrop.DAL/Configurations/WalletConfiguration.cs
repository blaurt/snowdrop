using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snowdrop.DAL.Configurations.core;
using Snowdrop.Data.Entities;

namespace Snowdrop.DAL.Configurations
{
    internal sealed class WalletConfiguration: BaseEntityTypeConfiguration<Wallet>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Wallet> builder)
        {
            base.ConfigureEntity(builder);
            builder
                .Property(wallet => wallet.Amount)
                .HasColumnType("money")
                .IsRequired();
            
            builder
                .Property(wallet => wallet.CurrencyType)
                .IsRequired();
        }
    }
}