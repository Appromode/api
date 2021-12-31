using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class FixShadowProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FSFolderRoles_IdRoles_RoleId1",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_IdUsers_UserId1",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMarkers_IdUsers_UserId1",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropForeignKey(
                name: "FK_IdRolePermission_IdRoles_RoleId1",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGrades_IdUsers_UserId1",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_IdUsers_UserId1",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_UserId1",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGrades_UserId1",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropIndex(
                name: "IX_IdRolePermission_RoleId1",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropIndex(
                name: "IX_GroupMarkers_UserId1",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserId1",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_FSFolderRoles_RoleId1",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "UserGroups",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "UserGrades",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "dbo",
                table: "IdRolePermission",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "GroupMarkers",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Grades",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId",
                schema: "dbo",
                table: "UserGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGrades_UserId",
                schema: "dbo",
                table: "UserGrades",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdRolePermission_RoleId",
                schema: "dbo",
                table: "IdRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMarkers_UserId",
                schema: "dbo",
                table: "GroupMarkers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId",
                schema: "dbo",
                table: "Grades",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FSFolderRoles_RoleId",
                schema: "dbo",
                table: "FSFolderRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FSFolderRoles_IdRoles_RoleId",
                schema: "dbo",
                table: "FSFolderRoles",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "IdRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_IdUsers_UserId",
                schema: "dbo",
                table: "Grades",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMarkers_IdUsers_UserId",
                schema: "dbo",
                table: "GroupMarkers",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdRolePermission_IdRoles_RoleId",
                schema: "dbo",
                table: "IdRolePermission",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "IdRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGrades_IdUsers_UserId",
                schema: "dbo",
                table: "UserGrades",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_IdUsers_UserId",
                schema: "dbo",
                table: "UserGroups",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FSFolderRoles_IdRoles_RoleId",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_IdUsers_UserId",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMarkers_IdUsers_UserId",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropForeignKey(
                name: "FK_IdRolePermission_IdRoles_RoleId",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGrades_IdUsers_UserId",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_IdUsers_UserId",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_UserId",
                schema: "dbo",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGrades_UserId",
                schema: "dbo",
                table: "UserGrades");

            migrationBuilder.DropIndex(
                name: "IX_IdRolePermission_RoleId",
                schema: "dbo",
                table: "IdRolePermission");

            migrationBuilder.DropIndex(
                name: "IX_GroupMarkers_UserId",
                schema: "dbo",
                table: "GroupMarkers");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserId",
                schema: "dbo",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_FSFolderRoles_RoleId",
                schema: "dbo",
                table: "FSFolderRoles");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                schema: "dbo",
                table: "UserGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                schema: "dbo",
                table: "UserGroups",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                schema: "dbo",
                table: "UserGrades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                schema: "dbo",
                table: "UserGrades",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                schema: "dbo",
                table: "IdRolePermission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                schema: "dbo",
                table: "IdRolePermission",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                schema: "dbo",
                table: "GroupMarkers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                schema: "dbo",
                table: "GroupMarkers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                schema: "dbo",
                table: "Grades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                schema: "dbo",
                table: "Grades",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                schema: "dbo",
                table: "FSFolderRoles",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId1",
                schema: "dbo",
                table: "UserGroups",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserGrades_UserId1",
                schema: "dbo",
                table: "UserGrades",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_IdRolePermission_RoleId1",
                schema: "dbo",
                table: "IdRolePermission",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMarkers_UserId1",
                schema: "dbo",
                table: "GroupMarkers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId1",
                schema: "dbo",
                table: "Grades",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_FSFolderRoles_RoleId1",
                schema: "dbo",
                table: "FSFolderRoles",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FSFolderRoles_IdRoles_RoleId1",
                schema: "dbo",
                table: "FSFolderRoles",
                column: "RoleId1",
                principalSchema: "dbo",
                principalTable: "IdRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_IdUsers_UserId1",
                schema: "dbo",
                table: "Grades",
                column: "UserId1",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMarkers_IdUsers_UserId1",
                schema: "dbo",
                table: "GroupMarkers",
                column: "UserId1",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdRolePermission_IdRoles_RoleId1",
                schema: "dbo",
                table: "IdRolePermission",
                column: "RoleId1",
                principalSchema: "dbo",
                principalTable: "IdRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGrades_IdUsers_UserId1",
                schema: "dbo",
                table: "UserGrades",
                column: "UserId1",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_IdUsers_UserId1",
                schema: "dbo",
                table: "UserGroups",
                column: "UserId1",
                principalSchema: "dbo",
                principalTable: "IdUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
