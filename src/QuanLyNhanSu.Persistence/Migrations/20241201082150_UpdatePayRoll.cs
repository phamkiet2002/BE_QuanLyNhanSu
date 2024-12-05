using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdatePayRoll : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PayRoll_Employee_EmployeeId1",
            table: "PayRoll");

        migrationBuilder.DropIndex(
            name: "IX_PayRoll_EmployeeId1",
            table: "PayRoll");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "a7fc8ca5-d41d-4287-9422-cab99da87621");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "ae0e79c0-7ca9-4c67-8759-3180e885499a");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "c08a1e77-0748-47bd-a98d-50561c0b8c30");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "e89543fc-5917-4eea-8663-eec665bec22e");

        migrationBuilder.DropColumn(
            name: "EmployeeId1",
            table: "PayRoll");

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "1fdc1b82-cad7-4fdd-b45f-92134c7d40b0", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "a5398478-d2ab-4e01-9f24-baa7a272591b", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" },
                { "e01c9fac-09ad-4744-8928-b1263994f59d", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "e73beb3f-ae41-4d3d-bf5e-deae8a2450cc", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "1fdc1b82-cad7-4fdd-b45f-92134c7d40b0");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "a5398478-d2ab-4e01-9f24-baa7a272591b");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "e01c9fac-09ad-4744-8928-b1263994f59d");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "e73beb3f-ae41-4d3d-bf5e-deae8a2450cc");

        migrationBuilder.AddColumn<Guid>(
            name: "EmployeeId1",
            table: "PayRoll",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "a7fc8ca5-d41d-4287-9422-cab99da87621", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" },
                { "ae0e79c0-7ca9-4c67-8759-3180e885499a", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "c08a1e77-0748-47bd-a98d-50561c0b8c30", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "e89543fc-5917-4eea-8663-eec665bec22e", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_PayRoll_EmployeeId1",
            table: "PayRoll",
            column: "EmployeeId1");

        migrationBuilder.AddForeignKey(
            name: "FK_PayRoll_Employee_EmployeeId1",
            table: "PayRoll",
            column: "EmployeeId1",
            principalTable: "Employee",
            principalColumn: "Id");
    }
}
