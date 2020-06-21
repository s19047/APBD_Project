using Microsoft.EntityFrameworkCore.Migrations;

namespace Test2.Migrations
{
    public partial class SeededDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "IdBuilding", "City", "Height", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "Warsaw", 10m, "Marii grzegowskiej", 1 },
                    { 2, "Warsaw", 3m, "Hala Banacha", 2 },
                    { 3, "Warsaw", 19m, "Centrum", 3 },
                    { 4, "Warsaw", 9m, "Mokotow", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Building",
                keyColumn: "IdBuilding",
                keyValue: 4);
        }
    }
}
