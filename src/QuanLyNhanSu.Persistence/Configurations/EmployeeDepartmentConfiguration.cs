using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class EmployeeDepartmentConfiguration : IEntityTypeConfiguration<EmployeeDepartment>
{
    public void Configure(EntityTypeBuilder<EmployeeDepartment> builder)
    {
        builder.ToTable(TableNames.EmployeeDepartment);

        builder.Ignore(x=> x.Id);

        builder.HasKey(ed => new { ed.EmployeeId, ed.DepartmentId });

        builder.Property(ed => ed.Status)
            .IsRequired(); 

        builder.Property(ed => ed.CreatedDate)
            .IsRequired();

        builder.HasOne(ed => ed.Employee)
            .WithMany(e => e.EmployeeDepartments)
            .HasForeignKey(ed => ed.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade); // Xóa Employee sẽ xóa EmployeeDepartment

        builder.HasOne(ed => ed.Department)
            .WithMany(d => d.EmployeeDepartments)
            .HasForeignKey(ed => ed.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);  // Không cho phép xóa Department khi có EmployeeDepartment liên kết
    }
}
