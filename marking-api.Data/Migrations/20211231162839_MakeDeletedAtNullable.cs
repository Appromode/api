using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class MakeDeletedAtNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdUserRoles_IdRoles_RoleId1",
                schema: "dbo",
                table: "IdUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_IdUserRoles_RoleId1",
                schema: "dbo",
                table: "IdUserRoles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                schema: "dbo",
                table: "IdUserRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGroups",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGrades",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Tags",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Projects",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "IdSiteArea",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "IdRolePermission",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Groups",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupMarkers",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Grades",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolders",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileVersions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileStates",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFiles",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGroups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGrades",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Tags",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Projects",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                schema: "dbo",
                table: "IdUserRoles",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "IdSiteArea",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "IdRolePermission",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Groups",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupMarkers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Grades",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileVersions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileStates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdUserRoles_RoleId1",
                schema: "dbo",
                table: "IdUserRoles",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IdUserRoles_IdRoles_RoleId1",
                schema: "dbo",
                table: "IdUserRoles",
                column: "RoleId1",
                principalSchema: "dbo",
                principalTable: "IdRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
