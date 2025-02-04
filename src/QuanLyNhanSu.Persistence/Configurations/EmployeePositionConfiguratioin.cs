using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class EmployeePositionConfiguratioin : IEntityTypeConfiguration<EmployeePosition>
{
    public void Configure(EntityTypeBuilder<EmployeePosition> builder)
    {
        builder.ToTable(TableNames.EmployeePosition);

        builder.Ignore(x => x.Id);

        builder.HasKey(ep => new { ep.EmployeeId, ep.PositionId });

        builder.Property(ed => ed.Status)
            .IsRequired();

        builder.Property(ed => ed.CreatedDate)
            .IsRequired();

        builder.HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeePositions)
            .HasForeignKey(ep => ep.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ep => ep.Position)
            .WithMany(p => p.EmployeePositions)
            .HasForeignKey(ep => ep.PositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
