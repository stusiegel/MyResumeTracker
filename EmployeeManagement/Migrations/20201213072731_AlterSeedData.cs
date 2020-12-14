using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class AlterSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Coomments", "Department", "Email", "Name" },
                values: new object[,]
                {
                    { 2, null, 3, "stu.siegel@yahoo.com", "Stu" },
                    { 3, null, 1, "MaryD@gmail.com", "Mary Duckworth" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Comments", "CompanyName", "Description", "Rating", "Title" },
                values: new object[,]
                {
                    { 2, "Java's a job ", "USAA", "Use Java", 2, "Sr Engineer" },
                    { 3, "Who doesn't love golf", "PGA", "Leaving the dream", 4, "Golf Pro" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
