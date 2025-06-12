using Microsoft.AspNetCore.Mvc;

namespace GauMeo.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            // Dữ liệu mẫu cho các thương hiệu
            var brands = new List<dynamic>
            {
                new { 
                    Id = 1, 
                    Name = "Royal Canin", 
                    ShortName = "RC",
                    Description = "Thương hiệu thức ăn cao cấp cho chó mèo",
                    FullDescription = "Royal Canin là thương hiệu thức ăn cao cấp hàng đầu thế giới, chuyên nghiên cứu và phát triển các sản phẩm dinh dưỡng tối ưu cho từng giống chó mèo cụ thể. Với hơn 50 năm kinh nghiệm, Royal Canin cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng của bạn.",
                    Features = new List<string> {
                        "Thức ăn chuyên biệt theo giống loài",
                        "Công thức dinh dưỡng khoa học",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cho mọi lứa tuổi",
                        "Chất lượng được kiểm định nghiêm ngặt",
                        "Nghiên cứu bởi đội ngũ chuyên gia",
                        "Đa dạng sản phẩm",
                        "Được tin dùng toàn cầu"
                    },
                    Founded = "1968",
                    Origin = "Pháp",
                    Website = "www.royalcanin.com",
                    Image = "/images/brands/1.png"
                },
                new { 
                    Id = 2, 
                    Name = "Pedigree", 
                    ShortName = "PD",
                    Description = "Thức ăn cho chó chất lượng cao",
                    FullDescription = "Pedigree là thương hiệu thức ăn cho chó nổi tiếng với chất lượng cao và giá cả hợp lý. Với hơn 80 năm kinh nghiệm, Pedigree đã trở thành lựa chọn tin cậy của hàng triệu chủ nuôi trên toàn thế giới.",
                    Features = new List<string> {
                        "Thức ăn cân bằng dinh dưỡng",
                        "Hỗ trợ sức khỏe răng miệng",
                        "Tăng cường hệ miễn dịch",
                        "Sản phẩm cho mọi kích thước chó",
                        "Giá cả hợp lý",
                        "Chất lượng ổn định",
                        "Dễ tiêu hóa",
                        "Hương vị hấp dẫn"
                    },
                    Founded = "1935",
                    Origin = "Mỹ",
                    Website = "www.pedigree.com",
                    Image = "/images/brands/2.jpg"
                },
                new { 
                    Id = 3, 
                    Name = "Purina", 
                    ShortName = "PN",
                    Description = "Thương hiệu thức ăn đa dạng",
                    FullDescription = "Purina là thương hiệu thức ăn thú cưng đa dạng với nhiều dòng sản phẩm khác nhau. Từ thức ăn cao cấp đến bình dân, Purina đáp ứng mọi nhu cầu của thú cưng và chủ nuôi.",
                    Features = new List<string> {
                        "Đa dạng sản phẩm",
                        "Chất lượng cao",
                        "Giá cả cạnh tranh",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Công thức dinh dưỡng khoa học",
                        "Sản phẩm cho mọi lứa tuổi",
                        "Dễ tiêu hóa",
                        "Được tin dùng rộng rãi"
                    },
                    Founded = "1894",
                    Origin = "Mỹ",
                    Website = "www.purina.com",
                    Image = "/images/brands/3.png"
                },
                new { 
                    Id = 4, 
                    Name = "Whiskas", 
                    ShortName = "WK",
                    Description = "Thức ăn cho mèo nổi tiếng",
                    FullDescription = "Whiskas là thương hiệu thức ăn cho mèo được yêu thích trên toàn thế giới. Với hơn 50 năm kinh nghiệm, Whiskas hiểu rõ nhu cầu dinh dưỡng của mèo và mang đến những sản phẩm chất lượng cao.",
                    Features = new List<string> {
                        "Thức ăn chuyên biệt cho mèo",
                        "Hương vị hấp dẫn",
                        "Dinh dưỡng cân bằng",
                        "Hỗ trợ sức khỏe mèo",
                        "Sản phẩm đa dạng",
                        "Chất lượng ổn định",
                        "Dễ tiêu hóa",
                        "Được mèo yêu thích"
                    },
                    Founded = "1958",
                    Origin = "Anh",
                    Website = "www.whiskas.com",
                    Image = "/images/brands/4.png"
                },
                new { 
                    Id = 5, 
                    Name = "Hill's", 
                    ShortName = "HS",
                    Description = "Thức ăn thú y chuyên nghiệp",
                    FullDescription = "Hill's Science Diet là thương hiệu thức ăn thú y chuyên nghiệp, được phát triển bởi các bác sĩ thú y. Sản phẩm của Hill's được thiết kế để hỗ trợ điều trị và phòng ngừa các vấn đề sức khỏe của thú cưng.",
                    Features = new List<string> {
                        "Công thức thú y chuyên nghiệp",
                        "Hỗ trợ điều trị bệnh",
                        "Dinh dưỡng khoa học",
                        "Được bác sĩ thú y khuyến nghị",
                        "Sản phẩm chuyên biệt",
                        "Chất lượng cao cấp",
                        "An toàn cho thú cưng",
                        "Hiệu quả đã được chứng minh"
                    },
                    Founded = "1939",
                    Origin = "Mỹ",
                    Website = "www.hills.com",
                    Image = "/images/brands/5.png"
                },
                new { 
                    Id = 6, 
                    Name = "Nutri-Source", 
                    ShortName = "NS",
                    Description = "Thức ăn tự nhiên cao cấp",
                    FullDescription = "Nutri-Source là thương hiệu thức ăn tự nhiên cao cấp, tập trung vào việc sử dụng các nguyên liệu tự nhiên và công thức dinh dưỡng tối ưu. Sản phẩm của Nutri-Source giúp thú cưng khỏe mạnh và tràn đầy năng lượng.",
                    Features = new List<string> {
                        "Nguyên liệu tự nhiên",
                        "Không chứa chất bảo quản nhân tạo",
                        "Dinh dưỡng tối ưu",
                        "Hỗ trợ tiêu hóa",
                        "Tăng cường năng lượng",
                        "Sản phẩm cao cấp",
                        "An toàn tuyệt đối",
                        "Hiệu quả lâu dài"
                    },
                    Founded = "1986",
                    Origin = "Mỹ",
                    Website = "www.nutri-source.com",
                    Image = "/images/brands/6.jpg"
                },
                new { 
                    Id = 7, 
                    Name = "Acana", 
                    ShortName = "AC",
                    Description = "Thức ăn sinh học tự nhiên",
                    FullDescription = "Acana là thương hiệu thức ăn sinh học tự nhiên, được sản xuất theo phương pháp truyền thống với các nguyên liệu tươi ngon. Acana cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng.",
                    Features = new List<string> {
                        "Nguyên liệu tươi ngon",
                        "Công thức sinh học",
                        "Không chứa ngũ cốc",
                        "Protein cao cấp",
                        "Dinh dưỡng tự nhiên",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cao cấp",
                        "Được tin dùng toàn cầu"
                    },
                    Founded = "1975",
                    Origin = "Canada",
                    Website = "www.acana.com",
                    Image = "/images/brands/acana.jpg"
                },
                new { 
                    Id = 8, 
                    Name = "Orijen", 
                    ShortName = "OR",
                    Description = "Thức ăn hoang dã tự nhiên",
                    FullDescription = "Orijen là thương hiệu thức ăn hoang dã tự nhiên, được thiết kế để mô phỏng chế độ ăn tự nhiên của thú cưng trong môi trường hoang dã. Sản phẩm của Orijen giúp thú cưng phát triển khỏe mạnh và tự nhiên.",
                    Features = new List<string> {
                        "Công thức hoang dã tự nhiên",
                        "Nguyên liệu tươi sống",
                        "Protein động vật cao",
                        "Không chứa ngũ cốc",
                        "Dinh dưỡng tự nhiên",
                        "Hỗ trợ phát triển tự nhiên",
                        "Sản phẩm cao cấp",
                        "Được chứng minh hiệu quả"
                    },
                    Founded = "1985",
                    Origin = "Canada",
                    Website = "www.orijen.com",
                    Image = "/images/brands/orijen.jpg"
                },
                new { 
                    Id = 9, 
                    Name = "Natural Core", 
                    ShortName = "NC",
                    Description = "Thức ăn hữu cơ tự nhiên",
                    FullDescription = "Natural Core là thương hiệu thức ăn hữu cơ tự nhiên, tập trung vào việc sử dụng các nguyên liệu hữu cơ và phương pháp sản xuất thân thiện với môi trường. Sản phẩm của Natural Core tốt cho thú cưng và môi trường.",
                    Features = new List<string> {
                        "Nguyên liệu hữu cơ",
                        "Thân thiện môi trường",
                        "Dinh dưỡng tự nhiên",
                        "Không chứa hóa chất",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm chất lượng cao",
                        "An toàn tuyệt đối",
                        "Bảo vệ môi trường"
                    },
                    Founded = "2003",
                    Origin = "Hàn Quốc",
                    Website = "www.naturalcore.com",
                    Image = "/images/brands/natural-core.jpg"
                },
                new { 
                    Id = 10, 
                    Name = "Monge", 
                    ShortName = "MG",
                    Description = "Thức ăn Ý cao cấp",
                    FullDescription = "Monge là thương hiệu thức ăn Ý cao cấp, được sản xuất với công nghệ tiên tiến và nguyên liệu chất lượng cao. Với truyền thống ẩm thực Ý, Monge mang đến những sản phẩm ngon miệng và bổ dưỡng cho thú cưng.",
                    Features = new List<string> {
                        "Công nghệ sản xuất tiên tiến",
                        "Nguyên liệu chất lượng cao",
                        "Hương vị Ý đặc trưng",
                        "Dinh dưỡng cân bằng",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cao cấp",
                        "Chất lượng quốc tế",
                        "Được yêu thích tại châu Âu"
                    },
                    Founded = "1963",
                    Origin = "Ý",
                    Website = "www.monge.com",
                    Image = "/images/brands/monge.jpg"
                },
                new { 
                    Id = 11, 
                    Name = "Zenith", 
                    ShortName = "ZT",
                    Description = "Thức ăn Úc chất lượng",
                    FullDescription = "Zenith là thương hiệu thức ăn Úc chất lượng, được sản xuất với các nguyên liệu tự nhiên và công thức dinh dưỡng khoa học. Zenith cam kết mang đến những sản phẩm tốt nhất cho thú cưng của bạn.",
                    Features = new List<string> {
                        "Nguyên liệu tự nhiên Úc",
                        "Công thức dinh dưỡng khoa học",
                        "Chất lượng cao",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm đa dạng",
                        "An toàn cho thú cưng",
                        "Được tin dùng tại Úc",
                        "Giá cả hợp lý"
                    },
                    Founded = "1990",
                    Origin = "Úc",
                    Website = "www.zenith.com",
                    Image = "/images/brands/zenith.jpg"
                },
                new { 
                    Id = 12, 
                    Name = "K9 Natural", 
                    ShortName = "K9",
                    Description = "Thức ăn tự nhiên New Zealand",
                    FullDescription = "K9 Natural là thương hiệu thức ăn tự nhiên New Zealand, được sản xuất với các nguyên liệu tươi ngon và phương pháp truyền thống. Sản phẩm của K9 Natural giúp thú cưng khỏe mạnh và tràn đầy năng lượng.",
                    Features = new List<string> {
                        "Nguyên liệu tươi New Zealand",
                        "Phương pháp truyền thống",
                        "Không chứa chất bảo quản",
                        "Dinh dưỡng tự nhiên",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cao cấp",
                        "An toàn tuyệt đối",
                        "Được tin dùng toàn cầu"
                    },
                    Founded = "2006",
                    Origin = "New Zealand",
                    Website = "www.k9natural.com",
                    Image = "/images/brands/k9-natural.jpg"
                }
            };

            ViewBag.Brands = brands;
            return View();
        }

        public IActionResult Detail(int id)
        {
            // Dữ liệu mẫu cho các thương hiệu (giống như trong Index)
            var brands = new List<dynamic>
            {
                new { 
                    Id = 1, 
                    Name = "Royal Canin", 
                    ShortName = "RC",
                    Description = "Thương hiệu thức ăn cao cấp cho chó mèo",
                    FullDescription = "Royal Canin là thương hiệu thức ăn cao cấp hàng đầu thế giới, chuyên nghiên cứu và phát triển các sản phẩm dinh dưỡng tối ưu cho từng giống chó mèo cụ thể. Với hơn 50 năm kinh nghiệm, Royal Canin cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng của bạn.",
                    Features = new List<string> {
                        "Thức ăn chuyên biệt theo giống loài",
                        "Công thức dinh dưỡng khoa học",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cho mọi lứa tuổi",
                        "Chất lượng được kiểm định nghiêm ngặt",
                        "Nghiên cứu bởi đội ngũ chuyên gia",
                        "Đa dạng sản phẩm",
                        "Được tin dùng toàn cầu"
                    },
                    Founded = "1968",
                    Origin = "Pháp",
                    Website = "www.royalcanin.com",
                    Image = "/images/brands/royal-canin.jpg"
                },
                new { 
                    Id = 2, 
                    Name = "Pedigree", 
                    ShortName = "PD",
                    Description = "Thức ăn cho chó chất lượng cao",
                    FullDescription = "Pedigree là thương hiệu thức ăn cho chó nổi tiếng với chất lượng cao và giá cả hợp lý. Với hơn 80 năm kinh nghiệm, Pedigree đã trở thành lựa chọn tin cậy của hàng triệu chủ nuôi trên toàn thế giới.",
                    Features = new List<string> {
                        "Thức ăn cân bằng dinh dưỡng",
                        "Hỗ trợ sức khỏe răng miệng",
                        "Tăng cường hệ miễn dịch",
                        "Sản phẩm cho mọi kích thước chó",
                        "Giá cả hợp lý",
                        "Chất lượng ổn định",
                        "Dễ tiêu hóa",
                        "Hương vị hấp dẫn"
                    },
                    Founded = "1935",
                    Origin = "Mỹ",
                    Website = "www.pedigree.com",
                    Image = "/images/brands/pedigree.jpg"
                },
                new { 
                    Id = 3, 
                    Name = "Purina", 
                    ShortName = "PN",
                    Description = "Thương hiệu thức ăn đa dạng",
                    FullDescription = "Purina là thương hiệu thức ăn thú cưng đa dạng với nhiều dòng sản phẩm khác nhau. Từ thức ăn cao cấp đến bình dân, Purina đáp ứng mọi nhu cầu của thú cưng và chủ nuôi.",
                    Features = new List<string> {
                        "Đa dạng sản phẩm",
                        "Chất lượng cao",
                        "Giá cả cạnh tranh",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Công thức dinh dưỡng khoa học",
                        "Sản phẩm cho mọi lứa tuổi",
                        "Dễ tiêu hóa",
                        "Được tin dùng rộng rãi"
                    },
                    Founded = "1894",
                    Origin = "Mỹ",
                    Website = "www.purina.com",
                    Image = "/images/brands/purina.jpg"
                },
                new { 
                    Id = 4, 
                    Name = "Whiskas", 
                    ShortName = "WK",
                    Description = "Thức ăn cho mèo nổi tiếng",
                    FullDescription = "Whiskas là thương hiệu thức ăn cho mèo được yêu thích trên toàn thế giới. Với hơn 50 năm kinh nghiệm, Whiskas hiểu rõ nhu cầu dinh dưỡng của mèo và mang đến những sản phẩm chất lượng cao.",
                    Features = new List<string> {
                        "Thức ăn chuyên biệt cho mèo",
                        "Hương vị hấp dẫn",
                        "Dinh dưỡng cân bằng",
                        "Hỗ trợ sức khỏe mèo",
                        "Sản phẩm đa dạng",
                        "Chất lượng ổn định",
                        "Dễ tiêu hóa",
                        "Được mèo yêu thích"
                    },
                    Founded = "1958",
                    Origin = "Anh",
                    Website = "www.whiskas.com",
                    Image = "/images/brands/whiskas.jpg"
                },
                new { 
                    Id = 5, 
                    Name = "Hill's", 
                    ShortName = "HS",
                    Description = "Thức ăn thú y chuyên nghiệp",
                    FullDescription = "Hill's Science Diet là thương hiệu thức ăn thú y chuyên nghiệp, được phát triển bởi các bác sĩ thú y. Sản phẩm của Hill's được thiết kế để hỗ trợ điều trị và phòng ngừa các vấn đề sức khỏe của thú cưng.",
                    Features = new List<string> {
                        "Công thức thú y chuyên nghiệp",
                        "Hỗ trợ điều trị bệnh",
                        "Dinh dưỡng khoa học",
                        "Được bác sĩ thú y khuyến nghị",
                        "Sản phẩm chuyên biệt",
                        "Chất lượng cao cấp",
                        "An toàn cho thú cưng",
                        "Hiệu quả đã được chứng minh"
                    },
                    Founded = "1939",
                    Origin = "Mỹ",
                    Website = "www.hills.com",
                    Image = "/images/brands/hills.jpg"
                },
                new { 
                    Id = 6, 
                    Name = "Nutri-Source", 
                    ShortName = "NS",
                    Description = "Thức ăn tự nhiên cao cấp",
                    FullDescription = "Nutri-Source là thương hiệu thức ăn tự nhiên cao cấp, tập trung vào việc sử dụng các nguyên liệu tự nhiên và công thức dinh dưỡng tối ưu. Sản phẩm của Nutri-Source giúp thú cưng khỏe mạnh và tràn đầy năng lượng.",
                    Features = new List<string> {
                        "Nguyên liệu tự nhiên",
                        "Không chứa chất bảo quản nhân tạo",
                        "Dinh dưỡng tối ưu",
                        "Hỗ trợ tiêu hóa",
                        "Tăng cường năng lượng",
                        "Sản phẩm cao cấp",
                        "An toàn tuyệt đối",
                        "Hiệu quả lâu dài"
                    },
                    Founded = "1986",
                    Origin = "Mỹ",
                    Website = "www.nutri-source.com",
                    Image = "/images/brands/nutri-source.jpg"
                },
                new { 
                    Id = 7, 
                    Name = "Acana", 
                    ShortName = "AC",
                    Description = "Thức ăn sinh học tự nhiên",
                    FullDescription = "Acana là thương hiệu thức ăn sinh học tự nhiên, được sản xuất theo phương pháp truyền thống với các nguyên liệu tươi ngon. Acana cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng.",
                    Features = new List<string> {
                        "Nguyên liệu tươi ngon",
                        "Công thức sinh học",
                        "Không chứa ngũ cốc",
                        "Protein cao cấp",
                        "Dinh dưỡng tự nhiên",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cao cấp",
                        "Được tin dùng toàn cầu"
                    },
                    Founded = "1975",
                    Origin = "Canada",
                    Website = "www.acana.com",
                    Image = "/images/brands/acana.jpg"
                },
                new { 
                    Id = 8, 
                    Name = "Orijen", 
                    ShortName = "OR",
                    Description = "Thức ăn hoang dã tự nhiên",
                    FullDescription = "Orijen là thương hiệu thức ăn hoang dã tự nhiên, được thiết kế để mô phỏng chế độ ăn tự nhiên của thú cưng trong môi trường hoang dã. Sản phẩm của Orijen giúp thú cưng phát triển khỏe mạnh và tự nhiên.",
                    Features = new List<string> {
                        "Công thức hoang dã tự nhiên",
                        "Nguyên liệu tươi sống",
                        "Protein động vật cao",
                        "Không chứa ngũ cốc",
                        "Dinh dưỡng tự nhiên",
                        "Hỗ trợ phát triển tự nhiên",
                        "Sản phẩm cao cấp",
                        "Được chứng minh hiệu quả"
                    },
                    Founded = "1985",
                    Origin = "Canada",
                    Website = "www.orijen.com",
                    Image = "/images/brands/orijen.jpg"
                },
                new { 
                    Id = 9, 
                    Name = "Natural Core", 
                    ShortName = "NC",
                    Description = "Thức ăn hữu cơ tự nhiên",
                    FullDescription = "Natural Core là thương hiệu thức ăn hữu cơ tự nhiên, tập trung vào việc sử dụng các nguyên liệu hữu cơ và phương pháp sản xuất thân thiện với môi trường. Sản phẩm của Natural Core tốt cho thú cưng và môi trường.",
                    Features = new List<string> {
                        "Nguyên liệu hữu cơ",
                        "Thân thiện môi trường",
                        "Dinh dưỡng tự nhiên",
                        "Không chứa hóa chất",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm chất lượng cao",
                        "An toàn tuyệt đối",
                        "Bảo vệ môi trường"
                    },
                    Founded = "2003",
                    Origin = "Hàn Quốc",
                    Website = "www.naturalcore.com",
                    Image = "/images/brands/natural-core.jpg"
                },
                new { 
                    Id = 10, 
                    Name = "Monge", 
                    ShortName = "MG",
                    Description = "Thức ăn Ý cao cấp",
                    FullDescription = "Monge là thương hiệu thức ăn Ý cao cấp, được sản xuất với công nghệ tiên tiến và nguyên liệu chất lượng cao. Với truyền thống ẩm thực Ý, Monge mang đến những sản phẩm ngon miệng và bổ dưỡng cho thú cưng.",
                    Features = new List<string> {
                        "Công nghệ sản xuất tiên tiến",
                        "Nguyên liệu chất lượng cao",
                        "Hương vị Ý đặc trưng",
                        "Dinh dưỡng cân bằng",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cao cấp",
                        "Chất lượng quốc tế",
                        "Được yêu thích tại châu Âu"
                    },
                    Founded = "1963",
                    Origin = "Ý",
                    Website = "www.monge.com",
                    Image = "/images/brands/monge.jpg"
                },
                new { 
                    Id = 11, 
                    Name = "Zenith", 
                    ShortName = "ZT",
                    Description = "Thức ăn Úc chất lượng",
                    FullDescription = "Zenith là thương hiệu thức ăn Úc chất lượng, được sản xuất với các nguyên liệu tự nhiên và công thức dinh dưỡng khoa học. Zenith cam kết mang đến những sản phẩm tốt nhất cho thú cưng của bạn.",
                    Features = new List<string> {
                        "Nguyên liệu tự nhiên Úc",
                        "Công thức dinh dưỡng khoa học",
                        "Chất lượng cao",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm đa dạng",
                        "An toàn cho thú cưng",
                        "Được tin dùng tại Úc",
                        "Giá cả hợp lý"
                    },
                    Founded = "1990",
                    Origin = "Úc",
                    Website = "www.zenith.com",
                    Image = "/images/brands/zenith.jpg"
                },
                new { 
                    Id = 12, 
                    Name = "K9 Natural", 
                    ShortName = "K9",
                    Description = "Thức ăn tự nhiên New Zealand",
                    FullDescription = "K9 Natural là thương hiệu thức ăn tự nhiên New Zealand, được sản xuất với các nguyên liệu tươi ngon và phương pháp truyền thống. Sản phẩm của K9 Natural giúp thú cưng khỏe mạnh và tràn đầy năng lượng.",
                    Features = new List<string> {
                        "Nguyên liệu tươi New Zealand",
                        "Phương pháp truyền thống",
                        "Không chứa chất bảo quản",
                        "Dinh dưỡng tự nhiên",
                        "Hỗ trợ sức khỏe toàn diện",
                        "Sản phẩm cao cấp",
                        "An toàn tuyệt đối",
                        "Được tin dùng toàn cầu"
                    },
                    Founded = "2006",
                    Origin = "New Zealand",
                    Website = "www.k9natural.com",
                    Image = "/images/brands/k9-natural.jpg"
                }
            };

            // Tìm thương hiệu theo ID
            var brand = brands.FirstOrDefault(b => b.Id == id);
            if (brand == null)
            {
                return RedirectToAction("Index");
            }

            // Dữ liệu mẫu cho sản phẩm
            var products = new List<dynamic>
            {
                new {
                    Id = 1,
                    Name = "Royal Canin Adult Dog Food",
                    Price = 450000,
                    OriginalPrice = 500000,
                    IsOnSale = true,
                    DiscountPercent = 10,
                    IsNew = false,
                    Rating = 5,
                    ReviewCount = 128,
                    Image = "/images/products/royal-canin-adult.jpg"
                },
                new {
                    Id = 2,
                    Name = "Royal Canin Kitten Food",
                    Price = 380000,
                    OriginalPrice = 380000,
                    IsOnSale = false,
                    DiscountPercent = 0,
                    IsNew = true,
                    Rating = 4,
                    ReviewCount = 89,
                    Image = "/images/products/royal-canin-kitten.jpg"
                },
                new {
                    Id = 3,
                    Name = "Royal Canin Senior Dog Food",
                    Price = 520000,
                    OriginalPrice = 580000,
                    IsOnSale = true,
                    DiscountPercent = 10,
                    IsNew = false,
                    Rating = 5,
                    ReviewCount = 156,
                    Image = "/images/products/royal-canin-senior.jpg"
                },
                new {
                    Id = 4,
                    Name = "Royal Canin Hair & Skin Care",
                    Price = 420000,
                    OriginalPrice = 420000,
                    IsOnSale = false,
                    DiscountPercent = 0,
                    IsNew = false,
                    Rating = 4,
                    ReviewCount = 203,
                    Image = "/images/products/royal-canin-hair-skin.jpg"
                },
                new {
                    Id = 5,
                    Name = "Royal Canin Digestive Care",
                    Price = 480000,
                    OriginalPrice = 540000,
                    IsOnSale = true,
                    DiscountPercent = 11,
                    IsNew = false,
                    Rating = 5,
                    ReviewCount = 167,
                    Image = "/images/products/royal-canin-digestive.jpg"
                },
                new {
                    Id = 6,
                    Name = "Royal Canin Weight Control",
                    Price = 460000,
                    OriginalPrice = 460000,
                    IsOnSale = false,
                    DiscountPercent = 0,
                    IsNew = false,
                    Rating = 4,
                    ReviewCount = 94,
                    Image = "/images/products/royal-canin-weight.jpg"
                }
            };

            // Dữ liệu mẫu cho danh mục
            var categories = new List<dynamic>
            {
                new { Id = 1, Name = "Thức ăn cho chó", Count = 45 },
                new { Id = 2, Name = "Thức ăn cho mèo", Count = 38 },
                new { Id = 3, Name = "Thức ăn khô", Count = 67 },
                new { Id = 4, Name = "Thức ăn ướt", Count = 23 },
                new { Id = 5, Name = "Thức ăn hữu cơ", Count = 15 },
                new { Id = 6, Name = "Thức ăn thú y", Count = 12 }
            };

            // Dữ liệu mẫu cho khoảng giá
            var priceRanges = new List<dynamic>
            {
                new { Value = "0-200000", Label = "Dưới 200.000đ", Count = 23 },
                new { Value = "200000-500000", Label = "200.000đ - 500.000đ", Count = 45 },
                new { Value = "500000-1000000", Label = "500.000đ - 1.000.000đ", Count = 28 },
                new { Value = "1000000+", Label = "Trên 1.000.000đ", Count = 12 }
            };

            ViewBag.Brand = brand;
            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.PriceRanges = priceRanges;

            return View();
        }
    }
} 