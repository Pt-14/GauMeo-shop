using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class SeedBrandData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Promotions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Promotions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "Features", "Founded", "FullDescription", "Image", "IsActive", "IsFeatured", "Name", "Origin", "ShortName", "UpdatedAt", "Website" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8696), "Thương hiệu thức ăn cao cấp cho chó mèo", 1, "[\"Thức ăn chuyên biệt theo giống loài\",\"Công thức dinh dưỡng khoa học\",\"Hỗ trợ sức khỏe toàn diện\",\"Sản phẩm cho mọi lứa tuổi\",\"Chất lượng được kiểm định nghiêm ngặt\",\"Nghiên cứu bởi đội ngũ chuyên gia\",\"Đa dạng sản phẩm\",\"Được tin dùng toàn cầu\"]", "1968", "Royal Canin là thương hiệu thức ăn cao cấp hàng đầu thế giới, chuyên nghiên cứu và phát triển các sản phẩm dinh dưỡng tối ưu cho từng giống chó mèo cụ thể. Với hơn 50 năm kinh nghiệm, Royal Canin cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng của bạn.", "/images/brands/1.png", true, true, "Royal Canin", "Pháp", "RC", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8697), "www.royalcanin.com" },
                    { 2, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8701), "Thức ăn cho chó chất lượng cao", 2, "[\"Thức ăn cân bằng dinh dưỡng\",\"Hỗ trợ sức khỏe răng miệng\",\"Tăng cường hệ miễn dịch\",\"Sản phẩm cho mọi kích thước chó\",\"Giá cả hợp lý\",\"Chất lượng ổn định\",\"Dễ tiêu hóa\",\"Hương vị hấp dẫn\"]", "1935", "Pedigree là thương hiệu thức ăn cho chó nổi tiếng với chất lượng cao và giá cả hợp lý. Với hơn 80 năm kinh nghiệm, Pedigree đã trở thành lựa chọn tin cậy của hàng triệu chủ nuôi trên toàn thế giới.", "/images/brands/2.jpg", true, true, "Pedigree", "Mỹ", "PD", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8701), "www.pedigree.com" },
                    { 3, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8704), "Thương hiệu thức ăn đa dạng", 3, "[\"Đa dạng sản phẩm\",\"Chất lượng cao\",\"Giá cả cạnh tranh\",\"Hỗ trợ sức khỏe toàn diện\",\"Công thức dinh dưỡng khoa học\",\"Sản phẩm cho mọi lứa tuổi\",\"Dễ tiêu hóa\",\"Được tin dùng rộng rãi\"]", "1894", "Purina là thương hiệu thức ăn thú cưng đa dạng với nhiều dòng sản phẩm khác nhau. Từ thức ăn cao cấp đến bình dân, Purina đáp ứng mọi nhu cầu của thú cưng và chủ nuôi.", "/images/brands/3.png", true, true, "Purina", "Mỹ", "PN", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8704), "www.purina.com" },
                    { 4, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8707), "Thức ăn cho mèo nổi tiếng", 4, "[\"Thức ăn chuyên biệt cho mèo\",\"Hương vị hấp dẫn\",\"Dinh dưỡng cân bằng\",\"Hỗ trợ sức khỏe mèo\",\"Sản phẩm đa dạng\",\"Chất lượng ổn định\",\"Dễ tiêu hóa\",\"Được mèo yêu thích\"]", "1958", "Whiskas là thương hiệu thức ăn cho mèo được yêu thích trên toàn thế giới. Với hơn 50 năm kinh nghiệm, Whiskas hiểu rõ nhu cầu dinh dưỡng của mèo và mang đến những sản phẩm chất lượng cao.", "/images/brands/4.png", true, true, "Whiskas", "Anh", "WK", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8707), "www.whiskas.com" },
                    { 5, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8709), "Thức ăn thú y chuyên nghiệp", 5, "[\"Công thức thú y chuyên nghiệp\",\"Hỗ trợ điều trị bệnh\",\"Dinh dưỡng khoa học\",\"Được bác sĩ thú y khuyến nghị\",\"Sản phẩm chuyên biệt\",\"Chất lượng cao cấp\",\"An toàn cho thú cưng\",\"Hiệu quả đã được chứng minh\"]", "1939", "Hill's Science Diet là thương hiệu thức ăn thú y chuyên nghiệp, được phát triển bởi các bác sĩ thú y. Sản phẩm của Hill's được thiết kế để hỗ trợ điều trị và phòng ngừa các vấn đề sức khỏe của thú cưng.", "/images/brands/5.png", true, true, "Hill's", "Mỹ", "HS", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8710), "www.hills.com" },
                    { 6, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8712), "Thức ăn tự nhiên cao cấp", 6, "[\"Nguyên liệu tự nhiên\",\"Không chứa chất bảo quản nhân tạo\",\"Dinh dưỡng tối ưu\",\"Hỗ trợ tiêu hóa\",\"Tăng cường năng lượng\",\"Sản phẩm cao cấp\",\"An toàn tuyệt đối\",\"Hiệu quả lâu dài\"]", "1986", "Nutri-Source là thương hiệu thức ăn tự nhiên cao cấp, tập trung vào việc sử dụng các nguyên liệu tự nhiên và công thức dinh dưỡng tối ưu. Sản phẩm của Nutri-Source giúp thú cưng khỏe mạnh và tràn đầy năng lượng.", "/images/brands/6.jpg", true, false, "Nutri-Source", "Mỹ", "NS", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8713), "www.nutri-source.com" },
                    { 7, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8715), "Thức ăn sinh học tự nhiên", 7, "[\"Nguyên liệu tươi ngon\",\"Công thức sinh học\",\"Không chứa ngũ cốc\",\"Protein cao cấp\",\"Dinh dưỡng tự nhiên\",\"Hỗ trợ sức khỏe toàn diện\",\"Sản phẩm cao cấp\",\"Được tin dùng toàn cầu\"]", "1975", "Acana là thương hiệu thức ăn sinh học tự nhiên, được sản xuất theo phương pháp truyền thống với các nguyên liệu tươi ngon. Acana cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng.", "/images/brands/acana.jpg", true, false, "Acana", "Canada", "AC", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8715), "www.acana.com" },
                    { 8, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8718), "Thức ăn hoang dã tự nhiên", 8, "[\"Công thức hoang dã tự nhiên\",\"Nguyên liệu tươi sống\",\"Protein động vật cao\",\"Không chứa ngũ cốc\",\"Dinh dưỡng tự nhiên\",\"Hỗ trợ phát triển tự nhiên\",\"Sản phẩm cao cấp\",\"Được chứng minh hiệu quả\"]", "1985", "Orijen là thương hiệu thức ăn hoang dã tự nhiên, được thiết kế để mô phỏng chế độ ăn tự nhiên của thú cưng trong môi trường hoang dã. Sản phẩm của Orijen giúp thú cưng phát triển khỏe mạnh và tự nhiên.", "/images/brands/orijen.jpg", true, false, "Orijen", "Canada", "OR", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8718), "www.orijen.com" },
                    { 9, new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8721), "Thức ăn hữu cơ tự nhiên", 9, "[\"Nguyên liệu hữu cơ\",\"Thân thiện môi trường\",\"Dinh dưỡng tự nhiên\",\"Không chứa hóa chất\",\"Hỗ trợ sức khỏe toàn diện\",\"Sản phẩm chất lượng cao\",\"An toàn tuyệt đối\",\"Bảo vệ môi trường\"]", "2003", "Natural Core là thương hiệu thức ăn hữu cơ tự nhiên, tập trung vào việc sử dụng các nguyên liệu hữu cơ và phương pháp sản xuất thân thiện với môi trường. Sản phẩm của Natural Core tốt cho thú cưng và môi trường.", "/images/brands/natural-core.jpg", true, false, "Natural Core", "Hàn Quốc", "NC", new DateTime(2025, 6, 19, 9, 24, 45, 389, DateTimeKind.Local).AddTicks(8721), "www.naturalcore.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Promotions");
        }
    }
}
