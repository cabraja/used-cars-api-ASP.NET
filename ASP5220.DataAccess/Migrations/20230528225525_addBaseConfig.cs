using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP5220.DataAccess.Migrations
{
    public partial class addBaseConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarUser_Cars_UserId",
                table: "CarUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CarUser_Users_CarId",
                table: "CarUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarUser",
                table: "CarUser");

            migrationBuilder.RenameTable(
                name: "CarUser",
                newName: "CarFollowers");

            migrationBuilder.RenameIndex(
                name: "IX_CarUser_UserId",
                table: "CarFollowers",
                newName: "IX_CarFollowers_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFollowers",
                table: "CarFollowers",
                columns: new[] { "CarId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers",
                column: "UserId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFollowers_Users_CarId",
                table: "CarFollowers",
                column: "CarId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Cars_UserId",
                table: "CarFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFollowers_Users_CarId",
                table: "CarFollowers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFollowers",
                table: "CarFollowers");

            migrationBuilder.RenameTable(
                name: "CarFollowers",
                newName: "CarUser");

            migrationBuilder.RenameIndex(
                name: "IX_CarFollowers_UserId",
                table: "CarUser",
                newName: "IX_CarUser_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarUser",
                table: "CarUser",
                columns: new[] { "CarId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarUser_Cars_UserId",
                table: "CarUser",
                column: "UserId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarUser_Users_CarId",
                table: "CarUser",
                column: "CarId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
