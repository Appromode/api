using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class AddMarkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.AddColumn<long>(
                name: "MarkId",
                schema: "dbo",
                table: "UserGrades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Marks",
                schema: "dbo",
                columns: table => new
                {
                    MarkId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaskDifficulty = table.Column<int>(type: "int", nullable: false),
                    TechnicalAchievements = table.Column<int>(type: "int", nullable: false),
                    TechnicalContributions = table.Column<int>(type: "int", nullable: false),
                    ProjectContributions = table.Column<int>(type: "int", nullable: false),
                    TeamworkSkills = table.Column<int>(type: "int", nullable: false),
                    CriticalReflection = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    canDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.MarkId);
                    table.ForeignKey(
                        name: "FK_Marks_IdUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "IdUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Marks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "dbo",
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserGrades_MarkId",
                schema: "dbo",
                table: "UserGrades",
                column: "MarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_ProjectId",
                schema: "dbo",
                table: "Marks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_UserId",
                schema: "dbo",
                table: "Marks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGrades_Marks_MarkId",
                schema: "dbo",
                table: "UserGrades",
                column: "MarkId",
                principalSchema: "dbo",
                principalTable: "Marks",
                principalColumn: "MarkId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGrades_Marks_MarkId",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropTable(
                name: "Marks",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_UserGrades_MarkId",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "MarkId",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                schema: "dbo",
                table: "UserGrades",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
