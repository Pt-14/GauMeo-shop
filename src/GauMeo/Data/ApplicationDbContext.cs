using GauMeo.Models;
using GauMeo.Models.Products;
using GauMeo.Models.Categories;
using GauMeo.Models.Services;
using GauMeo.Models.Promotions;
using GauMeo.Models.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Product Models
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Models.Products.ProductVariant> ProductVariants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }

        // Category & Brand Models
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        // Service Models
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<ServiceVariant> ServiceVariants { get; set; }
        public DbSet<ServiceBooking> ServiceBookings { get; set; }
        public DbSet<ServiceBookingAddon> ServiceBookingAddons { get; set; }
        public DbSet<ServiceAddon> ServiceAddons { get; set; }
        public DbSet<ServiceFAQ> ServiceFAQs { get; set; }
        public DbSet<ServiceNote> ServiceNotes { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        // Promotion Models
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }

        // Cart Models
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // Order Models
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Wishlist Model
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships and constraints
            
            // Category - Self referencing for Parent/Child
            builder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent multiple cascade paths
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ServiceBooking>()
                .HasOne(sb => sb.User)
                .WithMany(u => u.ServiceBookings)
                .HasForeignKey(sb => sb.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Sửa ServiceBooking cascade để tránh multiple cascade paths
            builder.Entity<ServiceBooking>()
                .HasOne(sb => sb.Service)
                .WithMany(s => s.ServiceBookings)
                .HasForeignKey(sb => sb.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceBooking>()
                .HasOne(sb => sb.ServiceVariant)
                .WithMany(sv => sv.ServiceBookings)
                .HasForeignKey(sb => sb.ServiceVariantId)
                .OnDelete(DeleteBehavior.Restrict);

            // WishlistItem relationships
            builder.Entity<WishlistItem>()
                .HasOne(w => w.User)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WishlistItem>()
                .HasOne(w => w.Product)
                .WithMany(p => p.WishlistItems)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent duplicate wishlist entries
            builder.Entity<WishlistItem>()
                .HasIndex(w => new { w.UserId, w.ProductId })
                .IsUnique()
                .HasDatabaseName("IX_WishlistItems_UserId_ProductId");

            // Seed Data for Brands
            SeedBrands(builder);
        }

        private void SeedBrands(ModelBuilder builder)
        {
            builder.Entity<Brand>().HasData(
                new Brand
                {
                    Id = 1,
                    Name = "Royal Canin",
                    ShortName = "RC",
                    Description = "Thương hiệu thức ăn cao cấp cho chó mèo",
                    FullDescription = "Royal Canin là thương hiệu thức ăn cao cấp hàng đầu thế giới, chuyên nghiên cứu và phát triển các sản phẩm dinh dưỡng tối ưu cho từng giống chó mèo cụ thể. Với hơn 50 năm kinh nghiệm, Royal Canin cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng của bạn.",
                    Features = "[\"Thức ăn chuyên biệt theo giống loài\",\"Công thức dinh dưỡng khoa học\",\"Hỗ trợ sức khỏe toàn diện\",\"Sản phẩm cho mọi lứa tuổi\",\"Chất lượng được kiểm định nghiêm ngặt\",\"Nghiên cứu bởi đội ngũ chuyên gia\",\"Đa dạng sản phẩm\",\"Được tin dùng toàn cầu\"]",
                    Founded = "1968",
                    Origin = "Pháp",
                    Website = "www.royalcanin.com",
                    Image = "/images/brands/1.png",
                    DisplayOrder = 1,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 2,
                    Name = "Pedigree",
                    ShortName = "PD",
                    Description = "Thức ăn cho chó chất lượng cao",
                    FullDescription = "Pedigree là thương hiệu thức ăn cho chó nổi tiếng với chất lượng cao và giá cả hợp lý. Với hơn 80 năm kinh nghiệm, Pedigree đã trở thành lựa chọn tin cậy của hàng triệu chủ nuôi trên toàn thế giới.",
                    Features = "[\"Thức ăn cân bằng dinh dưỡng\",\"Hỗ trợ sức khỏe răng miệng\",\"Tăng cường hệ miễn dịch\",\"Sản phẩm cho mọi kích thước chó\",\"Giá cả hợp lý\",\"Chất lượng ổn định\",\"Dễ tiêu hóa\",\"Hương vị hấp dẫn\"]",
                    Founded = "1935",
                    Origin = "Mỹ",
                    Website = "www.pedigree.com",
                    Image = "/images/brands/2.jpg",
                    DisplayOrder = 2,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 3,
                    Name = "Purina",
                    ShortName = "PN",
                    Description = "Thương hiệu thức ăn đa dạng",
                    FullDescription = "Purina là thương hiệu thức ăn thú cưng đa dạng với nhiều dòng sản phẩm khác nhau. Từ thức ăn cao cấp đến bình dân, Purina đáp ứng mọi nhu cầu của thú cưng và chủ nuôi.",
                    Features = "[\"Đa dạng sản phẩm\",\"Chất lượng cao\",\"Giá cả cạnh tranh\",\"Hỗ trợ sức khỏe toàn diện\",\"Công thức dinh dưỡng khoa học\",\"Sản phẩm cho mọi lứa tuổi\",\"Dễ tiêu hóa\",\"Được tin dùng rộng rãi\"]",
                    Founded = "1894",
                    Origin = "Mỹ",
                    Website = "www.purina.com",
                    Image = "/images/brands/3.png",
                    DisplayOrder = 3,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 4,
                    Name = "Whiskas",
                    ShortName = "WK",
                    Description = "Thức ăn cho mèo nổi tiếng",
                    FullDescription = "Whiskas là thương hiệu thức ăn cho mèo được yêu thích trên toàn thế giới. Với hơn 50 năm kinh nghiệm, Whiskas hiểu rõ nhu cầu dinh dưỡng của mèo và mang đến những sản phẩm chất lượng cao.",
                    Features = "[\"Thức ăn chuyên biệt cho mèo\",\"Hương vị hấp dẫn\",\"Dinh dưỡng cân bằng\",\"Hỗ trợ sức khỏe mèo\",\"Sản phẩm đa dạng\",\"Chất lượng ổn định\",\"Dễ tiêu hóa\",\"Được mèo yêu thích\"]",
                    Founded = "1958",
                    Origin = "Anh",
                    Website = "www.whiskas.com",
                    Image = "/images/brands/4.png",
                    DisplayOrder = 4,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 5,
                    Name = "Hill's",
                    ShortName = "HS",
                    Description = "Thức ăn thú y chuyên nghiệp",
                    FullDescription = "Hill's Science Diet là thương hiệu thức ăn thú y chuyên nghiệp, được phát triển bởi các bác sĩ thú y. Sản phẩm của Hill's được thiết kế để hỗ trợ điều trị và phòng ngừa các vấn đề sức khỏe của thú cưng.",
                    Features = "[\"Công thức thú y chuyên nghiệp\",\"Hỗ trợ điều trị bệnh\",\"Dinh dưỡng khoa học\",\"Được bác sĩ thú y khuyến nghị\",\"Sản phẩm chuyên biệt\",\"Chất lượng cao cấp\",\"An toàn cho thú cưng\",\"Hiệu quả đã được chứng minh\"]",
                    Founded = "1939",
                    Origin = "Mỹ",
                    Website = "www.hills.com",
                    Image = "/images/brands/5.png",
                    DisplayOrder = 5,
                    IsActive = true,
                    IsFeatured = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 6,
                    Name = "Nutri-Source",
                    ShortName = "NS",
                    Description = "Thức ăn tự nhiên cao cấp",
                    FullDescription = "Nutri-Source là thương hiệu thức ăn tự nhiên cao cấp, tập trung vào việc sử dụng các nguyên liệu tự nhiên và công thức dinh dưỡng tối ưu. Sản phẩm của Nutri-Source giúp thú cưng khỏe mạnh và tràn đầy năng lượng.",
                    Features = "[\"Nguyên liệu tự nhiên\",\"Không chứa chất bảo quản nhân tạo\",\"Dinh dưỡng tối ưu\",\"Hỗ trợ tiêu hóa\",\"Tăng cường năng lượng\",\"Sản phẩm cao cấp\",\"An toàn tuyệt đối\",\"Hiệu quả lâu dài\"]",
                    Founded = "1986",
                    Origin = "Mỹ",
                    Website = "www.nutri-source.com",
                    Image = "/images/brands/6.jpg",
                    DisplayOrder = 6,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 7,
                    Name = "Acana",
                    ShortName = "AC",
                    Description = "Thức ăn sinh học tự nhiên",
                    FullDescription = "Acana là thương hiệu thức ăn sinh học tự nhiên, được sản xuất theo phương pháp truyền thống với các nguyên liệu tươi ngon. Acana cam kết mang đến những sản phẩm chất lượng cao nhất cho thú cưng.",
                    Features = "[\"Nguyên liệu tươi ngon\",\"Công thức sinh học\",\"Không chứa ngũ cốc\",\"Protein cao cấp\",\"Dinh dưỡng tự nhiên\",\"Hỗ trợ sức khỏe toàn diện\",\"Sản phẩm cao cấp\",\"Được tin dùng toàn cầu\"]",
                    Founded = "1975",
                    Origin = "Canada",
                    Website = "www.acana.com",
                    Image = "/images/brands/acana.jpg",
                    DisplayOrder = 7,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 8,
                    Name = "Orijen",
                    ShortName = "OR",
                    Description = "Thức ăn hoang dã tự nhiên",
                    FullDescription = "Orijen là thương hiệu thức ăn hoang dã tự nhiên, được thiết kế để mô phỏng chế độ ăn tự nhiên của thú cưng trong môi trường hoang dã. Sản phẩm của Orijen giúp thú cưng phát triển khỏe mạnh và tự nhiên.",
                    Features = "[\"Công thức hoang dã tự nhiên\",\"Nguyên liệu tươi sống\",\"Protein động vật cao\",\"Không chứa ngũ cốc\",\"Dinh dưỡng tự nhiên\",\"Hỗ trợ phát triển tự nhiên\",\"Sản phẩm cao cấp\",\"Được chứng minh hiệu quả\"]",
                    Founded = "1985",
                    Origin = "Canada",
                    Website = "www.orijen.com",
                    Image = "/images/brands/orijen.jpg",
                    DisplayOrder = 8,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Brand
                {
                    Id = 9,
                    Name = "Natural Core",
                    ShortName = "NC",
                    Description = "Thức ăn hữu cơ tự nhiên",
                    FullDescription = "Natural Core là thương hiệu thức ăn hữu cơ tự nhiên, tập trung vào việc sử dụng các nguyên liệu hữu cơ và phương pháp sản xuất thân thiện với môi trường. Sản phẩm của Natural Core tốt cho thú cưng và môi trường.",
                    Features = "[\"Nguyên liệu hữu cơ\",\"Thân thiện môi trường\",\"Dinh dưỡng tự nhiên\",\"Không chứa hóa chất\",\"Hỗ trợ sức khỏe toàn diện\",\"Sản phẩm chất lượng cao\",\"An toàn tuyệt đối\",\"Bảo vệ môi trường\"]",
                    Founded = "2003",
                    Origin = "Hàn Quốc",
                    Website = "www.naturalcore.com",
                    Image = "/images/brands/natural-core.jpg",
                    DisplayOrder = 9,
                    IsActive = true,
                    IsFeatured = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }
    }
} 