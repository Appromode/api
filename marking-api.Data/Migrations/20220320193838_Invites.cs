using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class Invites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                schema: "dbo",
                table: "UserTags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "deletedBy",
                schema: "dbo",
                table: "UserTags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                schema: "dbo",
                table: "UserTags",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invites",
                schema: "dbo",
                columns: table => new
                {
                    InviteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReceiverId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    canDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invites", x => x.InviteId);
                    table.ForeignKey(
                        name: "FK_Invites_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "dbo",
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invites_IdUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "dbo",
                        principalTable: "IdUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invites_IdUsers_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "dbo",
                        principalTable: "IdUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_GroupId",
                schema: "dbo",
                table: "Invites",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_ReceiverId",
                schema: "dbo",
                table: "Invites",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_SenderId",
                schema: "dbo",
                table: "Invites",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invites",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "createdBy",
                schema: "dbo",
                table: "UserTags");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                schema: "dbo",
                table: "UserTags");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                schema: "dbo",
                table: "UserTags");
        }
    }
}
