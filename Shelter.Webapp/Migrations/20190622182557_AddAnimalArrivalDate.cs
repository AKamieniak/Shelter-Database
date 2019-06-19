using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shelter.Webapp.Migrations
{
    public partial class AddAnimalArrivalDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfArrival",
                schema: "sh",
                table: "Animals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfArrival",
                schema: "sh",
                table: "Animals");
        }
    }
}
