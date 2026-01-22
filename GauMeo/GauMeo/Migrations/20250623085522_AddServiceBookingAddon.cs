using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceBookingAddon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequests",
                table: "ServiceBookings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "PetSize",
                table: "ServiceBookings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PetBreed",
                table: "ServiceBookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ServiceBookings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerEmail",
                table: "ServiceBookings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "ServiceBookingAddons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceBookingId = table.Column<int>(type: "int", nullable: false),
                    ServiceAddonId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceBookingAddons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceBookingAddons_ServiceAddons_ServiceAddonId",
                        column: x => x.ServiceAddonId,
                        principalTable: "ServiceAddons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceBookingAddons_ServiceBookings_ServiceBookingId",
                        column: x => x.ServiceBookingId,
                        principalTable: "ServiceBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBookingAddons_ServiceAddonId",
                table: "ServiceBookingAddons",
                column: "ServiceAddonId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBookingAddons_ServiceBookingId",
                table: "ServiceBookingAddons",
                column: "ServiceBookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceBookingAddons");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialRequests",
                table: "ServiceBookings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PetSize",
                table: "ServiceBookings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PetBreed",
                table: "ServiceBookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ServiceBookings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerEmail",
                table: "ServiceBookings",
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
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1593), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1593) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1597), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1598) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1601), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1601) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1604), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1604) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1607), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1608) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1610), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1611) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1613), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1614) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1616), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1617) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1619), new DateTime(2025, 6, 23, 6, 15, 3, 508, DateTimeKind.Local).AddTicks(1620) });
        }
    }
}
