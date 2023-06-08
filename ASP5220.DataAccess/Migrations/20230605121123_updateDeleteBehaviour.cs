using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP5220.DataAccess.Migrations
{
    public partial class updateDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Cars_CarId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers",
                column: "UserId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Cars_CarId",
                table: "Files",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Cars_CarId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers",
                column: "UserId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Cars_CarId",
                table: "Files",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
