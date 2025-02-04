using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class AllowanceAndPenaltiesConfiguration : IEntityTypeConfiguration<AllowanceAndPenalties>
{
    public void Configure(EntityTypeBuilder<AllowanceAndPenalties> builder)
    {
        builder.ToTable(TableNames.AllowanceAndPenalties);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
           .IsRequired(false); 

        builder.Property(x => x.TypeOfAllowance)
            .IsRequired(false); 

        builder.Property(x => x.TypeOfPenalty)
            .IsRequired(false); 

        builder.Property(x => x.Money)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.EffectiveDate)
            .IsRequired()
            .HasColumnType("date");  

        builder.Property(x => x.Note)
            .HasMaxLength(500);
    }
}
