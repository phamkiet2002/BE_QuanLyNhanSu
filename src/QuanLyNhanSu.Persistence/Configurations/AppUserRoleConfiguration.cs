using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuanLyNhanSu.Persistence.Configurations;
public sealed class AppUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "711c9d05-0ec4-46ef-bc3b-d4bd898971f3",
                UserId = "e864be78-819c-40c2-9e8e-805d209cf78a"
            }
        );
    }
}
