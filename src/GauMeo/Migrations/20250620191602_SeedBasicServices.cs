using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class SeedBasicServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4146), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4146) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4151), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4151) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4154), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4154) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4157), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4158) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4160), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4161) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4163), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4164) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4166), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4167) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4169), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4172), new DateTime(2025, 6, 21, 2, 16, 2, 274, DateTimeKind.Local).AddTicks(4173) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6598), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6598) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6602), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6603) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6606), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6606) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6609), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6613), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6613) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6616), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6617) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6619), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6622), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6623) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6626), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6626) });
        }
    }
}
