using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceImages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ServiceImages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ServiceAddons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAddons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAddons_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceFAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceFAQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceFAQs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NoteType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceNotes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8103), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8104) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8108), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8108) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8111), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8112) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8115), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8115) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8118), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8119) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8121), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8122) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8124), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8125) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8128), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8128) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8131), new DateTime(2025, 6, 21, 1, 28, 5, 112, DateTimeKind.Local).AddTicks(8131) });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAddons_ServiceId",
                table: "ServiceAddons",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFAQs_ServiceId",
                table: "ServiceFAQs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceNotes_ServiceId",
                table: "ServiceNotes",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceAddons");

            migrationBuilder.DropTable(
                name: "ServiceFAQs");

            migrationBuilder.DropTable(
                name: "ServiceNotes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ServiceImages");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8696), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8697) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8701), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8701) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8704), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8704) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8707), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8707) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8709), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8712), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8713) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8715), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8715) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8718), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8718) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8721), new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8721) });
        }
    }
}
