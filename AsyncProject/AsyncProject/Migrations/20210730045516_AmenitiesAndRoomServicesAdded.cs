using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncProject.Migrations
{
    public partial class AmenitiesAndRoomServicesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "TV" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Layout", "Name" },
                values: new object[] { 1, 2, "Suite 1" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Layout", "Name" },
                values: new object[] { 2, 1, "Suite 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
