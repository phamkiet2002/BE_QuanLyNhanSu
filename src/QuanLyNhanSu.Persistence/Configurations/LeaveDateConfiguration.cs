using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class LeaveDateConfiguration : IEntityTypeConfiguration<LeaveDate>
{
    public void Configure(EntityTypeBuilder<LeaveDate> builder)
    {
        builder.ToTable(TableNames.LeaveDate);

        builder.HasKey(ld => ld.Id);

        builder.Property(ld => ld.Name)
            .IsRequired()  
            .HasMaxLength(200); 

        builder.Property(ld => ld.TotalAnnualLeaveDate)
            .IsRequired(); 

        builder.Property(ld => ld.MaximumDaysOffPerMonth)
            .IsRequired(); 

        builder.Property(ld => ld.Description)
            .HasMaxLength(500);

        builder.HasMany(ld => ld.LeaveRegistrations)
            .WithOne(lr => lr.LeaveDate)
            .HasForeignKey(lr => lr.LeaveDateId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
