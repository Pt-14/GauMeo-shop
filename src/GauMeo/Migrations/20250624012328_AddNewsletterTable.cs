using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsletterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Newsletters");

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
    }
}
