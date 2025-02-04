using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.ToTable(TableNames.Salary);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Salarys)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.Salarys)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
