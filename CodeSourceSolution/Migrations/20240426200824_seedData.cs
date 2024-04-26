using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeSourceSolution.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Formats",
                columns: new[] { "FormatId", "FormatName" },
                values: new object[] { 1, "ODI" });

            migrationBuilder.InsertData(
                table: "Formats",
                columns: new[] { "FormatId", "FormatName" },
                values: new object[] { 2, "Test" });

            migrationBuilder.InsertData(
                table: "Formats",
                columns: new[] { "FormatId", "FormatName" },
                values: new object[] { 3, "T20" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Formats",
                keyColumn: "FormatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Formats",
                keyColumn: "FormatId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Formats",
                keyColumn: "FormatId",
                keyValue: 3);
        }
    }
}
