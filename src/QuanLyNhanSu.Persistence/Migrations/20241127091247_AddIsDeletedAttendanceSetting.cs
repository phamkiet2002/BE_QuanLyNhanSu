using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddIsDeletedAttendanceSetting : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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

        migrationBuilder.AddColumn<int>(
            name: "Status",
            table: "Contract",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "AttendanceSetting",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "AttendanceSetting",
            type: "bit",
            nullable: true);

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
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

        migrationBuilder.DropColumn(
            name: "Status",
            table: "Contract");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "AttendanceSetting");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "AttendanceSetting");

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
}
