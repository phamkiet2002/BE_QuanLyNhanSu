using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class WorkPlaceConfiguration : IEntityTypeConfiguration<WorkPlace>
{
    public void Configure(EntityTypeBuilder<WorkPlace> builder)
    {
        builder.ToTable(TableNames.WorkPlace);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
           .IsRequired()
           .HasMaxLength(200);

        builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Address)
            .HasMaxLength(300);

        builder.HasMany(x => x.Departments)
           .WithOne(d => d.WorkPlace)
           .HasForeignKey(d => d.WorkPlaceId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Employees)
           .WithOne(e => e.WorkPlace)
           .HasForeignKey(e => e.WorkPlaceId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
