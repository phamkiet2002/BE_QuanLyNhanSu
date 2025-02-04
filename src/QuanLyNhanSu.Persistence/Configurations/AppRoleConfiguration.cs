using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhanSu.Domain.Entities.Identity;
using QuanLyNhanSu.Persistence.Constants;

namespace QuanLyNhanSu.Persistence.Configurations;
public sealed class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(TableNames.AppRoles);

        builder.Property(p => p.Description).HasMaxLength(200).IsRequired(false);


        var roles = new List<AppRole>
        {
            new AppRole
            {
                 Id = "711c9d05-0ec4-46ef-bc3b-d4bd898971f3",
                 Name = "Admin",
                 NormalizedName = "ADMIN",
                 Description = "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền."
            },
            new AppRole
            {
                 Name = "HR Manager",
                 NormalizedName = "HR_MANAGER",
                 Description = "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm."
            },
            new AppRole
            {
                 Name = "Department Manager",
                 NormalizedName = "DEPARTMENT_MANAGER",
                 Description = "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban."
            },
            new AppRole
            {
                 Name = "Employee",
                 NormalizedName = "EMPLOYEE",
                 Description = "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương."
            }
        };

        builder.HasData(roles);
    }
}
