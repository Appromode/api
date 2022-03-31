using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class ChangeFeedbackInGradeDM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_FSFiles_FeedbackId",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Marks_FeedbackId",
                schema: "dbo",
                table: "Grades",
                column: "FeedbackId",
                principalSchema: "dbo",
                principalTable: "Marks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Marks_FeedbackId",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_FSFiles_FeedbackId",
                schema: "dbo",
                table: "Grades",
                column: "FeedbackId",
                principalSchema: "dbo",
                principalTable: "FSFiles",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
