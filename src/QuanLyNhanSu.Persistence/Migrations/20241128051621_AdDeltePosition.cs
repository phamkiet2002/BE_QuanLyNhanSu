using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AdDeltePosition : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "WorkShedule",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "WorkShedule",
            type: "bit",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Position",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Position",
            type: "bit",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Level",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Level",
            type: "bit",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Employee",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "Status",
            table: "Employee",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Department",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Department",
            type: "bit",
            nullable: true);

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
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

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "WorkShedule");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "WorkShedule");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Position");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Position");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Level");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Level");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Employee");

        migrationBuilder.DropColumn(
            name: "Status",
            table: "Employee");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Department");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Department");

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
}
