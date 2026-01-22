-- Drop existing data first
DELETE FROM ServiceNotes;
DELETE FROM ServiceAddons;  
DELETE FROM ServiceFAQs;
DELETE FROM ServiceVariants;
DELETE FROM Services;

-- Reset identity seeds
DBCC CHECKIDENT ('Services', RESEED, 0);
DBCC CHECKIDENT ('ServiceVariants', RESEED, 0);
DBCC CHECKIDENT ('ServiceFAQs', RESEED, 0);
DBCC CHECKIDENT ('ServiceAddons', RESEED, 0);
DBCC CHECKIDENT ('ServiceNotes', RESEED, 0);

-- Insert Services
INSERT INTO Services (Name, ShortName, Description, FullDescription, Price, MinPrice, MaxPrice, Duration, Image, Features, IsActive, IsFeatured, DisplayOrder, CreatedAt, UpdatedAt, FaqImage)
VALUES 
-- Service 1: Pet Spa & Grooming
(N'Pet Spa & Grooming', N'SPA & GROOMING', N'Spa & Cắt tỉa lông chuyên nghiệp', 
N'Dịch vụ Spa và cắt tỉa lông cao cấp cho thú cưng của bạn. Bao gồm tắm rửa, massage, chăm sóc lông da, cắt tỉa lông theo yêu cầu và các liệu pháp thư giãn đặc biệt. Chúng tôi sử dụng các sản phẩm cao cấp và kỹ thuật chuyên nghiệp để đảm bảo thú cưng của bạn được chăm sóc tốt nhất.',
N'180.000 - 600.000 VNĐ', 180000, 600000, N'75 - 150 phút',
'/images/servicepic/grooming.png',
N'["Tắm rửa với dầu gội cao cấp chuyên dụng","Massage thư giãn chuyên nghiệp","Chăm sóc lông và da toàn diện","Cắt tỉa lông theo yêu cầu và giống loài","Tạo kiểu lông đẹp mắt và thời trang","Cắt móng chân an toàn và chính xác","Vệ sinh tai và mắt kỹ lưỡng","Xịt nước hoa thú cưng thơm mát","Chăm sóc sau spa và grooming"]',
1, 1, 1, GETDATE(), GETDATE(), '/images/servicepic/spa.png'),

-- Service 2: Pet Hotel  
(N'Pet Hotel', N'HOTEL', N'Khách sạn thú cưng cao cấp',
N'Pet''s Hotel cung cấp dịch vụ lưu trú an toàn và tiện nghi cho thú cưng của bạn. Với không gian rộng rãi, sạch sẽ và đội ngũ chăm sóc chuyên nghiệp 24/7, chúng tôi đảm bảo thú cưng của bạn sẽ có một kỳ nghỉ thoải mái và an toàn.',
N'100.000 - 450.000 VNĐ', 100000, 450000, N'1 ngày',
'/images/servicepic/hotel.png',
N'["Phòng riêng biệt, sạch sẽ và thoáng mát","Chế độ ăn uống đầy đủ và cân bằng dinh dưỡng","Vận động và vui chơi hàng ngày","Giám sát 24/7 bởi đội ngũ chuyên nghiệp","Chăm sóc y tế khi cần thiết","Báo cáo tình trạng hàng ngày cho chủ","Không gian riêng cho chó và mèo","Hệ thống camera giám sát","Dịch vụ đưa đón tận nhà"]',
1, 1, 2, GETDATE(), GETDATE(), '/images/servicepic/hotel.jpg'),

-- Service 3: Pet Swimming
(N'Pet Swimming', N'SWIMMING', N'Hồ bơi dành riêng cho chó',
N'Hồ bơi sạch sẽ, an toàn dành riêng cho chó. Giúp chó vận động, giải nhiệt và rèn luyện sức khỏe trong môi trường nước. Chúng tôi có đội ngũ giám sát chuyên nghiệp và các thiết bị an toàn đầy đủ.',
N'100.000 - 280.000 VNĐ', 100000, 280000, N'30 - 75 phút',
'/images/servicepic/swim.png',
N'["Hồ bơi sạch sẽ, an toàn với nước được lọc thường xuyên","Nhiệt độ nước phù hợp cho chó","Giám sát chuyên nghiệp 24/7","Dụng cụ bơi lội và đồ chơi dưới nước","Vệ sinh sau khi bơi với dầu gội chuyên dụng","Khăn tắm và sấy khô","Huấn luyện bơi lội cơ bản","Thiết bị an toàn đầy đủ","Khu vực nghỉ ngơi sau bơi"]',
1, 1, 3, GETDATE(), GETDATE(), '/images/servicepic/pool.jpg'),

-- Service 4: Pet Daycare
(N'Pet Daycare', N'DAYCARE', N'Trông giữ thú cưng theo ngày',
N'Dịch vụ trông giữ thú cưng theo ngày với môi trường vui chơi, học tập và giao lưu. Thú cưng của bạn sẽ được chăm sóc, vui chơi cùng bạn bè và phát triển kỹ năng xã hội trong môi trường an toàn, chuyên nghiệp.',
N'80.000 - 280.000 VNĐ', 80000, 280000, N'4 - 8 giờ',
'/images/servicepic/daycare.png',
N'["Trông giữ theo giờ hoặc cả ngày","Hoạt động vui chơi đa dạng","Giao lưu với thú cưng khác","Huấn luyện kỹ năng cơ bản","Chăm sóc ăn uống đầy đủ","Báo cáo hoạt động hàng ngày","Khu vực riêng cho chó và mèo","Giám sát 24/7","Hoạt động ngoài trời an toàn"]',
1, 1, 4, GETDATE(), GETDATE(), '/images/servicepic/Daycare.jpg'),

-- Service 5: Pet Training
(N'Pet Training', N'TRAINING', N'Huấn luyện thú cưng chuyên nghiệp',
N'Dịch vụ huấn luyện thú cưng chuyên nghiệp với các khóa học từ cơ bản đến nâng cao. Giúp thú cưng của bạn học các kỹ năng cần thiết, cải thiện hành vi và tăng cường mối quan hệ giữa chủ và thú cưng. Chúng tôi cung cấp huấn luyện cho cả chó và mèo với phương pháp phù hợp cho từng loài.',
N'300.000 - 1.400.000 VNĐ', 300000, 1400000, N'4 - 8 buổi',
'/images/servicepic/train.png',
N'["Huấn luyện vâng lời cơ bản","Sửa chữa hành vi xấu","Kỹ năng xã hội với người và thú cưng khác","Huấn luyện kỹ năng đặc biệt","Tư vấn chăm sóc và giáo dục","Khóa học nhóm và cá nhân","Huấn luyện thể thao cho chó","Huấn luyện thông minh cho mèo","Hướng dẫn chủ cách huấn luyện","Theo dõi tiến độ dài hạn"]',
1, 1, 5, GETDATE(), GETDATE(), '/images/servicepic/train.jpg'); 