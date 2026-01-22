using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class DropAndRecreateServiceBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8088), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8089) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8093), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8093) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8096), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8096) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8099), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8099) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8102), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8102) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8104), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8105) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8107), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8108) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8110), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8111) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8113), new DateTime(2025, 6, 23, 16, 32, 18, 193, DateTimeKind.Local).AddTicks(8113) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2258), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2259) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2262), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2263) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2294), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2294) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2297), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2297) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2299), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2302), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2303) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2305), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2306) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2308), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2308) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2311), new DateTime(2025, 6, 23, 15, 55, 21, 764, DateTimeKind.Local).AddTicks(2311) });
        }
    }
}
