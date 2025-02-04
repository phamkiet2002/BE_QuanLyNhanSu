using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;
public sealed class AttendanceSettingConfiguration : IEntityTypeConfiguration<AttendanceSetting>
{
    public void Configure(EntityTypeBuilder<AttendanceSetting> builder)
    {
        builder.ToTable(TableNames.AttendanceSetting);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MaximumLateAllowed).IsRequired();
        builder.Property(x => x.MaxEarlyLeaveAllowed).IsRequired();
        builder.Property(x => x.Status).IsRequired(false);
    }
}
