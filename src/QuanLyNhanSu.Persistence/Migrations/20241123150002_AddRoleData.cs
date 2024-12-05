using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddRoleData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "65f79ae8-f8e8-4032-b9b7-ee9ea12e466b", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" },
                { "9c2d8e7b-e188-4258-926a-d4cdd09e7ac7", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "a66a2906-4aab-4835-8ca2-1e14062fa9ba", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" },
                { "ba4738d7-60dc-4234-a6a0-bdc128acbebe", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "65f79ae8-f8e8-4032-b9b7-ee9ea12e466b");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "9c2d8e7b-e188-4258-926a-d4cdd09e7ac7");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "a66a2906-4aab-4835-8ca2-1e14062fa9ba");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "ba4738d7-60dc-4234-a6a0-bdc128acbebe");
    }
}
