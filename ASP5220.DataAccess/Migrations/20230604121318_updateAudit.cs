using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP5220.DataAccess.Migrations
{
    public partial class updateAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "AuditLogs",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AuditLogs",
                newName: "User");
        }
    }
}
