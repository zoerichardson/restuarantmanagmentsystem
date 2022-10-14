using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restuarantmanagmentsystem.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Staff_StaffId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Table_TableId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Order",
                newName: "StaffID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StaffId",
                table: "Order",
                newName: "IX_Order_StaffID");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "ID", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "CheeseBurger", 8f },
                    { 2, "Pizza", 6f },
                    { 3, "Ice Cream", 3f }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Name", "Passcode" },
                values: new object[,]
                {
                    { 1, "George", 6060 },
                    { 2, "Lisa", 9000 },
                    { 3, "Tom", 2222 }
                });

            migrationBuilder.InsertData(
                table: "Table",
                columns: new[] { "Id", "IsAvailable", "StaffId", "TableNumber" },
                values: new object[,]
                {
                    { 1, true, null, 100 },
                    { 2, true, null, 101 },
                    { 3, true, null, 103 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Staff_StaffID",
                table: "Order",
                column: "StaffID",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Table_TableId",
                table: "Order",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Staff_StaffID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Table_TableId",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Table",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Table",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Table",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "StaffID",
                table: "Order",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StaffID",
                table: "Order",
                newName: "IX_Order_StaffId");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Staff_StaffId",
                table: "Order",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Table_TableId",
                table: "Order",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
