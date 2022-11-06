using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _787DD97A_API.Migrations
{
    public partial class DeviceIdNotUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDevices_DeviceId",
                table: "UserDevices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserDevices_DeviceId",
                table: "UserDevices",
                column: "DeviceId",
                unique: true);
        }
    }
}
