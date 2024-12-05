using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSu.Persistence.Migrations;

/// <inheritdoc />
public partial class AddRelationRole : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AspNetRoleClaims_AppRoles_AppRoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AppRoles_AppRoleId",
            table: "AspNetUserRoles");

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
    }
}
