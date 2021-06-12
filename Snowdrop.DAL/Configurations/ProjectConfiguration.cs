using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snowdrop.DAL.Configurations.core;
using Snowdrop.Data.Entities;

namespace Snowdrop.DAL.Configurations
{
    internal sealed class ProjectConfiguration: BaseEntityTypeConfiguration<Project>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Project> builder)
        {
            base.ConfigureEntity(builder);
            builder
                .Property(project => project.Title)
                .IsRequired();
            
            builder
                .Property(project => project.Description)
                .IsRequired();

            builder
                .HasMany(project => project.Wallets)
                .WithOne(wallet => wallet.Project)
                .HasForeignKey(wallet => wallet.ProjectId);
        }
    }
}