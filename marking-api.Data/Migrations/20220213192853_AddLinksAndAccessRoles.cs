using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class AddLinksAndAccessRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "UserGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "UserGrades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "Marks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "IdSiteArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "IdRolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "GroupMarkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFolders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFolderFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFileVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFileStates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccessRole",
                schema: "dbo",
                table: "Audit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Links",
                schema: "dbo",
                columns: table => new
                {
                    LinkId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LinkName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkIcon = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkSecurity = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkPosition = table.Column<long>(type: "bigint", nullable: false),
                    LinkParentId = table.Column<long>(type: "bigint", nullable: true),
                    canDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AccessRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_Links_Links_LinkParentId",
                        column: x => x.LinkParentId,
                        principalSchema: "dbo",
                        principalTable: "Links",
                        principalColumn: "LinkId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleLinks",
                schema: "dbo",
                columns: table => new
                {
                    RoleLinkId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleLinks", x => x.RoleLinkId);
                    table.ForeignKey(
                        name: "FK_RoleLinks_IdRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "IdRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleLinks_Links_LinkId",
                        column: x => x.LinkId,
                        principalSchema: "dbo",
                        principalTable: "Links",
                        principalColumn: "LinkId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Links_LinkParentId",
                schema: "dbo",
                table: "Links",
                column: "LinkParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleLinks_LinkId",
                schema: "dbo",
                table: "RoleLinks",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleLinks_RoleId",
                schema: "dbo",
                table: "RoleLinks",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleLinks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Links",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "IdSiteArea");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFolders");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFolderFiles");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFileVersions");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFileStates");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "FSFiles");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "dbo",
                table: "Audit");
        }
    }
}
