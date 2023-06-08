using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP5220.DataAccess.Migrations
{
    public partial class cfupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Users_CarId",
                table: "CarFollowers");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Cars_CarId",
                table: "CarFollowers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Users_UserId",
                table: "CarFollowers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Cars_CarId",
                table: "CarFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Users_UserId",
                table: "CarFollowers");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers",
                column: "UserId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Users_CarId",
                table: "CarFollowers",
                column: "CarId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
