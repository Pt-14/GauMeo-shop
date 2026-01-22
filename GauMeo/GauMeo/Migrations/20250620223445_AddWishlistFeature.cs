using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class AddWishlistFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1885), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1885) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1889), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1893), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1893) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1896), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1897) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1900), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1900) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1903), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1903) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1906), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1907) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1909), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1909) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1912), new DateTime(2025, 6, 21, 5, 34, 44, 655, DateTimeKind.Local).AddTicks(1912) });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId_ProductId",
                table: "WishlistItems",
                columns: new[] { "UserId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2833), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2833) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2837), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2838) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2840), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2841) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2843), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2844) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2847), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2847) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2849), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2852), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2853) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2855), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2856) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2858), new DateTime(2025, 6, 21, 3, 34, 2, 784, DateTimeKind.Local).AddTicks(2859) });
        }
    }
}
