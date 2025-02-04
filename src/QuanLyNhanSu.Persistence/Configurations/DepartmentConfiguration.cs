using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(TableNames.Department);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasOne(x => x.WorkPlace)
           .WithMany(w => w.Departments)
           .HasForeignKey(x => x.WorkPlaceId)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.WorkShedule)
            .WithMany(ws => ws.Departments)
            .HasForeignKey(x => x.WorkSheduleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.EmployeeDepartments)
            .WithOne(ed => ed.Department)
            .HasForeignKey(ed => ed.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
