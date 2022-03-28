using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class AddThreadTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentTime",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Replies",
                schema: "dbo",
                table: "Comments",
                newName: "QuotedCommentId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "dbo",
                table: "Comments",
                newName: "ParentThreadId");

            migrationBuilder.RenameColumn(
                name: "ParentCommentId",
                schema: "dbo",
                table: "Comments",
                newName: "ProjectDMProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProjectId",
                schema: "dbo",
                table: "Comments",
                newName: "IX_Comments_ParentThreadId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentCommentId",
                schema: "dbo",
                table: "Comments",
                newName: "IX_Comments_ProjectDMProjectId");

            migrationBuilder.CreateTable(
                name: "Threads",
                schema: "dbo",
                columns: table => new
                {
                    ThreadId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ThreadTitle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ThreadDesc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ThreadStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Replies = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkedProjectId = table.Column<long>(type: "bigint", nullable: false),
                    TotalMembers = table.Column<int>(type: "int", nullable: true),
                    canDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AccessRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.ThreadId);
                    table.ForeignKey(
                        name: "FK_Threads_IdUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "IdUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Threads_Projects_LinkedProjectId",
                        column: x => x.LinkedProjectId,
                        principalSchema: "dbo",
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_QuotedCommentId",
                schema: "dbo",
                table: "Comments",
                column: "QuotedCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_LinkedProjectId",
                schema: "dbo",
                table: "Threads",
                column: "LinkedProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_UserId",
                schema: "dbo",
                table: "Threads",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_QuotedCommentId",
                schema: "dbo",
                table: "Comments",
                column: "QuotedCommentId",
                principalSchema: "dbo",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_ProjectDMProjectId",
                schema: "dbo",
                table: "Comments",
                column: "ProjectDMProjectId",
                principalSchema: "dbo",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Threads_ParentThreadId",
                schema: "dbo",
                table: "Comments",
                column: "ParentThreadId",
                principalSchema: "dbo",
                principalTable: "Threads",
                principalColumn: "ThreadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_QuotedCommentId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_ProjectDMProjectId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Threads_ParentThreadId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Threads",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Comments_QuotedCommentId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "QuotedCommentId",
                schema: "dbo",
                table: "Comments",
                newName: "Replies");

            migrationBuilder.RenameColumn(
                name: "ProjectDMProjectId",
                schema: "dbo",
                table: "Comments",
                newName: "ParentCommentId");

            migrationBuilder.RenameColumn(
                name: "ParentThreadId",
                schema: "dbo",
                table: "Comments",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProjectDMProjectId",
                schema: "dbo",
                table: "Comments",
                newName: "IX_Comments_ParentCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentThreadId",
                schema: "dbo",
                table: "Comments",
                newName: "IX_Comments_ProjectId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentTime",
                schema: "dbo",
                table: "Comments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                schema: "dbo",
                table: "Comments",
                column: "ParentCommentId",
                principalSchema: "dbo",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                schema: "dbo",
                table: "Comments",
                column: "ProjectId",
                principalSchema: "dbo",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
