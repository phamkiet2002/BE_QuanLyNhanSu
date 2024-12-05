using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AllowanceAndPenalties",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<int>(type: "int", nullable: true),
                TypeOfAllowance = table.Column<int>(type: "int", nullable: true),
                TypeOfPenalty = table.Column<int>(type: "int", nullable: true),
                Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                EffectiveDate = table.Column<DateTime>(type: "date", nullable: false),
                Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                IsAllWorkPlace = table.Column<bool>(type: "bit", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AllowanceAndPenalties", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AttendanceSetting",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                MaximumLateAllowed = table.Column<TimeSpan>(type: "time", nullable: false),
                MaxEarlyLeaveAllowed = table.Column<TimeSpan>(type: "time", nullable: false),
                Status = table.Column<int>(type: "int", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AttendanceSetting", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "LeaveDate",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                TotalAnnualLeaveDate = table.Column<int>(type: "int", nullable: false),
                MaximumDaysOffPerMonth = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LeaveDate", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Level",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Level", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Position",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Position", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "WorkPlace",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkPlace", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "WorkShedule",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                BreakStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                BreakEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkShedule", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AppRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppRoles", x => x.Id);
                table.ForeignKey(
                    name: "FK_AppRoles_AspNetRoles_Id",
                    column: x => x.Id,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Employee",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                MaNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                IdentityCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Gender = table.Column<int>(type: "int", nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                BankAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                WorkPlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Employee", x => x.Id);
                table.ForeignKey(
                    name: "FK_Employee_WorkPlace_WorkPlaceId",
                    column: x => x.WorkPlaceId,
                    principalTable: "WorkPlace",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "WorkPlaceAllowanceAndPenalties",
            columns: table => new
            {
                WorkPlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AllowanceAndPenaltiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkPlaceAllowanceAndPenalties", x => new { x.WorkPlaceId, x.AllowanceAndPenaltiesId });
                table.ForeignKey(
                    name: "FK_WorkPlaceAllowanceAndPenalties_AllowanceAndPenalties_AllowanceAndPenaltiesId",
                    column: x => x.AllowanceAndPenaltiesId,
                    principalTable: "AllowanceAndPenalties",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_WorkPlaceAllowanceAndPenalties_WorkPlace_WorkPlaceId",
                    column: x => x.WorkPlaceId,
                    principalTable: "WorkPlace",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Department",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                WorkPlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                WorkSheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Department", x => x.Id);
                table.ForeignKey(
                    name: "FK_Department_WorkPlace_WorkPlaceId",
                    column: x => x.WorkPlaceId,
                    principalTable: "WorkPlace",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Department_WorkShedule_WorkSheduleId",
                    column: x => x.WorkSheduleId,
                    principalTable: "WorkShedule",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "PositionRole",
            columns: table => new
            {
                PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PositionRole", x => new { x.PositionId, x.RoleId });
                table.ForeignKey(
                    name: "FK_PositionRole_AppRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AppRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PositionRole_Position_PositionId",
                    column: x => x.PositionId,
                    principalTable: "Position",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AppUsers",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppUsers", x => x.Id);
                table.ForeignKey(
                    name: "FK_AppUsers_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Attendance",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsLate = table.Column<bool>(type: "bit", nullable: false),
                LateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsEarlyLeave = table.Column<bool>(type: "bit", nullable: false),
                EarlyLeaveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                OvertimeOutsideWorkHours = table.Column<bool>(type: "bit", nullable: false),
                StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                PendingApproval = table.Column<int>(type: "int", nullable: true),
                ApprovedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                ApprovedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Attendance", x => x.Id);
                table.ForeignKey(
                    name: "FK_Attendance_Employee_ApprovedId",
                    column: x => x.ApprovedId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Attendance_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Contract",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ContracNumber = table.Column<int>(type: "int", nullable: false),
                SignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Contract", x => x.Id);
                table.ForeignKey(
                    name: "FK_Contract_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "EmployeeLevel",
            columns: table => new
            {
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmployeeLevel", x => new { x.EmployeeId, x.LevelId });
                table.ForeignKey(
                    name: "FK_EmployeeLevel_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_EmployeeLevel_Level_LevelId",
                    column: x => x.LevelId,
                    principalTable: "Level",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EmployeePosition",
            columns: table => new
            {
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmployeePosition", x => new { x.EmployeeId, x.PositionId });
                table.ForeignKey(
                    name: "FK_EmployeePosition_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_EmployeePosition_Position_PositionId",
                    column: x => x.PositionId,
                    principalTable: "Position",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "LeaveRegistration",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TypeOfLeave = table.Column<int>(type: "int", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                HalfDayOff = table.Column<int>(type: "int", nullable: true),
                DayOff = table.Column<DateTime>(type: "datetime2", nullable: true),
                LeaveReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                PendingApproval = table.Column<int>(type: "int", nullable: true),
                ApprovedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                ApprovedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LeaveDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LeaveRegistration", x => x.Id);
                table.ForeignKey(
                    name: "FK_LeaveRegistration_Employee_ApprovedId",
                    column: x => x.ApprovedId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_LeaveRegistration_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_LeaveRegistration_LeaveDate_LeaveDateId",
                    column: x => x.LeaveDateId,
                    principalTable: "LeaveDate",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Salary",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Salarys = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: true),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Salary", x => x.Id);
                table.ForeignKey(
                    name: "FK_Salary_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EmployeeDepartment",
            columns: table => new
            {
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmployeeDepartment", x => new { x.EmployeeId, x.DepartmentId });
                table.ForeignKey(
                    name: "FK_EmployeeDepartment_Department_DepartmentId",
                    column: x => x.DepartmentId,
                    principalTable: "Department",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_EmployeeDepartment_Employee_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employee",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AppUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AppUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AppUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AppUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AppUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AppUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "IX_AppUsers_EmployeeId",
            table: "AppUsers",
            column: "EmployeeId",
            unique: true,
            filter: "[EmployeeId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AppUsers",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AspNetUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "AspNetUserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_Attendance_ApprovedId",
            table: "Attendance",
            column: "ApprovedId");

        migrationBuilder.CreateIndex(
            name: "IX_Attendance_EmployeeId",
            table: "Attendance",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_Contract_EmployeeId",
            table: "Contract",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_Department_WorkPlaceId",
            table: "Department",
            column: "WorkPlaceId");

        migrationBuilder.CreateIndex(
            name: "IX_Department_WorkSheduleId",
            table: "Department",
            column: "WorkSheduleId");

        migrationBuilder.CreateIndex(
            name: "IX_Employee_WorkPlaceId",
            table: "Employee",
            column: "WorkPlaceId");

        migrationBuilder.CreateIndex(
            name: "IX_EmployeeDepartment_DepartmentId",
            table: "EmployeeDepartment",
            column: "DepartmentId");

        migrationBuilder.CreateIndex(
            name: "IX_EmployeeLevel_LevelId",
            table: "EmployeeLevel",
            column: "LevelId");

        migrationBuilder.CreateIndex(
            name: "IX_EmployeePosition_PositionId",
            table: "EmployeePosition",
            column: "PositionId");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRegistration_ApprovedId",
            table: "LeaveRegistration",
            column: "ApprovedId");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRegistration_EmployeeId",
            table: "LeaveRegistration",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRegistration_LeaveDateId",
            table: "LeaveRegistration",
            column: "LeaveDateId");

        migrationBuilder.CreateIndex(
            name: "IX_PositionRole_RoleId",
            table: "PositionRole",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_Salary_EmployeeId",
            table: "Salary",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_WorkPlaceAllowanceAndPenalties_AllowanceAndPenaltiesId",
            table: "WorkPlaceAllowanceAndPenalties",
            column: "AllowanceAndPenaltiesId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AspNetRoleClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens");

        migrationBuilder.DropTable(
            name: "Attendance");

        migrationBuilder.DropTable(
            name: "AttendanceSetting");

        migrationBuilder.DropTable(
            name: "Contract");

        migrationBuilder.DropTable(
            name: "EmployeeDepartment");

        migrationBuilder.DropTable(
            name: "EmployeeLevel");

        migrationBuilder.DropTable(
            name: "EmployeePosition");

        migrationBuilder.DropTable(
            name: "LeaveRegistration");

        migrationBuilder.DropTable(
            name: "PositionRole");

        migrationBuilder.DropTable(
            name: "Salary");

        migrationBuilder.DropTable(
            name: "WorkPlaceAllowanceAndPenalties");

        migrationBuilder.DropTable(
            name: "AppUsers");

        migrationBuilder.DropTable(
            name: "Department");

        migrationBuilder.DropTable(
            name: "Level");

        migrationBuilder.DropTable(
            name: "LeaveDate");

        migrationBuilder.DropTable(
            name: "AppRoles");

        migrationBuilder.DropTable(
            name: "Position");

        migrationBuilder.DropTable(
            name: "AllowanceAndPenalties");

        migrationBuilder.DropTable(
            name: "Employee");

        migrationBuilder.DropTable(
            name: "WorkShedule");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.DropTable(
            name: "WorkPlace");
    }
}
