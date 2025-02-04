using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Persistence.Configurations;
public sealed class PayRollConfiguration : IEntityTypeConfiguration<PayRoll>
{
    public void Configure(EntityTypeBuilder<PayRoll> builder)
    {
        builder.ToTable(nameof(PayRoll));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EmployeeId).IsRequired(false);
        builder.Property(x => x.FromDate).IsRequired();
        builder.Property(x => x.ToDate).IsRequired();
        builder.Property(x => x.SalaryGross).IsRequired();
        builder.Property(x => x.SalaryNet).IsRequired();
        builder.Property(x => x.PayRollStatus).IsRequired(false);
        builder.Property(x => x.PaymentDate).IsRequired(false);

        builder.HasOne(x => x.Employee)
            .WithMany(x=> x.PayRolls)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
