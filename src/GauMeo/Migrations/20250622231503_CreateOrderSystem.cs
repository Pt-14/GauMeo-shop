using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {            migrationBuilder.DropTable(
                name: "ProductQuestions");            // migrationBuilder.DropColumn(
            //     name: "ImageUrl",
            //     table: "Products");

            // migrationBuilder.DropColumn(
            //     name: "Price",
            //     table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "AnimalType",
                table: "Categories",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),                oldType: "decimal(18,2)");

            // migrationBuilder.AddColumn<string>(
            //     name: "ImageUrl",
            //     table: "Products",
            //     type: "nvarchar(500)",
            //     maxLength: 500,
            //     nullable: false,
            //     defaultValue: "");            // migrationBuilder.AddColumn<decimal>(
            //     name: "Price",
            //     table: "Products",
            //     type: "decimal(18,2)",
            //     nullable: false,
            //     defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "Categories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnimalType",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnsweredByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnsweredBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAnswered = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuestions_AspNetUsers_AnsweredByUserId",
                        column: x => x.AnsweredByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQuestions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductQuestions_Products_ProductId",
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
                name: "IX_ProductQuestions_AnsweredByUserId",
                table: "ProductQuestions",
                column: "AnsweredByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestions_ProductId",
                table: "ProductQuestions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestions_UserId",
                table: "ProductQuestions",
                column: "UserId");
        }
    }
}
