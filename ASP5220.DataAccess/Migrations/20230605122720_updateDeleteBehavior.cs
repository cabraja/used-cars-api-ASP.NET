using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP5220.DataAccess.Migrations
{
    public partial class updateDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationCar_Cars_CarId",
                table: "SpecificationCar");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationCar_Cars_CarId",
                table: "SpecificationCar",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationCar_Cars_CarId",
                table: "SpecificationCar");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationCar_Cars_CarId",
                table: "SpecificationCar",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
