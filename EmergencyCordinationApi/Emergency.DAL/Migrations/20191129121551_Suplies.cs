using Microsoft.EntityFrameworkCore.Migrations;

namespace Emergency.DAL.Migrations
{
    public partial class Suplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Supplies");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Supplies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Supplies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Supplies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "Supplies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Supplies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Supplies");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Supplies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
