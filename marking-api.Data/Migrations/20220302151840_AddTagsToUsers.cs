using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class AddTagsToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Tags",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId",
                schema: "dbo",
                table: "Tags",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_IdUsers_UserId",
                schema: "dbo",
                table: "Tags",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_IdUsers_UserId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_UserId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Tags");
        }
    }
}
