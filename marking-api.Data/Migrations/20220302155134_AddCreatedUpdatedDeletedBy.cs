using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class AddCreatedUpdatedDeletedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "UserGroups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGroups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "UserGroups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "UserGrades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGrades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "UserGrades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Threads",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Threads",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Threads",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Tags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Tags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Tags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Links",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Links",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Links",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "IdSiteArea",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "IdSiteArea",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "IdSiteArea",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "IdRolePermission",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "IdRolePermission",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "IdRolePermission",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Groups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Groups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Groups",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "GroupMarkers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "GroupMarkers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "GroupMarkers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Grades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Grades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Grades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "FSFolders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFolders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "FSFileVersions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileVersions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFileVersions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "FSFileStates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileStates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFileStates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "FSFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Feedback",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Feedback",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Feedback",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Comments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Comments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Comments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "Audit",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "Audit",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "Audit",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "IdSiteArea");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "IdSiteArea");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "IdSiteArea");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "FSFolders");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolders");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFolders");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "FSFolderFiles");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFolderFiles");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFolderFiles");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "FSFileVersions");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileVersions");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFileVersions");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "FSFileStates");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFileStates");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFileStates");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "Audit");
        }
    }
}
