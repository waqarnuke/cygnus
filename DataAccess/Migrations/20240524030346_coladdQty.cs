using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class coladdQty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "New");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sale");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Disposables");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 4, 4, null, "E-Liquids" },
                    { 5, 5, null, "Vape kits" },
                    { 6, 6, null, "Accessories" },
                    { 7, 6, null, "Multifarious" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Quantity", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1019), 0, new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1022) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "Quantity", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1032), 0, new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1034) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "Quantity", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1042), 0, new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1043) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "Quantity", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1050), 0, new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1052) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "Quantity", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1059), 0, new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1060) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "Quantity", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1068), 0, new DateTime(2024, 5, 23, 22, 3, 46, 211, DateTimeKind.Local).AddTicks(1069) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Action");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "SciFi");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "History");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4706), new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4707) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4713), new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4714) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4719), new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4725), new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4726) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4732), new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4733) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreateDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4738), new DateTime(2024, 5, 8, 19, 10, 15, 539, DateTimeKind.Local).AddTicks(4739) });
        }
    }
}
