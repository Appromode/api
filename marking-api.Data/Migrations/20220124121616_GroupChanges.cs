using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class GroupChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                schema: "dbo",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                schema: "dbo",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Groups",
                keyColumn: "GroupName",
                keyValue: null,
                column: "GroupName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                schema: "dbo",
                table: "Groups",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_GroupId",
                schema: "dbo",
                table: "Tags",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProjectId",
                schema: "dbo",
                table: "Tags",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupName",
                schema: "dbo",
                table: "Groups",
                column: "GroupName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Groups_GroupId",
                schema: "dbo",
                table: "Tags",
                column: "GroupId",
                principalSchema: "dbo",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Projects_ProjectId",
                schema: "dbo",
                table: "Tags",
                column: "ProjectId",
                principalSchema: "dbo",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Groups_GroupId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Projects_ProjectId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_GroupId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProjectId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupName",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "dbo",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                schema: "dbo",
                table: "Groups",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
