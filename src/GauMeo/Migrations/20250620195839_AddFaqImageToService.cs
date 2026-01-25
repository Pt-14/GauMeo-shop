using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class AddFaqImageToService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaqImage",
                table: "Services",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3309), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3310) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3314), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3314) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3318), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3318) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3321), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3322) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3325), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3325) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3328), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3329) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3332), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3332) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3335), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3335) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3338), new DateTime(2025, 6, 21, 2, 58, 39, 13, DateTimeKind.Local).AddTicks(3339) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaqImage",
                table: "Services");

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
    }
}
