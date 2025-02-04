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

        builder.HasData(
            new AppUser
            {
                Id = "e864be78-819c-40c2-9e8e-805d209cf78a",
                UserName = "admin",
                NormalizedUserName = "ADMIN", // Tên đăng nhập chuẩn hóa (thường viết hoa)
                Email = "admin@gmail.com", // Email
                NormalizedEmail = "ADMIN@GMAIL.COM", // Email chuẩn hóa
                EmailConfirmed = false, // Email chưa xác nhận
                PasswordHash = "AQAAAAIAAYagAAAAEKe9kLTkmpZ6Ay5hsXnSaBIn99g4mtrZwS6h71E6/+0gHbnl9xQG9Nls4vuo/L4khQ==", // Mật khẩu hash
                SecurityStamp = "K7JSDLAPWQOKQRBEZ7JGWHR3CLBKGOBA", // Giá trị ngẫu nhiên để bảo mật
                ConcurrencyStamp = "be593c9e-c3d4-41a8-a9f1-f616768216e2", // Giá trị để track thay đổi
                PhoneNumber = null, // Không có số điện thoại
                PhoneNumberConfirmed = false, // Số điện thoại chưa xác nhận
                TwoFactorEnabled = false, // Không bật xác thực 2 yếu tố
                LockoutEnd = null, // Không bị khóa
                LockoutEnabled = true, // Cho phép khóa tài khoản
                AccessFailedCount = 0, // Số lần đăng nhập thất bại
                EmployeeId = null
            }
        );
    }
}
