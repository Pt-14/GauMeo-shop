using Microsoft.AspNetCore.Mvc;

namespace GauMeo.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            // Dữ liệu mẫu cho các dịch vụ
            var services = new List<dynamic>
            {
                new { 
                    Id = 1, 
                    Name = "Pet's Spa", 
                    ShortName = "Spa",
                    Description = "Dịch vụ Spa cao cấp cho thú cưng",
                    FullDescription = "Pet's Spa cung cấp dịch vụ chăm sóc toàn diện cho thú cưng của bạn. Bao gồm tắm rửa, massage, chăm sóc lông da, và các liệu pháp thư giãn đặc biệt. Chúng tôi sử dụng các sản phẩm cao cấp và kỹ thuật chuyên nghiệp để đảm bảo thú cưng của bạn được chăm sóc tốt nhất.",
                    Features = new List<string> {
                        "Tắm rửa với dầu gội cao cấp",
                        "Massage thư giãn chuyên nghiệp",
                        "Chăm sóc lông và da toàn diện",
                        "Cắt móng chân an toàn",
                        "Vệ sinh tai và mắt kỹ lưỡng",
                        "Xịt nước hoa thú cưng thơm mát",
                        "Chải lông và tạo kiểu",
                        "Kiểm tra sức khỏe cơ bản"
                    },
                    Price = "150.000 - 300.000 VNĐ",
                    Duration = "60 - 90 phút",
                    Image = "/images/services/spa.jpg"
                },
                new { 
                    Id = 2, 
                    Name = "Pet's Grooming", 
                    ShortName = "Grooming",
                    Description = "Cắt tỉa lông chuyên nghiệp",
                    FullDescription = "Dịch vụ cắt tỉa lông chuyên nghiệp với đội ngũ thợ có tay nghề cao và kinh nghiệm nhiều năm. Chúng tôi sẽ tạo kiểu lông đẹp mắt và phù hợp với từng giống thú cưng, đảm bảo thú cưng của bạn luôn xinh đẹp và thoải mái.",
                    Features = new List<string> {
                        "Cắt tỉa lông theo yêu cầu và giống loài",
                        "Tạo kiểu lông đẹp mắt và thời trang",
                        "Cắt tỉa lông mặt và chân tỉ mỉ",
                        "Vệ sinh kỹ lưỡng toàn thân",
                        "Tư vấn kiểu lông phù hợp",
                        "Chăm sóc sau cắt tỉa",
                        "Cắt tỉa móng chân an toàn",
                        "Vệ sinh tai và mắt chuyên nghiệp"
                    },
                    Price = "200.000 - 500.000 VNĐ",
                    Duration = "90 - 120 phút",
                    Image = "/images/services/grooming.jpg"
                },
                new { 
                    Id = 3, 
                    Name = "Pet's Hotel", 
                    ShortName = "Hotel",
                    Description = "Khách sạn thú cưng cao cấp",
                    FullDescription = "Pet's Hotel cung cấp dịch vụ lưu trú an toàn và tiện nghi cho thú cưng của bạn. Với không gian rộng rãi, sạch sẽ và đội ngũ chăm sóc chuyên nghiệp, chúng tôi đảm bảo thú cưng của bạn sẽ có một kỳ nghỉ thoải mái và an toàn.",
                    Features = new List<string> {
                        "Phòng riêng biệt, sạch sẽ và thoáng mát",
                        "Chế độ ăn uống đầy đủ và cân bằng dinh dưỡng",
                        "Vận động và vui chơi hàng ngày",
                        "Giám sát 24/7 bởi đội ngũ chuyên nghiệp",
                        "Chăm sóc y tế khi cần thiết",
                        "Báo cáo tình trạng hàng ngày cho chủ",
                        "Không gian vui chơi rộng rãi",
                        "Dịch vụ tắm rửa và chăm sóc cơ bản"
                    },
                    Price = "100.000 - 200.000 VNĐ/ngày",
                    Duration = "Theo yêu cầu",
                    Image = "/images/services/hotel.jpg"
                },
                new { 
                    Id = 4, 
                    Name = "Pet's Swimming", 
                    ShortName = "Swimming",
                    Description = "Hồ bơi dành riêng cho thú cưng",
                    FullDescription = "Hồ bơi sạch sẽ, an toàn dành riêng cho thú cưng. Giúp thú cưng vận động, giải nhiệt và rèn luyện sức khỏe trong môi trường nước. Chúng tôi có đội ngũ giám sát chuyên nghiệp và các thiết bị an toàn.",
                    Features = new List<string> {
                        "Hồ bơi sạch sẽ, an toàn với nước được lọc thường xuyên",
                        "Nhiệt độ nước phù hợp cho thú cưng",
                        "Giám sát chuyên nghiệp 24/7",
                        "Dụng cụ bơi lội và đồ chơi dưới nước",
                        "Vệ sinh sau khi bơi với dầu gội chuyên dụng",
                        "Khăn tắm và sấy khô",
                        "Khu vực nghỉ ngơi sau khi bơi",
                        "Hướng dẫn bơi lội cho thú cưng mới"
                    },
                    Price = "80.000 - 150.000 VNĐ",
                    Duration = "30 - 60 phút",
                    Image = "/images/services/swimming.jpg"
                }
            };

            ViewBag.Services = services;
            ViewBag.CurrentService = services.First(); // Mặc định là Pet's Spa
            ViewBag.CurrentServiceId = 1;

            return View();
        }
    }
}
