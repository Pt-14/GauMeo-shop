-- Seed Categories Data for GauMeo Shop - FIXED VERSION
-- Cấu trúc: Level 1 (Chó/Mèo) -> Level 2 (Thức ăn, Đồ chơi, etc.) -> Level 3 (4 mục mỗi danh mục, nội dung bao quát)
-- Thứ tự cột: Id (auto), Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId

-- Sửa cột IconUrl để cho phép NULL
ALTER TABLE Categories ALTER COLUMN IconUrl NVARCHAR(500) NULL;
-- Xóa dữ liệu cũ (nếu có)
DELETE FROM Categories;
DBCC CHECKIDENT('Categories', RESEED, 0);
-- Tạm dừng IDENTITY_INSERT để có thể chèn ID cụ thể
SET IDENTITY_INSERT Categories ON;


-- Level 1: Main Categories (Chó/Mèo)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(1, N'Sản phẩm cho chó', 'san-pham-cho-cho', N'Tất cả sản phẩm dành cho chó', '/images/category/dog/dog.png', NULL, N'🐕', 1, 1, 1, 1, 'dog', GETDATE(), GETDATE(), NULL),
(2, N'Sản phẩm cho mèo', 'san-pham-cho-meo', N'Tất cả sản phẩm dành cho mèo', '/images/category/cat/cat.png', NULL, N'🐱', 1, 2, 1, 1, 'cat', GETDATE(), GETDATE(), NULL);

-- Level 2: Parent Categories for Dogs
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(3, N'Thức ăn cho chó', 'thuc-an-cho-cho', N'Các loại thức ăn dinh dưỡng cho chó', '/images/category/dog/1.png', NULL, N'🥣', 2, 1, 1, 1, 'dog', GETDATE(), GETDATE(), 1),
(4, N'Pate - Bánh thưởng', 'pate-banh-thuong-cho-cho', N'Pate và bánh thưởng cho chó', '/images/category/dog/2.png', NULL, N'🍽️', 2, 2, 1, 1, 'dog', GETDATE(), GETDATE(), 1),
(5, N'Chăm sóc sức khỏe', 'cham-soc-suc-khoe-cho-cho', N'Sản phẩm chăm sóc sức khỏe cho chó', '/images/category/dog/3.png', NULL, N'💊', 2, 3, 1, 1, 'dog', GETDATE(), GETDATE(), 1),
(6, N'Chăm sóc vệ sinh', 'cham-soc-ve-sinh-cho-cho', N'Sản phẩm vệ sinh cho chó', '/images/category/dog/4.png', NULL, N'🧴', 2, 4, 1, 1, 'dog', GETDATE(), GETDATE(), 1),
(7, N'Đồ chơi', 'do-choi-cho-cho', N'Đồ chơi vui nhộn cho chó', '/images/category/dog/5.png', NULL, N'🧸', 2, 5, 1, 1, 'dog', GETDATE(), GETDATE(), 1),
(8, N'Phụ kiện khác', 'phu-kien-khac-cho-cho', N'Các phụ kiện khác cho chó', '/images/category/dog/6.png', NULL, N'🎒', 2, 6, 1, 1, 'dog', GETDATE(), GETDATE(), 1);

-- Level 2: Parent Categories for Cats
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(9, N'Thức ăn cho mèo', 'thuc-an-cho-meo', N'Các loại thức ăn dinh dưỡng cho mèo', '/images/category/cat/1.png', NULL, N'🥣', 2, 1, 1, 1, 'cat', GETDATE(), GETDATE(), 2),
(10, N'Pate - Bánh thưởng', 'pate-banh-thuong-cho-meo', N'Pate và bánh thưởng cho mèo', '/images/category/cat/2.png', NULL, N'🍽️', 2, 2, 1, 1, 'cat', GETDATE(), GETDATE(), 2),
(11, N'Chăm sóc sức khỏe', 'cham-soc-suc-khoe-cho-meo', N'Sản phẩm chăm sóc sức khỏe cho mèo', '/images/category/cat/3.png', NULL, N'💊', 2, 3, 1, 1, 'cat', GETDATE(), GETDATE(), 2),
(12, N'Chăm sóc vệ sinh', 'cham-soc-ve-sinh-cho-meo', N'Sản phẩm vệ sinh cho mèo', '/images/category/cat/4.png', NULL, N'🧴', 2, 4, 1, 1, 'cat', GETDATE(), GETDATE(), 2),
(13, N'Đồ chơi', 'do-choi-cho-meo', N'Đồ chơi vui nhộn cho mèo', '/images/category/cat/5.png', NULL, N'🧸', 2, 5, 1, 1, 'cat', GETDATE(), GETDATE(), 2),
(14, N'Phụ kiện khác', 'phu-kien-khac-cho-meo', N'Các phụ kiện khác cho mèo', '/images/category/cat/6.png', NULL, N'🎒', 2, 6, 1, 1, 'cat', GETDATE(), GETDATE(), 2);

-- Level 3: Sub Categories for Dog Food (Parent: 3)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(15, N'Thức ăn hạt', 'thuc-an-hat-cho-cho', N'Thức ăn hạt khô cho chó', '/images/category/dog/lv3/1.png', NULL, N'🥣', 3, 1, 1, 0, 'dog', GETDATE(), GETDATE(), 3),
(16, N'Thức ăn ướt', 'thuc-an-uot-cho-cho', N'Thức ăn ướt đóng hộp cho chó', '/images/category/dog/lv3/2.png', NULL, N'🥫', 3, 2, 1, 0, 'dog', GETDATE(), GETDATE(), 3),
(17, N'Thức ăn hữu cơ', 'thuc-an-huu-co-cho-cho', N'Thức ăn hữu cơ tự nhiên cho chó', '/images/category/dog/lv3/3.png', NULL, N'🌿', 3, 3, 1, 0, 'dog', GETDATE(), GETDATE(), 3),
(18, N'Thức ăn đặc biệt', 'thuc-an-dac-biet-cho-cho', N'Thức ăn theo nhu cầu sức khỏe cho chó', '/images/category/dog/lv3/4.png', NULL, N'🍖', 3, 4, 1, 0, 'dog', GETDATE(), GETDATE(), 3);

-- Level 3: Sub Categories for Dog Treats (Parent: 4)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(19, N'Pate', 'pate-cho-cho', N'Pate mềm cho chó', '/images/category/dog/lv3/5.png', NULL, N'🍽️', 3, 1, 1, 0, 'dog', GETDATE(), GETDATE(), 4),
(20, N'Thịt sấy khô', 'thit-say-kho-cho-cho', N'Thịt sấy khô làm đồ ăn vặt cho chó', '/images/category/dog/lv3/6.png', NULL, N'🥩', 3, 2, 1, 0, 'dog', GETDATE(), GETDATE(), 4),
(21, N'Súp thưởng', 'sup-thuong-cho-cho', N'Súp thưởng dinh dưỡng cho chó', '/images/category/dog/lv3/7.png', NULL, N'🍲', 3, 3, 1, 0, 'dog', GETDATE(), GETDATE(), 4),
(22, N'Bánh quy', 'banh-quy-cho-cho', N'Bánh quy giòn cho chó', '/images/category/dog/lv3/8.png', NULL, N'🍪', 3, 4, 1, 0, 'dog', GETDATE(), GETDATE(), 4);

-- Level 3: Sub Categories for Dog Health (Parent: 5)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(23, N'Vitamin', 'vitamin-cho-cho', N'Vitamin bổ sung cho chó', '/images/category/dog/lv3/9.png', NULL, N'💊', 3, 1, 1, 0, 'dog', GETDATE(), GETDATE(), 5),
(24, N'Thuốc nhỏ gáy', 'thuoc-nho-gay-cho-cho', N'Thuốc trị ve rận cho chó', '/images/category/dog/lv3/10.png', NULL, N'💉', 3, 2, 1, 0, 'dog', GETDATE(), GETDATE(), 5),
(25, N'Hỗ trợ tiêu hóa', 'ho-tro-tieu-hoa-cho-cho', N'Sản phẩm hỗ trợ tiêu hóa cho chó', '/images/category/dog/lv3/11.png', NULL, N'🌱', 3, 3, 1, 0, 'dog', GETDATE(), GETDATE(), 5),
(26, N'Bổ sung dinh dưỡng', 'bo-sung-dinh-duong-cho-cho', N'Sản phẩm bổ sung dinh dưỡng cho chó', '/images/category/dog/lv3/12.png', NULL, N'🏥', 3, 4, 1, 0, 'dog', GETDATE(), GETDATE(), 5);

-- Level 3: Sub Categories for Dog Grooming (Parent: 6)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(27, N'Sữa tắm', 'sua-tam-cho-cho', N'Sữa tắm cho chó', '/images/category/dog/lv3/13.png', NULL, N'🧴', 3, 1, 1, 0, 'dog', GETDATE(), GETDATE(), 6),
(28, N'Bàn chải', 'ban-chai-cho-cho', N'Bàn chải chải lông và răng cho chó', '/images/category/dog/lv3/14.png', NULL, N'🪥', 3, 2, 1, 0, 'dog', GETDATE(), GETDATE(), 6),
(29, N'Khăn lau', 'khan-lau-cho-cho', N'Khăn lau vệ sinh cho chó', '/images/category/dog/lv3/15.png', NULL, N'🧽', 3, 3, 1, 0, 'dog', GETDATE(), GETDATE(), 6),
(30, N'Khác', 'san-pham-ve-sinh-khac-cho-cho', N'Sản phẩm vệ sinh khác cho chó', '/images/category/dog/lv3/16.png', NULL, N'🧼', 3, 4, 1, 0, 'dog', GETDATE(), GETDATE(), 6);

-- Level 3: Sub Categories for Dog Toys (Parent: 7)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(31, N'Bóng', 'bong-cho-cho', N'Bóng chơi cho chó', '/images/category/dog/lv3/17.png', NULL, N'⚽', 3, 1, 1, 0, 'dog', GETDATE(), GETDATE(), 7),
(32, N'Xương gặm', 'xuong-gam-cho-cho', N'Xương gặm cho chó', '/images/category/dog/lv3/18.png', NULL, N'🦴', 3, 2, 1, 0, 'dog', GETDATE(), GETDATE(), 7),
(33, N'Đồ chơi gặm', 'do-choi-gam-cho-cho', N'Đồ chơi gặm cho chó', '/images/category/dog/lv3/19.png', NULL, N'🧸', 3, 3, 1, 0, 'dog', GETDATE(), GETDATE(), 7),
(34, N'Đồ chơi vận động', 'do-choi-van-dong-cho-cho', N'Đồ chơi vận động như đĩa bay, dây thừng cho chó', '/images/category/dog/lv3/20.png', NULL, N'🎾', 3, 4, 1, 0, 'dog', GETDATE(), GETDATE(), 7);

-- Level 3: Sub Categories for Dog Accessories (Parent: 8)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(35, N'Vòng cổ', 'vong-co-cho-cho', N'Vòng cổ cho chó', '/images/category/dog/lv3/21.png', NULL, N'🔗', 3, 1, 1, 0, 'dog', GETDATE(), GETDATE(), 8),
(36, N'Dây dắt', 'day-dat-cho-cho', N'Dây dắt cho chó', '/images/category/dog/lv3/22.png', NULL, N'🦮', 3, 2, 1, 0, 'dog', GETDATE(), GETDATE(), 8),
(37, N'Lồng vận chuyển', 'long-van-chuyen-cho-cho', N'Lồng vận chuyển cho chó', '/images/category/dog/lv3/23.png', NULL, N'📦', 3, 3, 1, 0, 'dog', GETDATE(), GETDATE(), 8),
(38, N'Phụ kiện tiện ích', 'phu-kien-tien-ich-cho-cho', N'Phụ kiện tiện ích như bát ăn, giường cho chó', '/images/category/dog/lv3/24.png', NULL, N'🎒', 3, 4, 1, 0, 'dog', GETDATE(), GETDATE(), 8);

-- Level 3: Sub Categories for Cat Food (Parent: 9)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(39, N'Thức ăn hạt', 'thuc-an-hat-cho-meo', N'Thức ăn hạt khô cho mèo', '/images/category/cat/lv3/1.png', NULL, N'🥣', 3, 1, 1, 0, 'cat', GETDATE(), GETDATE(), 9),
(40, N'Thức ăn ướt', 'thuc-an-uot-cho-meo', N'Thức ăn ướt đóng hộp cho mèo', '/images/category/cat/lv3/2.png', NULL, N'🥫', 3, 2, 1, 0, 'cat', GETDATE(), GETDATE(), 9),
(41, N'Thức ăn hữu cơ', 'thuc-an-huu-co-cho-meo', N'Thức ăn hữu cơ tự nhiên cho mèo', '/images/category/cat/lv3/3.png', NULL, N'🌿', 3, 3, 1, 0, 'cat', GETDATE(), GETDATE(), 9),
(42, N'Thức ăn đặc biệt', 'thuc-an-dac-biet-cho-meo', N'Thức ăn theo nhu cầu sức khỏe cho mèo', '/images/category/cat/lv3/4.png', NULL, N'🍖', 3, 4, 1, 0, 'cat', GETDATE(), GETDATE(), 9);

-- Level 3: Sub Categories for Cat Treats (Parent: 10)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(43, N'Pate', 'pate-cho-meo', N'Pate mềm cho mèo', '/images/category/cat/lv3/5.png', NULL, N'🍽️', 3, 1, 1, 0, 'cat', GETDATE(), GETDATE(), 10),
(44, N'Thịt sấy khô', 'thit-say-kho-cho-meo', N'Thịt sấy khô làm đồ ăn vặt cho mèo', '/images/category/cat/lv3/6.png', NULL, N'🥩', 3, 2, 1, 0, 'cat', GETDATE(), GETDATE(), 10),
(45, N'Súp thưởng', 'sup-thuong-cho-meo', N'Súp thưởng dinh dưỡng cho mèo', '/images/category/cat/lv3/7.png', NULL, N'🍲', 3, 3, 1, 0, 'cat', GETDATE(), GETDATE(), 10),
(46, N'Bánh quy', 'banh-quy-cho-meo', N'Bánh quy giòn cho mèo', '/images/category/cat/lv3/8.png', NULL, N'🍪', 3, 4, 1, 0, 'cat', GETDATE(), GETDATE(), 10);

-- Level 3: Sub Categories for Cat Health (Parent: 11)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(47, N'Vitamin', 'vitamin-cho-meo', N'Vitamin bổ sung cho mèo', '/images/category/cat/lv3/9.png', NULL, N'💊', 3, 1, 1, 0, 'cat', GETDATE(), GETDATE(), 11),
(48, N'Thuốc nhỏ gáy', 'thuoc-nho-gay-cho-meo', N'Thuốc trị ve rận cho mèo', '/images/category/cat/lv3/10.png', NULL, N'💉', 3, 2, 1, 0, 'cat', GETDATE(), GETDATE(), 11),
(49, N'Hỗ trợ tiêu hóa', 'ho-tro-tieu-hoa-cho-meo', N'Sản phẩm hỗ trợ tiêu hóa cho mèo', '/images/category/cat/lv3/11.png', NULL, N'🌱', 3, 3, 1, 0, 'cat', GETDATE(), GETDATE(), 11),
(50, N'Bổ sung dinh dưỡng', 'bo-sung-dinh-duong-cho-meo', N'Sản phẩm bổ sung dinh dưỡng cho mèo', '/images/category/cat/lv3/12.png', NULL, N'🏥', 3, 4, 1, 0, 'cat', GETDATE(), GETDATE(), 11);

-- Level 3: Sub Categories for Cat Grooming (Parent: 12)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(51, N'Sữa tắm', 'sua-tam-cho-meo', N'Sữa tắm cho mèo', '/images/category/cat/lv3/13.png', NULL, N'🧴', 3, 1, 1, 0, 'cat', GETDATE(), GETDATE(), 12),
(52, N'Bàn chải', 'ban-chai-cho-meo', N'Bàn chải chải lông và răng cho mèo', '/images/category/cat/lv3/14.png', NULL, N'🪥', 3, 2, 1, 0, 'cat', GETDATE(), GETDATE(), 12),
(53, N'Khăn lau', 'khan-lau-cho-meo', N'Khăn lau vệ sinh cho mèo', '/images/category/cat/lv3/15.png', NULL, N'🧽', 3, 3, 1, 0, 'cat', GETDATE(), GETDATE(), 12),
(54, N'Khác', 'san-pham-ve-sinh-khac-cho-meo', N'Sản phẩm vệ sinh khác cho mèo', '/images/category/cat/lv3/16.png', NULL, N'🧼', 3, 4, 1, 0, 'cat', GETDATE(), GETDATE(), 12);

-- Level 3: Sub Categories for Cat Toys (Parent: 13)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(55, N'Bóng', 'bong-cho-meo', N'Bóng chơi cho mèo', '/images/category/cat/lv3/17.png', NULL, N'⚽', 3, 1, 1, 0, 'cat', GETDATE(), GETDATE(), 13),
(56, N'Cần câu mèo', 'can-cau-meo', N'Cần câu cho mèo', '/images/category/cat/lv3/18.png', NULL, N'🎣', 3, 2, 1, 0, 'cat', GETDATE(), GETDATE(), 13),
(57, N'Trụ cào móng', 'tru-cao-mong-cho-meo', N'Trụ cào móng cho mèo', '/images/category/cat/lv3/19.png', NULL, N'🐾', 3, 3, 1, 0, 'cat', GETDATE(), GETDATE(), 13),
(58, N'Đồ chơi tương tác', 'do-choi-tuong-tac-cho-meo', N'Đồ chơi tương tác như chuột giả, hầm chui cho mèo', '/images/category/cat/lv3/20.png', NULL, N'🎾', 3, 4, 1, 0, 'cat', GETDATE(), GETDATE(), 13);

-- Level 3: Sub Categories for Cat Accessories (Parent: 14)
INSERT INTO Categories (Id, Name, Slug, Description, ImageUrl, IconUrl, Icon, Level, DisplayOrder, IsActive, IsShowOnHome, AnimalType, CreatedAt, UpdatedAt, ParentCategoryId) VALUES
(59, N'Vòng cổ', 'vong-co-cho-meo', N'Vòng cổ cho mèo', '/images/category/cat/lv3/21.png', NULL, N'🔗', 3, 1, 1, 0, 'cat', GETDATE(), GETDATE(), 14),
(60, N'Dây dắt', 'day-dat-cho-meo', N'Dây dắt cho mèo', '/images/category/cat/lv3/22.png', NULL, N'🦮', 3, 2, 1, 0, 'cat', GETDATE(), GETDATE(), 14),
(61, N'Lồng vận chuyển', 'long-van-chuyen-cho-meo', N'Lồng vận chuyển cho mèo', '/images/category/cat/lv3/23.png', NULL, N'📦', 3, 3, 1, 0, 'cat', GETDATE(), GETDATE(), 14),
(62, N'Phụ kiện tiện ích', 'phu-kien-tien-ich-cho-meo', N'Phụ kiện tiện ích như bát ăn, giường cho mèo', '/images/category/cat/lv3/24.png', NULL, N'🎒', 3, 4, 1, 0, 'cat', GETDATE(), GETDATE(), 14);

-- Tắt IDENTITY_INSERT
SET IDENTITY_INSERT Categories OFF;

-- Xem kết quả
SELECT 
    c1.Name AS Level1,
    c2.Name AS Level2,
    c3.Name AS Level3,
    c3.Icon,
    c3.Slug
FROM Categories c1
LEFT JOIN Categories c2 ON c1.Id = c2.ParentCategoryId
LEFT JOIN Categories c3 ON c2.Id = c3.ParentCategoryId
WHERE c1.Level = 1
ORDER BY c1.DisplayOrder, c2.DisplayOrder, c3.DisplayOrder; 