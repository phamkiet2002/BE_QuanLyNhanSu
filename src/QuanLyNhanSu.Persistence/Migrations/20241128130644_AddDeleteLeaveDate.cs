using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddDeleteLeaveDate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "289ddae4-0c5d-4862-be49-3d101b5e0473");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "cc16df91-e26b-463e-ab61-c6ab9a2b25b1");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "d12839ea-3e69-4edd-a1f3-bf56251ba334");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "dd4662ec-322d-4fcd-9a18-78a2d1d2f471");

        migrationBuilder.AddColumn<DateTime>(
            name: "DateCancel",
            table: "LeaveRegistration",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "LeaveDate",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "LeaveDate",
            type: "bit",
            nullable: true);

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
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

        migrationBuilder.DropColumn(
            name: "DateCancel",
            table: "LeaveRegistration");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "LeaveDate");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "LeaveDate");

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "289ddae4-0c5d-4862-be49-3d101b5e0473", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "cc16df91-e26b-463e-ab61-c6ab9a2b25b1", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" },
                { "d12839ea-3e69-4edd-a1f3-bf56251ba334", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "dd4662ec-322d-4fcd-9a18-78a2d1d2f471", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" }
            });
    }
}
