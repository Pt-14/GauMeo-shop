using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class MakeSpecialRequestsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8794), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8795) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8799), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8799) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8802), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8803) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8806), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8806) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8809), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8810) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8812), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8813) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8816), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8819), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8819) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8822), new DateTime(2025, 6, 24, 4, 28, 17, 518, DateTimeKind.Local).AddTicks(8822) });
        }
    }
}
