using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class UpdateThreadCommentDMs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThreadDesc",
                schema: "dbo",
                table: "Threads",
                newName: "ThreadContent");

            migrationBuilder.RenameColumn(
                name: "Replies",
                schema: "dbo",
                table: "Threads",
                newName: "ReplyCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThreadContent",
                schema: "dbo",
                table: "Threads",
                newName: "ThreadDesc");

            migrationBuilder.RenameColumn(
                name: "ReplyCount",
                schema: "dbo",
                table: "Threads",
                newName: "Replies");
        }
    }
}
