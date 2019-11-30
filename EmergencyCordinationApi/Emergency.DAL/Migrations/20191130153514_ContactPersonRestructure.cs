using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Emergency.DAL.Migrations
{
    public partial class ContactPersonRestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact Person_Shelter",
                table: "Contact Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Contact Person",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "IX_Supplies_ContactPersonId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "ContactPersonId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Supplies");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShelterId",
                table: "Contact Person",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplieId",
                table: "Contact Person",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact Person_SupplieId",
                table: "Contact Person",
                column: "SupplieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact Person_Shelter",
                table: "Contact Person",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact Person_Supplie",
                table: "Contact Person",
                column: "SupplieId",
                principalTable: "Supplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact Person_Shelter",
                table: "Contact Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact Person_Supplie",
                table: "Contact Person");

            migrationBuilder.DropIndex(
                name: "IX_Contact Person_SupplieId",
                table: "Contact Person");

            migrationBuilder.DropColumn(
                name: "SupplieId",
                table: "Contact Person");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactPersonId",
                table: "Supplies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Supplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ShelterId",
                table: "Contact Person",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_ContactPersonId",
                table: "Supplies",
                column: "ContactPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact Person_Shelter",
                table: "Contact Person",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Contact Person",
                table: "Supplies",
                column: "ContactPersonId",
                principalTable: "Contact Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
