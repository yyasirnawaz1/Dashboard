using Microsoft.EntityFrameworkCore.Migrations;

namespace LTC_Dashboard.Migrations
{
    public partial class addednewcolumnsforadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAssignOfficeEnabled",
                table: "Authentication",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditModuleEnabled",
                table: "Authentication",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditUserEnabled",
                table: "Authentication",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssignOfficeEnabled",
                table: "Authentication");

            migrationBuilder.DropColumn(
                name: "IsEditModuleEnabled",
                table: "Authentication");

            migrationBuilder.DropColumn(
                name: "IsEditUserEnabled",
                table: "Authentication");
        }
    }
}
