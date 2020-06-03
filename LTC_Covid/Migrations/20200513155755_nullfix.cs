using Microsoft.EntityFrameworkCore.Migrations;

namespace LTC_Dashboard.Migrations
{
    public partial class nullfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cust_id",
                table: "Authentication",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cust_id",
                table: "Authentication",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
