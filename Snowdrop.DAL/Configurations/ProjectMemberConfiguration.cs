using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snowdrop.DAL.Configurations.core;
using Snowdrop.Data.Entities;

namespace Snowdrop.DAL.Configurations
{
    internal sealed class ProjectMemberConfiguration : BaseTypeConfiguration<ProjectMember>
    {
        protected override Expression<Func<ProjectMember, object>> Key =>
            member => new {member.UserId, member.ProjectId};

        protected override void ConfigureEntity(EntityTypeBuilder<ProjectMember> builder)
        {
            builder
                .HasOne(member => member.Project)
                .WithMany(project => project.Team)
                .HasForeignKey(member => member.ProjectId)
                .IsRequired();

            builder
                .HasOne(member => member.User)
                .WithMany(user => user.Projects)
                .HasForeignKey(member => member.UserId)
                .IsRequired();

            builder
                .Property(member => member.Role)
                .IsRequired();
        }
    }
}