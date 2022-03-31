using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class RemoveDeletedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "UserTags");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "UserTags");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "IdSiteArea");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "IdSiteArea");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupTags");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "GroupTags");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolders");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolders");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderFiles");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderFiles");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileVersions");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileVersions");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileStates");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileStates");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "deletedAt",
                schema: "dbo",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Audit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserTags",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "UserTags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGroups",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGroups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "UserGrades",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGrades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Threads",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Threads",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Tags",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Tags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Projects",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Links",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Links",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Invites",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Invites",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "IdSiteArea",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "IdSiteArea",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "IdRolePermission",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "IdRolePermission",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupTags",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "GroupTags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Groups",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Groups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "GroupMarkers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "GroupMarkers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Grades",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Grades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolders",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileVersions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileVersions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFileStates",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileStates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "FSFiles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Feedback",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Feedback",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Comments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Comments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedAt",
                schema: "dbo",
                table: "Audit",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Audit",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
