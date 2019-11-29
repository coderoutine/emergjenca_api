using Microsoft.EntityFrameworkCore.Migrations;

namespace Emergency.DAL.Migrations
{
    public partial class Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Shelter");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Shelter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "Shelter",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Shelter");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Shelter");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Shelter",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
