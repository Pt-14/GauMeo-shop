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
                    FullDescription = "Pet's Spa cung cấp dịch vụ chăm sóc toàn diện cho thú cưng của bạn. Bao gồm tắm rửa, massage, chăm sóc lông da, và các liệu pháp thư giãn đặc biệt.",
                    Features = new List<string> {
                        "Tắm rửa với dầu gội cao cấp",
                        "Massage thư giãn",
                        "Chăm sóc lông và da",
                        "Cắt móng chân",
                        "Vệ sinh tai và mắt",
                        "Xịt nước hoa thú cưng"
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
                    FullDescription = "Dịch vụ cắt tỉa lông chuyên nghiệp với đội ngũ thợ có tay nghề cao. Chúng tôi sẽ tạo kiểu lông đẹp mắt và phù hợp với từng giống thú cưng.",
                    Features = new List<string> {
                        "Cắt tỉa lông theo yêu cầu",
                        "Tạo kiểu lông đẹp mắt",
                        "Cắt tỉa lông mặt và chân",
                        "Vệ sinh kỹ lưỡng",
                        "Tư vấn kiểu lông phù hợp",
                        "Chăm sóc sau cắt tỉa"
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
                    FullDescription = "Pet's Hotel cung cấp dịch vụ lưu trú an toàn và tiện nghi cho thú cưng của bạn. Với không gian rộng rãi, sạch sẽ và đội ngũ chăm sóc chuyên nghiệp.",
                    Features = new List<string> {
                        "Phòng riêng biệt, sạch sẽ",
                        "Chế độ ăn uống đầy đủ",
                        "Vận động và vui chơi hàng ngày",
                        "Giám sát 24/7",
                        "Chăm sóc y tế khi cần",
                        "Báo cáo tình trạng hàng ngày"
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
                    FullDescription = "Hồ bơi sạch sẽ, an toàn dành riêng cho thú cưng. Giúp thú cưng vận động, giải nhiệt và rèn luyện sức khỏe trong môi trường nước.",
                    Features = new List<string> {
                        "Hồ bơi sạch sẽ, an toàn",
                        "Nhiệt độ nước phù hợp",
                        "Giám sát chuyên nghiệp",
                        "Dụng cụ bơi lội",
                        "Vệ sinh sau khi bơi",
                        "Khăn tắm và sấy khô"
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
