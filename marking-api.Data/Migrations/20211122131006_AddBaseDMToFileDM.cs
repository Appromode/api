using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace marking_api.Data.Migrations
{
    public partial class AddBaseDMToFileDM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "canDelete",
                schema: "dbo",
                table: "FSFiles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                schema: "dbo",
                table: "FSFiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                schema: "dbo",
                table: "FSFiles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                schema: "dbo",
                table: "FSFiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "canDelete",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "createdAt",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "deleted",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                schema: "dbo",
                table: "FSFiles");
        }
    }
}
