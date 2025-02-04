using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(TableNames.Notification);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EmployeeId).IsRequired(false);
        builder.Property(x => x.Message).IsRequired(false).HasMaxLength(255);
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.IsRead).IsRequired(false);
        builder.Property(x => x.Url).IsRequired(false).HasMaxLength(500);
    }
}
