using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddDeleteAllowanceAndPenalties : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "15b5d543-947e-4a0a-b3fe-f6fc6f8646e5");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "8375d908-dfdc-476e-9e0e-c8472ec24c5f");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "bc1c1cd1-c644-4219-a671-642f8a1ac2c8");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "e327b8db-0b04-49ed-ab62-35b0fa303cd8");

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "AllowanceAndPenalties",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "AllowanceAndPenalties",
            type: "bit",
            nullable: true);

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "358d08bb-4fd3-440d-969e-4d899fb59375", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "3ba3e235-5f1f-43f6-8558-fe0d830a0949", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" },
                { "71aee981-da4d-4242-a5a0-68d031ef9507", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "9fc66145-e225-4203-9253-45399b9c7c85", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "358d08bb-4fd3-440d-969e-4d899fb59375");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "3ba3e235-5f1f-43f6-8558-fe0d830a0949");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "71aee981-da4d-4242-a5a0-68d031ef9507");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "9fc66145-e225-4203-9253-45399b9c7c85");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "AllowanceAndPenalties");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "AllowanceAndPenalties");

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "15b5d543-947e-4a0a-b3fe-f6fc6f8646e5", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" },
                { "8375d908-dfdc-476e-9e0e-c8472ec24c5f", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "bc1c1cd1-c644-4219-a671-642f8a1ac2c8", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "e327b8db-0b04-49ed-ab62-35b0fa303cd8", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" }
            });
    }
}
