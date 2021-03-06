using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snowdrop.Data.Entities.Core;

namespace Snowdrop.DAL.Configurations.core
{
    internal abstract class BaseEntityTypeConfiguration<TEntity>: BaseTypeConfiguration<TEntity> where TEntity: BaseEntity
    {
        protected override Expression<Func<TEntity, object>> Key => entity => entity.Id;
        
        protected override void ConfigureEntity(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(entity => entity.CreatedAt)
                .HasColumnType("Date")
                .HasDefaultValueSql("NOW()")
                .IsRequired();
            
            builder
                .Property(entity => entity.UpdatedAt)
                .HasColumnType("Date")
                .HasDefaultValueSql("NOW()")
                .IsRequired();
        }
    }
}