using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable(TableNames.Attendance);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.CheckIn)
            .IsRequired(false);

        builder.Property(a => a.CheckOut)
            .IsRequired(false);

        builder.Property(a => a.IsLate)
            .IsRequired(false);

        builder.Property(a => a.IsEarlyLeave)
            .IsRequired(false);

        builder.Property(a => a.OvertimeOutsideWorkHours)
            .IsRequired(false);

        builder.Property(a => a.IsAbsent)
            .IsRequired(false);

        builder.Property(a => a.LeaveRequest)
            .IsRequired(false);

        builder.Property(a => a.LateTime)
            .IsRequired(false);

        builder.Property(a => a.EarlyLeaveTime)
            .IsRequired(false);

        builder.Property(a => a.StartTime)
            .IsRequired(false);

        builder.Property(a => a.EndTime)
            .IsRequired(false);

        builder.Property(a => a.PendingApproval)
            .IsRequired(false);

        builder.Property(a => a.ApprovedTime)
            .IsRequired(false);

        builder.Property(a => a.ApprovedId)
            .IsRequired(false);

        builder.HasOne(a => a.Approval)
            .WithMany(a => a.AttendanceApproves)
            .HasForeignKey(a => a.ApprovedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Employee)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
