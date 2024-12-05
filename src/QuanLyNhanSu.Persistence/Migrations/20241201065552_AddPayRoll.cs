using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddPayRoll : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "1dac1f8d-6588-4875-98f4-5109bb1cdd80");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "7e980330-e263-425b-a13d-a01cfe1d1453");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "7f792bce-4d16-4f10-a002-2f1b7921ed0b");

        migrationBuilder.DeleteData(
            table: "AppRoles",
            keyColumn: "Id",
            keyValue: "a52acb87-d556-479c-b4c5-509a68c12002");

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "WorkShedule",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "WorkPlace",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Salary",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Salary",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Position",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Level",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "LeaveRegistration",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "LeaveRegistration",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "LeaveDate",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Employee",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Department",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Contract",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Contract",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "AttendanceSetting",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedDate",
            table: "Attendance",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "Attendance",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedDate",
            table: "AllowanceAndPenalties",
            type: "datetime2",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "PayRoll",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                SalaryGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                SalaryNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                PayRollStatus = table.Column<int>(type: "int", nullable: true),
                PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                EmployeeId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PayRoll", x => x.Id);
                table.ForeignKey(
                    name: "FK_PayRoll_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_PayRoll_Employee_EmployeeId1",
                    column: x => x.EmployeeId1,
                    principalTable: "Employee",
                    principalColumn: "Id");
            });

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
            name: "IX_PayRoll_EmployeeId",
            table: "PayRoll",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_PayRoll_EmployeeId1",
            table: "PayRoll",
            column: "EmployeeId1");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PayRoll");

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
            name: "UpdatedDate",
            table: "WorkShedule");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "WorkPlace");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Salary");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Salary");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Position");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Level");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "LeaveRegistration");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "LeaveRegistration");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "LeaveDate");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Employee");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Department");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Contract");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Contract");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "AttendanceSetting");

        migrationBuilder.DropColumn(
            name: "DeletedDate",
            table: "Attendance");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "Attendance");

        migrationBuilder.DropColumn(
            name: "UpdatedDate",
            table: "AllowanceAndPenalties");

        migrationBuilder.InsertData(
            table: "AppRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
            values: new object[,]
            {
                { "1dac1f8d-6588-4875-98f4-5109bb1cdd80", null, "Quản trị hệ thống: Quản lý toàn bộ dữ liệu, có quyền thêm, sửa, xóa tất cả các bảng, quản lý người dùng và phân quyền.", "Admin", "ADMIN" },
                { "7e980330-e263-425b-a13d-a01cfe1d1453", null, "Nhân viên: Xem thông tin cá nhân, đăng ký nghỉ phép, theo dõi lịch làm việc và lương.", "Employee", "EMPLOYEE" },
                { "7f792bce-4d16-4f10-a002-2f1b7921ed0b", null, "Trưởng phòng: Quản lý nhân viên trong phòng ban, phê duyệt nghỉ phép, giám sát chấm công và theo dõi các phụ cấp, vi phạm liên quan đến phòng ban.", "Department Manager", "DEPARTMENT_MANAGER" },
                { "a52acb87-d556-479c-b4c5-509a68c12002", null, "Quản lý nhân sự: Quản lý nhân viên, phòng ban, các chức vụ, lương, hợp đồng, lịch làm việc, đăng ký nghỉ phép, chấm công và vi phạm.", "HR Manager", "HR_MANAGER" }
            });
    }
}
