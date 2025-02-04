using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class EmployeeLevelConfiguration : IEntityTypeConfiguration<EmployeeLevel>
{
    public void Configure(EntityTypeBuilder<EmployeeLevel> builder)
    {
        builder.ToTable(TableNames.EmployeeLevel);

        builder.Ignore(x => x.Id);

        builder.HasKey(ed => new { ed.EmployeeId, ed.LevelId });

        builder.Property(ed => ed.Status)
            .IsRequired();

        builder.Property(ed => ed.CreatedDate)
            .IsRequired();

        builder.HasOne(ed => ed.Employee)
            .WithMany(e => e.EmployeeLevels)
            .HasForeignKey(ed => ed.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(ed => ed.Level)
            .WithMany(d => d.EmployeeLevels)
            .HasForeignKey(ed => ed.LevelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
