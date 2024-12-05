using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddAppRole1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            table: "AspNetUserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_PositionRole_AspNetRoles_RoleId",
            table: "PositionRole");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.CreateTable(
            name: "AppRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppRoles", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AppRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetRoleClaims_AppRoles_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId",
            principalTable: "AppRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserRoles_AppRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId",
            principalTable: "AppRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PositionRole_AppRoles_RoleId",
            table: "PositionRole",
            column: "RoleId",
            principalTable: "AppRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AspNetRoleClaims_AppRoles_RoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AppRoles_RoleId",
            table: "AspNetUserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_PositionRole_AppRoles_RoleId",
            table: "PositionRole");

        migrationBuilder.DropTable(
            name: "AppRoles");

        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_PositionRole_AspNetRoles_RoleId",
            table: "PositionRole",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
