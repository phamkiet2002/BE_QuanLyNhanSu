using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class LeaveRegistrationConfiguration : IEntityTypeConfiguration<LeaveRegistration>
{
    public void Configure(EntityTypeBuilder<LeaveRegistration> builder)
    {
        builder.ToTable(TableNames.LeaveRegistration);

        builder.HasKey(lr => lr.Id);

        builder.Property(lr => lr.TypeOfLeave)
            .IsRequired(); 

        builder.Property(lr => lr.StartDate)
            .IsRequired(false); 

        builder.Property(lr => lr.EndDate)
            .IsRequired(false); 

        builder.Property(lr => lr.HalfDayOff)
            .IsRequired(false);  

        builder.Property(lr => lr.DayOff)
            .IsRequired(false);  

        builder.Property(lr => lr.LeaveReason)
            .HasMaxLength(500);  
        builder.Property(lr => lr.PendingApproval)
            .IsRequired(false);

        builder.Property(lr => lr.ApprovedTime)
            .IsRequired(false);

        builder.Property(lr => lr.ApprovedId)
            .IsRequired(false);

        builder.HasOne(lr => lr.Approval)
            .WithMany(e => e.LeaveRegistrationApproves)
            .HasForeignKey(lr => lr.ApprovedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(lr => lr.Employee)
            .WithMany(e => e.LeaveRegistrations)
            .HasForeignKey(lr => lr.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);  

        builder.HasOne(lr => lr.LeaveDate)
            .WithMany(ld => ld.LeaveRegistrations) 
            .HasForeignKey(lr => lr.LeaveDateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
