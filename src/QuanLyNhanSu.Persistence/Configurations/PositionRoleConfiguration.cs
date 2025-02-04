using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities.Identity;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;
public class PositionRoleConfiguration : IEntityTypeConfiguration<PositionRole>
{
    public void Configure(EntityTypeBuilder<PositionRole> builder)
    {
        builder.ToTable(TableNames.PositionRole);
        builder.Ignore(p => p.Id);

        builder.HasKey(p => new { p.PositionId, p.RoleId });

        builder.HasOne(p => p.AppRole)
            .WithMany(p => p.PositionRoles)
            .HasForeignKey(p => p.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Position)
            .WithMany(p => p.PositionRoles)
            .HasForeignKey(p => p.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
