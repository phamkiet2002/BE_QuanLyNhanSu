using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class WorkSheduleConfiguration : IEntityTypeConfiguration<WorkShedule>
{
    public void Configure(EntityTypeBuilder<WorkShedule> builder)
    {
        builder.ToTable(TableNames.WorkShedule);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.BreakStartTime)
            .IsRequired();

        builder.Property(x => x.BreakEndTime)
            .IsRequired();

        builder.HasMany(x => x.Departments)
            .WithOne(d => d.WorkShedule)
            .HasForeignKey(d => d.WorkSheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
