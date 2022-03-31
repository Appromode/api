using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class ChangeMarkTableToFeedbackTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Marks_FeedbackId",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_IdUsers_UserId",
                schema: "dbo",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Projects_ProjectId",
                schema: "dbo",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGrades_Marks_MarkId",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.RenameTable(
                name: "Marks",
                schema: "dbo",
                newName: "Feedback",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_UserId",
                schema: "dbo",
                table: "Feedback",
                newName: "IX_Feedback_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Marks_ProjectId",
                schema: "dbo",
                table: "Feedback",
                newName: "IX_Feedback_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_IdUsers_UserId",
                schema: "dbo",
                table: "Feedback",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Projects_ProjectId",
                schema: "dbo",
                table: "Feedback",
                column: "ProjectId",
                principalSchema: "dbo",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Feedback_FeedbackId",
                schema: "dbo",
                table: "Grades",
                column: "FeedbackId",
                principalSchema: "dbo",
                principalTable: "Feedback",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGrades_Feedback_MarkId",
                schema: "dbo",
                table: "UserGrades",
                column: "MarkId",
                principalSchema: "dbo",
                principalTable: "Feedback",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_IdUsers_UserId",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Projects_ProjectId",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Feedback_FeedbackId",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGrades_Feedback_MarkId",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                schema: "dbo",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "Feedback",
                schema: "dbo",
                newName: "Marks",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_UserId",
                schema: "dbo",
                table: "Marks",
                newName: "IX_Marks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_ProjectId",
                schema: "dbo",
                table: "Marks",
                newName: "IX_Marks_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marks",
                schema: "dbo",
                table: "Marks",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Marks_FeedbackId",
                schema: "dbo",
                table: "Grades",
                column: "FeedbackId",
                principalSchema: "dbo",
                principalTable: "Marks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_IdUsers_UserId",
                schema: "dbo",
                table: "Marks",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Projects_ProjectId",
                schema: "dbo",
                table: "Marks",
                column: "ProjectId",
                principalSchema: "dbo",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGrades_Marks_MarkId",
                schema: "dbo",
                table: "UserGrades",
                column: "MarkId",
                principalSchema: "dbo",
                principalTable: "Marks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
