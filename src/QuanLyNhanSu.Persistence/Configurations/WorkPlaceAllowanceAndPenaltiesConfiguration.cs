using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

internal class WorkPlaceAllowanceAndPenaltiesConfiguration : IEntityTypeConfiguration<WorkPlaceAllowanceAndPenalties>
{
    public void Configure(EntityTypeBuilder<WorkPlaceAllowanceAndPenalties> builder)
    {
        builder.ToTable(TableNames.WorkPlaceAllowanceAndPenalties);

        builder.Ignore(x => x.Id);

        builder.HasKey(ed => new { ed.WorkPlaceId, ed.AllowanceAndPenaltiesId });

        builder.Property(ed => ed.Status)
            .IsRequired();

        builder.Property(ed => ed.CreatedDate)
            .IsRequired();

        builder.HasOne(ed => ed.WorkPlace)
            .WithMany(e => e.WorkPlaceAndAllowanceAndPenalties)
            .HasForeignKey(ed => ed.WorkPlaceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ed => ed.AllowanceAndPenalties)
            .WithMany(d => d.WorkPlaceAndAllowanceAndPenalties)
            .HasForeignKey(ed => ed.AllowanceAndPenaltiesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
