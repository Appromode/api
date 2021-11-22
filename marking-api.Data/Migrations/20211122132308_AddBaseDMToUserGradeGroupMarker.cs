using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace marking_api.Data.Migrations
{
    public partial class AddBaseDMToUserGradeGroupMarker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "canDelete",
                schema: "dbo",
                table: "UserGrades",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                schema: "dbo",
                table: "UserGrades",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                schema: "dbo",
                table: "UserGrades",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGrades",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                schema: "dbo",
                table: "UserGrades",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                schema: "dbo",
                table: "GroupMarkers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "canDelete",
                schema: "dbo",
                table: "GroupMarkers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                schema: "dbo",
                table: "GroupMarkers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                schema: "dbo",
                table: "GroupMarkers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupMarkers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                schema: "dbo",
                table: "GroupMarkers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_GroupMarkers_GroupId",
                schema: "dbo",
                table: "GroupMarkers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMarkers_Groups_GroupId",
                schema: "dbo",
                table: "GroupMarkers",
                column: "GroupId",
                principalSchema: "dbo",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMarkers_Groups_GroupId",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropIndex(
                name: "IX_GroupMarkers_GroupId",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "canDelete",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "createdAt",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "deleted",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "canDelete",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "createdAt",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "deleted",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                schema: "dbo",
                table: "GroupMarkers");
        }
    }
}
