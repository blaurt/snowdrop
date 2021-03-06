using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snowdrop.DAL.Configurations.core;
using Snowdrop.Data.Entities;

namespace Snowdrop.DAL.Configurations
{
    internal sealed class BalanceConfiguration : BaseEntityTypeConfiguration<Balance>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Balance> builder)
        {
            base.ConfigureEntity(builder);
            builder
                .Property(balance => balance.Amount)
                .HasColumnType("money")
                .IsRequired();


            builder
                .Property(balance => balance.CurrencyType)
                .IsRequired();
        }
    }
}