using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _787DD97A_API.Migrations
{
    public partial class AddApartmentPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "Price",
                table: "Apartments",
                type: "bigint unsigned",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Apartments");
        }
    }
}
