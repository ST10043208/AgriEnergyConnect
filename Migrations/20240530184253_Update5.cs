using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgriEnergyConnect.Migrations
{
    /// <inheritdoc />
    public partial class Update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farmers_Employees_EmployeeId",
                table: "Farmers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Farmers_FarmerId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FarmerId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Farmers_EmployeeId",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FarmerId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Farmers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProductionDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProductionDate",
                table: "Products",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "FarmerId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Farmers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FarmerId1",
                table: "Products",
                column: "FarmerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Farmers_EmployeeId",
                table: "Farmers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Farmers_Employees_EmployeeId",
                table: "Farmers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Farmers_FarmerId1",
                table: "Products",
                column: "FarmerId1",
                principalTable: "Farmers",
                principalColumn: "FarmerId");
        }
    }
}
