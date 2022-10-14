using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restuarantmanagmentsystem.Migrations
{
    public partial class revenueupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyTotal",
                table: "Revenue");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Revenue",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 1,
                column: "CategoryType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 2,
                column: "CategoryType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 3,
                column: "CategoryType",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Revenue");

            migrationBuilder.AddColumn<float>(
                name: "MonthlyTotal",
                table: "Revenue",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 1,
                column: "CategoryType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 2,
                column: "CategoryType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 3,
                column: "CategoryType",
                value: 0);
        }
    }
}
