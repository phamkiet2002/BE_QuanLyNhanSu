using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(TableNames.Employee);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Phone)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(e => e.IdentityCard)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Gender)
            .IsRequired();

        builder.Property(e => e.DateOfBirth)
            .IsRequired();

        builder.Property(e => e.Address)
            .HasMaxLength(300);

        builder.Property(e => e.JoinDate)
            .IsRequired();

        builder.Property(e => e.BankName)
            .HasMaxLength(100);

        builder.Property(e => e.BankAccountNumber)
            .HasMaxLength(50);

        builder.HasOne(e => e.WorkPlace)
            .WithMany(wp => wp.Employees)
            .HasForeignKey(e => e.WorkPlaceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.EmployeeDepartments)
            .WithOne(ed => ed.Employee)
            .HasForeignKey(ed => ed.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.EmployeePositions)
            .WithOne(ep => ep.Employee)
            .HasForeignKey(ep => ep.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Salarys)
            .WithOne(es => es.Employee)
            .HasForeignKey(es => es.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.EmployeeLevels)
            .WithOne(el => el.Employee)
            .HasForeignKey(el => el.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Contracts)
            .WithOne(c => c.Employee)
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.LeaveRegistrations)
            .WithOne(lr => lr.Employee)
            .HasForeignKey(lr => lr.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.LeaveRegistrationApproves)
            .WithOne(lr => lr.Approval)
            .HasForeignKey(lr => lr.ApprovedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Attendances)
            .WithOne(a => a.Employee)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.AttendanceApproves)
            .WithOne(a => a.Approval)
            .HasForeignKey(a => a.ApprovedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.PayRolls)
            .WithOne(pr => pr.Employee)
            .HasForeignKey(pr => pr.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(e => e.User)
        //    .WithOne()
        //    .HasForeignKey<Employee>(e => e.Id);
    }
}
