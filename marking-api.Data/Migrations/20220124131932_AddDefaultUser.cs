using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marking_api.Data.Migrations
{
    public partial class AddDefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO idusers (UserId, IsDisabled, IsDeleted, ProfilePicture, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, FirstName, LastName)
Values (UUID(), false, false, null, 'sjp75', 'SJP75', 'sjp75@kent.ac.uk', 'SJP75@KENT.AC.UK', true, null, SecurityStamp = UUID(), ConcurrencyStamp = UUID(), null, false, false, false, 0, 'Steve', 'Parkinson');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
