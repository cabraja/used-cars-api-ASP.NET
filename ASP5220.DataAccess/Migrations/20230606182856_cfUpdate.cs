using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP5220.DataAccess.Migrations
{
    public partial class cfUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Cars_CarId",
                table: "CarFollowers");

            migrationBuilder.CreateIndex(
                name: "IX_CarFollowers_UserId",
                table: "CarFollowers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers",
                column: "UserId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers");

            migrationBuilder.DropIndex(
                name: "IX_CarFollowers_UserId",
                table: "CarFollowers");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Cars_CarId",
                table: "CarFollowers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
