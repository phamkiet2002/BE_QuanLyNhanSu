using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddAppRole : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AspNetRoleClaims_AppRoles_AppRoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AppRoles_AppRoleId",
            table: "AspNetUserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_PositionRole_AppRoles_RoleId",
            table: "PositionRole");

        migrationBuilder.DropTable(
            name: "AppRoles");

        migrationBuilder.DropIndex(
            name: "IX_AspNetUserRoles_AppRoleId",
            table: "AspNetUserRoles");

        migrationBuilder.DropIndex(
            name: "IX_AspNetRoleClaims_AppRoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.DropColumn(
            name: "AppRoleId",
            table: "AspNetUserRoles");

        migrationBuilder.DropColumn(
            name: "AppRoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.AddForeignKey(
            name: "FK_PositionRole_AspNetRoles_RoleId",
            table: "PositionRole",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_PositionRole_AspNetRoles_RoleId",
            table: "PositionRole");

        migrationBuilder.AddColumn<string>(
            name: "AppRoleId",
            table: "AspNetUserRoles",
            type: "nvarchar(450)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "AppRoleId",
            table: "AspNetRoleClaims",
            type: "nvarchar(450)",
            nullable: true);

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

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_AppRoleId",
            table: "AspNetUserRoles",
            column: "AppRoleId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_AppRoleId",
            table: "AspNetRoleClaims",
            column: "AppRoleId");

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetRoleClaims_AppRoles_AppRoleId",
            table: "AspNetRoleClaims",
            column: "AppRoleId",
            principalTable: "AppRoles",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserRoles_AppRoles_AppRoleId",
            table: "AspNetUserRoles",
            column: "AppRoleId",
            principalTable: "AppRoles",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_PositionRole_AppRoles_RoleId",
            table: "PositionRole",
            column: "RoleId",
            principalTable: "AppRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
