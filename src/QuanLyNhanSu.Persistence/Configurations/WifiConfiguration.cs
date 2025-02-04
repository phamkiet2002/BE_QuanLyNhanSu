using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;
public class WifiConfiguration : IEntityTypeConfiguration<WifiConfig>
{
    public void Configure(EntityTypeBuilder<WifiConfig> builder)
    {
        builder.ToTable(TableNames.WifiConfig);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SSID).IsRequired().HasMaxLength(100);
        builder.Property(x => x.BSSID).IsRequired().HasMaxLength(100);
        
    }
}
