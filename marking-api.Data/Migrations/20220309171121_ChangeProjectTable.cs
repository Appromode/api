using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class ChangeProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LinkedThreadId",
                schema: "dbo",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LinkedThreadId",
                schema: "dbo",
                table: "Projects",
                column: "LinkedThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Threads_LinkedThreadId",
                schema: "dbo",
                table: "Projects",
                column: "LinkedThreadId",
                principalSchema: "dbo",
                principalTable: "Threads",
                principalColumn: "ThreadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Threads_LinkedThreadId",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_LinkedThreadId",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LinkedThreadId",
                schema: "dbo",
                table: "Projects");
        }
    }
}
