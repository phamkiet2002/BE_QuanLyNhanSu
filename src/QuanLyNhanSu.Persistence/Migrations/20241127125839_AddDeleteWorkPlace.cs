using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddDeleteWorkPlace : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "08a12a17-5195-433e-857d-f2c36cc0a782");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "924e4f5f-9ea8-4624-b3d2-63f0e2d077d6");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "939660be-2a45-4f0a-ab00-d3cd160eaa6d");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "aa586dd5-f17b-4b06-b406-98c297533d68");

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "WorkPlace",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "WorkPlace",
            type: "bit",
            nullable: true);

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "6ad5a99c-de93-4162-a4b9-f7df4183af24", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" },
                { "81fa18c7-b997-48fd-b598-ccf8092a97cd", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "bdc3a7d9-4d05-4096-ae0f-08769b1b780a", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" },
                { "ff093041-d46a-4951-bc5e-734fb0cfbb11", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "6ad5a99c-de93-4162-a4b9-f7df4183af24");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "81fa18c7-b997-48fd-b598-ccf8092a97cd");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "bdc3a7d9-4d05-4096-ae0f-08769b1b780a");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "ff093041-d46a-4951-bc5e-734fb0cfbb11");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "WorkPlace");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "WorkPlace");

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "08a12a17-5195-433e-857d-f2c36cc0a782", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "924e4f5f-9ea8-4624-b3d2-63f0e2d077d6", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "939660be-2a45-4f0a-ab00-d3cd160eaa6d", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" },
                { "aa586dd5-f17b-4b06-b406-98c297533d68", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" }
            });
    }
}
