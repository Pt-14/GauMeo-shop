using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7357), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7358) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7361), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7362) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7365), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7365) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7368), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7368) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7370), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7371) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7373), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7374) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7376), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7377) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7379), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7382), new DateTime(2025, 6, 24, 4, 59, 53, 14, DateTimeKind.Local).AddTicks(7383) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5671), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5672) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5682), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5683) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5690), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5691) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5698), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5699) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5706), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5707) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5713), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5714) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5721), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5722) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5729), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5736), new DateTime(2025, 6, 24, 4, 55, 13, 110, DateTimeKind.Local).AddTicks(5737) });
        }
    }
}
