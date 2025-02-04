using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class ContractConfiguration : IEntityTypeConfiguration<Domain.Entities.Contract>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Contract> builder)
    {
        builder.ToTable(TableNames.Contract);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ContracNumber)
            .IsRequired();

        builder.Property(c => c.SignDate)
            .IsRequired();

        builder.Property(c => c.EffectiveDate)
            .IsRequired();

        builder.Property(c => c.ExpirationDate)
            .IsRequired();

        builder.HasOne(c => c.Employee)
            .WithMany(e => e.Contracts)
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
