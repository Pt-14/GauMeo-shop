using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(275), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(275) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(279), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(283), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(284) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(287), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(287) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(290), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(290) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(293), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(293) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(296), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(297) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(299), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(302), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(303) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1445), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1445) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1449), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1453), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1453) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1456), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1456) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1459), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1462), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1462) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1465), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1465) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1468), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1468) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1471), new DateTime(2025, 6, 21, 1, 58, 5, 404, DateTimeKind.Local).AddTicks(1471) });
        }
    }
}
