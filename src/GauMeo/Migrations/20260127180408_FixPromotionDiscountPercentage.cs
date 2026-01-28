using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class FixPromotionDiscountPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "Promotions",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Carts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8244), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8245) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8249), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8249) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8253), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8253) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8256), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8257) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8260), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8263), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8263) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8267), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8267) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8270), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8270) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8273), new DateTime(2026, 1, 28, 1, 4, 8, 391, DateTimeKind.Local).AddTicks(8274) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "Promotions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Carts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2767), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2768) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2772), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2772) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2775), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2776) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2778), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2779) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2781), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2782) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2784), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2785) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2787), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2788) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2790), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2791) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2793), new DateTime(2025, 6, 24, 8, 23, 28, 524, DateTimeKind.Local).AddTicks(2794) });
        }
    }
}
