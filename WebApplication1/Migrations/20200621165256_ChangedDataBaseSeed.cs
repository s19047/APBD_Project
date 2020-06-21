using Microsoft.EntityFrameworkCore.Migrations;

namespace Test2.Migrations
{
    public partial class ChangedDataBaseSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 2,
                column: "Street",
                value: "Marii grzegowskiej");

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 3,
                column: "Street",
                value: "Marii grzegowskiej");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 2,
                column: "Street",
                value: "Hala Banacha");

            migrationBuilder.UpdateData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 3,
                column: "Street",
                value: "Centrum");
        }
    }
}
