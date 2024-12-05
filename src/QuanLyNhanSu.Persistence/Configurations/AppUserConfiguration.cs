using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyNhanSu.Domain.Entities.Identity;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(TableNames.AppUsers);
        //builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Employee)
            .WithOne(x => x.AppUser)
            .HasForeignKey<AppUser>(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);



        //// Each User can have many UserClaims
        //builder.HasMany(e => e.Claims)
        //    .WithOne()
        //    .HasForeignKey(uc => uc.UserId)
        //    .IsRequired();

        //// Each User can have many UserLogins
        //builder.HasMany(e => e.Logins)
        //    .WithOne()
        //    .HasForeignKey(ul => ul.UserId)
        //    .IsRequired();

        //// Each User can have many UserTokens
        //builder.HasMany(e => e.Tokens)
        //    .WithOne()
        //    .HasForeignKey(ut => ut.UserId)
        //    .IsRequired();

        //// Each User can have many entries in the UserRole join table
        //builder.HasMany(e => e.UserRoles)
        //    .WithOne()
        //    .HasForeignKey(ur => ur.UserId)
        //    .IsRequired();
    }
}
