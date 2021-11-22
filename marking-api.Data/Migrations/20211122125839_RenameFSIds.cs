using Microsoft.EntityFrameworkCore.Migrations;

namespace marking_api.Data.Migrations
{
    public partial class RenameFSIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_FSFiles_FileId",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "FileId",
                schema: "dbo",
                table: "Projects",
                newName: "EthicsFormId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_FileId",
                schema: "dbo",
                table: "Projects",
                newName: "IX_Projects_EthicsFormId");

            migrationBuilder.RenameColumn(
                name: "FSFolderId",
                schema: "dbo",
                table: "FSFolders",
                newName: "FolderId");

            migrationBuilder.RenameColumn(
                name: "FSFolderFileId",
                schema: "dbo",
                table: "FSFolderFiles",
                newName: "FolderFileId");

            migrationBuilder.RenameColumn(
                name: "FSFileVersionId",
                schema: "dbo",
                table: "FSFileVersions",
                newName: "FileVersionId");

            migrationBuilder.RenameColumn(
                name: "FSFileId",
                schema: "dbo",
                table: "FSFiles",
                newName: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_FSFiles_EthicsFormId",
                schema: "dbo",
                table: "Projects",
                column: "EthicsFormId",
                principalSchema: "dbo",
                principalTable: "FSFiles",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_FSFiles_EthicsFormId",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "EthicsFormId",
                schema: "dbo",
                table: "Projects",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_EthicsFormId",
                schema: "dbo",
                table: "Projects",
                newName: "IX_Projects_FileId");

            migrationBuilder.RenameColumn(
                name: "FolderId",
                schema: "dbo",
                table: "FSFolders",
                newName: "FSFolderId");

            migrationBuilder.RenameColumn(
                name: "FolderFileId",
                schema: "dbo",
                table: "FSFolderFiles",
                newName: "FSFolderFileId");

            migrationBuilder.RenameColumn(
                name: "FileVersionId",
                schema: "dbo",
                table: "FSFileVersions",
                newName: "FSFileVersionId");

            migrationBuilder.RenameColumn(
                name: "FileId",
                schema: "dbo",
                table: "FSFiles",
                newName: "FSFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_FSFiles_FileId",
                schema: "dbo",
                table: "Projects",
                column: "FileId",
                principalSchema: "dbo",
                principalTable: "FSFiles",
                principalColumn: "FSFileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
