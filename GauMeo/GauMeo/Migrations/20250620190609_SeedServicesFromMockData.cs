using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GauMeo.Migrations
{
    /// <inheritdoc />
    public partial class SeedServicesFromMockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed Services từ mock data
            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "ShortName", "Description", "FullDescription", "Features", "Price", "Duration", "Image", "IsActive", "IsFeatured", "DisplayOrder", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    {
                        1,
                        "Pet Spa & Grooming",
                        "SPA & GROOMING",
                        "Spa & Cắt tỉa lông chuyên nghiệp",
                        "Dịch vụ Spa và cắt tỉa lông cao cấp cho thú cưng của bạn. Bao gồm tắm rửa, massage, chăm sóc lông da, cắt tỉa lông theo yêu cầu và các liệu pháp thư giãn đặc biệt. Chúng tôi sử dụng các sản phẩm cao cấp và kỹ thuật chuyên nghiệp để đảm bảo thú cưng của bạn được chăm sóc tốt nhất.",
                        "[\"Tắm rửa với dầu gội cao cấp chuyên dụng\",\"Massage thư giãn chuyên nghiệp\",\"Chăm sóc lông và da toàn diện\",\"Cắt tỉa lông theo yêu cầu và giống loài\",\"Tạo kiểu lông đẹp mắt và thời trang\",\"Cắt móng chân an toàn và chính xác\",\"Vệ sinh tai và mắt kỹ lưỡng\",\"Xịt nước hoa thú cưng thơm mát\",\"Chăm sóc sau spa và grooming\"]",
                        "180.000 - 600.000 VNĐ", 
                        "75 - 150 phút",
                        "/images/servicepic/spa1.jpg",
                        true,
                        true,
                        1,
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local),
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local)
                    },
                    {
                        2,
                        "Pet Hotel",
                        "HOTEL",
                        "Khách sạn thú cưng cao cấp",
                        "Pet's Hotel cung cấp dịch vụ lưu trú an toàn và tiện nghi cho thú cưng của bạn. Với không gian rộng rãi, sạch sẽ và đội ngũ chăm sóc chuyên nghiệp 24/7, chúng tôi đảm bảo thú cưng của bạn sẽ có một kỳ nghỉ thoải mái và an toàn.",
                        "[\"Phòng riêng biệt, sạch sẽ và thoáng mát\",\"Chế độ ăn uống đầy đủ và cân bằng dinh dưỡng\",\"Vận động và vui chơi hàng ngày\",\"Giám sát 24/7 bởi đội ngũ chuyên nghiệp\",\"Chăm sóc y tế khi cần thiết\",\"Báo cáo tình trạng hàng ngày cho chủ\",\"Không gian riêng cho chó và mèo\",\"Hệ thống camera giám sát\",\"Dịch vụ đưa đón tận nhà\"]",
                        "100.000 - 450.000 VNĐ",
                        "1 ngày", 
                        "/images/servicepic/hotel1.jpg",
                        true,
                        true,
                        2,
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local),
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local)
                    },
                    {
                        3,
                        "Pet Swimming",
                        "SWIMMING",
                        "Hồ bơi dành riêng cho chó",
                        "Hồ bơi sạch sẽ, an toàn dành riêng cho chó. Giúp chó vận động, giải nhiệt và rèn luyện sức khỏe trong môi trường nước. Chúng tôi có đội ngũ giám sát chuyên nghiệp và các thiết bị an toàn đầy đủ.",
                        "[\"Hồ bơi sạch sẽ, an toàn với nước được lọc thường xuyên\",\"Nhiệt độ nước phù hợp cho chó\",\"Giám sát chuyên nghiệp 24/7\",\"Dụng cụ bơi lội và đồ chơi dưới nước\",\"Vệ sinh sau khi bơi với dầu gội chuyên dụng\",\"Khăn tắm và sấy khô\",\"Huấn luyện bơi lội cơ bản\",\"Thiết bị an toàn đầy đủ\",\"Khu vực nghỉ ngơi sau bơi\"]",
                        "100.000 - 280.000 VNĐ",
                        "30 - 75 phút",
                        "/images/servicepic/swimming1.jpg",
                        true,
                        true,
                        3,
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local),
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local)
                    },
                    {
                        4,
                        "Pet Daycare",
                        "DAYCARE",
                        "Trông giữ thú cưng theo ngày",
                        "Dịch vụ trông giữ thú cưng theo ngày với môi trường vui chơi, học tập và giao lưu. Thú cưng của bạn sẽ được chăm sóc, vui chơi cùng bạn bè và phát triển kỹ năng xã hội trong môi trường an toàn, chuyên nghiệp.",
                        "[\"Trông giữ theo giờ hoặc cả ngày\",\"Hoạt động vui chơi đa dạng\",\"Giao lưu với thú cưng khác\",\"Huấn luyện kỹ năng cơ bản\",\"Chăm sóc ăn uống đầy đủ\",\"Báo cáo hoạt động hàng ngày\",\"Khu vực riêng cho chó và mèo\",\"Giám sát 24/7\",\"Hoạt động ngoài trời an toàn\"]",
                        "80.000 - 280.000 VNĐ",
                        "4 - 8 giờ",
                        "/images/servicepic/daycare1.jpg",
                        true,
                        true,
                        4,
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local),
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local)
                    },
                    {
                        5,
                        "Pet Training",
                        "TRAINING",
                        "Huấn luyện thú cưng chuyên nghiệp",
                        "Dịch vụ huấn luyện thú cưng chuyên nghiệp với các khóa học từ cơ bản đến nâng cao. Giúp thú cưng của bạn học các kỹ năng cần thiết, cải thiện hành vi và tăng cường mối quan hệ giữa chủ và thú cưng. Chúng tôi cung cấp huấn luyện cho cả chó và mèo với phương pháp phù hợp cho từng loài.",
                        "[\"Huấn luyện vâng lời cơ bản\",\"Sửa chữa hành vi xấu\",\"Kỹ năng xã hội với người và thú cưng khác\",\"Huấn luyện kỹ năng đặc biệt\",\"Tư vấn chăm sóc và giáo dục\",\"Khóa học nhóm và cá nhân\",\"Huấn luyện thể thao cho chó\",\"Huấn luyện thông minh cho mèo\",\"Hướng dẫn chủ cách huấn luyện\",\"Theo dỗi tiến độ dài hạn\"]",
                        "300.000 - 1.400.000 VNĐ",
                        "4 - 8 buổi",
                        "/images/servicepic/training1.jpg",
                        true,
                        true,
                        5,
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local),
                        new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local)
                    }
                });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6598), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6598) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6602), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6603) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6606), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6606) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6609), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6613), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6613) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6616), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6617) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6619), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6622), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6623) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6626), new DateTime(2025, 6, 21, 2, 6, 8, 745, DateTimeKind.Local).AddTicks(6626) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(275), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(275) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(279), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(283), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(284) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(287), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(287) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(290), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(290) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(293), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(293) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(296), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(297) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(299), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(302), new DateTime(2025, 6, 21, 1, 58, 31, 598, DateTimeKind.Local).AddTicks(303) });
        }
    }
}
