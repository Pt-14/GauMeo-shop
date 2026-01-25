using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Products",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "AltText",
                table: "ProductImages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Products",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AltText",
                table: "ProductImages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

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
    }
}
