using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class SeedCorrectServiceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6809), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6810) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6813), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6814) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6817), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6817) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6820), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6820) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6823), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6823) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6826), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6829), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6829) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6832), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6832) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6835), new DateTime(2025, 6, 21, 2, 26, 59, 582, DateTimeKind.Local).AddTicks(6835) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
