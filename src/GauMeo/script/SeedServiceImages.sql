-- ==============================================
-- SEED SERVICE IMAGES DATA
-- ==============================================

-- Hero Images and FAQ Images are now seeded directly in SeedServiceData.sql
-- No need to UPDATE as they are included in the INSERT statements

-- Clear existing service images
DELETE FROM ServiceImages;
DBCC CHECKIDENT ('ServiceImages', RESEED, 0);

-- ==============================================
-- SERVICE 1: SPA & GROOMING SLIDER IMAGES
-- ==============================================
INSERT INTO ServiceImages (ImageUrl, AltText, IsMain, DisplayOrder, CreatedAt, ServiceId, Description, Title)
VALUES 
('/images/servicepic/spa/spa.jpg', N'Spa Thư Giãn cho thú cưng', 1, 1, GETDATE(), 1, N'Dịch vụ spa cao cấp giúp thú cưng thư giãn hoàn toàn', N'Spa Thư Giãn'),
('/images/servicepic/spa/1.jpg', N'Cắt tỉa lông chuyên nghiệp', 0, 2, GETDATE(), 1, N'Cắt tỉa lông chuyên nghiệp theo nhiều kiểu dáng hiện đại', N'Cắt Tỉa Lông'),
('/images/servicepic/spa/2.jpg', N'Tắm gội cao cấp cho thú cưng', 0, 3, GETDATE(), 1, N'Sử dụng sản phẩm chăm sóc cao cấp an toàn cho da lông', N'Tắm Gội Cao Cấp'),
('/images/servicepic/spa/3.jpg', N'Chăm sóc móng cho thú cưng', 0, 4, GETDATE(), 1, N'Cắt móng và chăm sóc đầy đủ cho thú cưng', N'Chăm Sóc Móng'),
('/images/servicepic/spa/4.jpg', N'Vệ sinh răng miệng thú cưng', 0, 5, GETDATE(), 1, N'Chăm sóc răng miệng chuyên nghiệp, an toàn', N'Vệ Sinh Răng Miệng'),
('/images/servicepic/spa/5.jpg', N'Massage thư giãn cho thú cưng', 0, 6, GETDATE(), 1, N'Massage chuyên nghiệp giúp giảm căng thẳng', N'Massage Thư Giãn');

-- ==============================================
-- SERVICE 2: PET HOTEL SLIDER IMAGES
-- ==============================================
INSERT INTO ServiceImages (ImageUrl, AltText, IsMain, DisplayOrder, CreatedAt, ServiceId, Description, Title)
VALUES 
('/images/servicepic/hotel/hotel.jpg', N'Chăm sóc 24/7 cho thú cưng', 1, 1, GETDATE(), 2, N'Đội ngũ chăm sóc chuyên nghiệp 24/7', N'Chăm Sóc 24/7'),
('/images/servicepic/hotel/1.jpg', N'Phòng Standard cho thú cưng', 0, 2, GETDATE(), 2, N'Phòng ở tiêu chuẩn thoải mái và an toàn', N'Phòng Standard'),
('/images/servicepic/hotel/2.jpg', N'Phòng VIP cho thú cưng', 0, 3, GETDATE(), 2, N'Phòng ở cao cấp với đầy đủ tiện nghi cho thú cưng', N'Phòng VIP'),
('/images/servicepic/hotel/3.jpg', N'Khu vui chơi cho thú cưng', 0, 4, GETDATE(), 2, N'Không gian vui chơi rộng rãi cho thú cưng', N'Khu Vui Chơi'),
('/images/servicepic/hotel/4.jpg', N'Bữa ăn dinh dụng cho thú cưng', 0, 5, GETDATE(), 2, N'Thức ăn dinh dưỡng được chuẩn bị tận tình', N'Bữa Ăn Dinh Dưỡng'),
('/images/servicepic/hotel/5.jpg', N'Dịch vụ đưa đón thú cưng', 0, 6, GETDATE(), 2, N'Dịch vụ đưa đón thú cưng tận nhà', N'Đưa Đón Tận Nơi');

-- ==============================================
-- SERVICE 3: PET SWIMMING SLIDER IMAGES
-- ==============================================
INSERT INTO ServiceImages (ImageUrl, AltText, IsMain, DisplayOrder, CreatedAt, ServiceId, Description, Title)
VALUES 
('/images/servicepic/pool/pool.jpg', N'Bể bơi chính cho chó', 1, 1, GETDATE(), 3, N'Bể bơi rộng rãi, nước sạch được thay đổi thường xuyên', N'Bể Bơi Chính'),
('/images/servicepic/pool/1.jpg', N'Khu tập bơi cho chó', 0, 2, GETDATE(), 3, N'Khu vực riêng dành cho tập luyện bơi lội', N'Khu Tập Bơi'),
('/images/servicepic/pool/2.jpg', N'Trang thiết bị bơi an toàn', 0, 3, GETDATE(), 3, N'Đầy đủ trang thiết bị an toàn cho chó bơi', N'Trang Thiết Bị'),
('/images/servicepic/pool/3.jpg', N'Liệu pháp nước cho chó', 0, 4, GETDATE(), 3, N'Liệu pháp phục hồi chức năng trong nước', N'Liệu Pháp Nước'),
('/images/servicepic/pool/4.jpg', N'Vệ sinh sau khi bơi', 0, 5, GETDATE(), 3, N'Tắm rửa và sấy khô hoàn toàn sau khi bơi', N'Vệ Sinh Sau Bơi'),
('/images/servicepic/pool/5.jpg', N'Huấn luyện viên bơi chuyên nghiệp', 0, 6, GETDATE(), 3, N'Đội ngũ huấn luyện viên chuyên nghiệp', N'Huấn Luyện Viên');

-- ==============================================
-- SERVICE 4: PET DAYCARE SLIDER IMAGES
-- ==============================================
INSERT INTO ServiceImages (ImageUrl, AltText, IsMain, DisplayOrder, CreatedAt, ServiceId, Description, Title)
VALUES 
('/images/servicepic/daycare/daycare.jpg', N'Khu vui chơi daycare', 1, 1, GETDATE(), 4, N'Không gian vui chơi an toàn và rộng rãi', N'Khu Vui Chơi'),
('/images/servicepic/daycare/1.jpg', N'Hoạt động tập thể daycare', 0, 2, GETDATE(), 4, N'Các hoạt động giao lưu và xã hội hóa', N'Hoạt Động Tập Thể'),
('/images/servicepic/daycare/2.jpg', N'Bữa ăn dinh dưỡng daycare', 0, 3, GETDATE(), 4, N'Chế độ ăn uống cân bằng và bổ dưỡng', N'Bữa Ăn Dinh Dưỡng'),
('/images/servicepic/daycare/3.jpg', N'Khu nghỉ ngơi daycare', 0, 4, GETDATE(), 4, N'Không gian yên tĩnh để nghỉ ngơi thư giãn', N'Khu Nghỉ Ngơi'),
('/images/servicepic/daycare/4.jpg', N'Dịch vụ đưa đón daycare', 0, 5, GETDATE(), 4, N'Dịch vụ đưa đón thuận tiện mỗi ngày', N'Đưa Đón Hàng Ngày'),
('/images/servicepic/daycare/5.jpg', N'Giám sát 24/7 daycare', 0, 6, GETDATE(), 4, N'Hệ thống giám sát và chăm sóc toàn thời gian', N'Giám Sát 24/7');

-- ==============================================
-- SERVICE 5: PET TRAINING SLIDER IMAGES
-- ==============================================
INSERT INTO ServiceImages (ImageUrl, AltText, IsMain, DisplayOrder, CreatedAt, ServiceId, Description, Title)
VALUES 
('/images/servicepic/train/training.jpg', N'Huấn luyện cơ bản cho thú cưng', 1, 1, GETDATE(), 5, N'Dạy các kỹ năng cơ bản: ngồi, nằm, đứng', N'Huấn Luyện Cơ Bản'),
('/images/servicepic/train/1.jpg', N'Huấn luyện nâng cao cho thú cưng', 0, 2, GETDATE(), 5, N'Các kỹ năng phức tạp và biểu diễn', N'Huấn Luyện Nâng Cao'),
('/images/servicepic/train/2.jpg', N'Sửa hành vi cho thú cưng', 0, 3, GETDATE(), 5, N'Khắc phục các hành vi không mong muốn', N'Sửa Hành Vi'),
('/images/servicepic/train/3.jpg', N'Huấn luyện thể thao cho thú cưng', 0, 4, GETDATE(), 5, N'Rèn luyện sự nhanh nhẹn và thể lực', N'Huấn Luyện Thể Thao'),
('/images/servicepic/train/4.jpg', N'Xã hội hóa cho thú cưng', 0, 5, GETDATE(), 5, N'Học cách tương tác với người và động vật khác', N'Xã Hội Hóa'),
('/images/servicepic/train/5.jpg', N'Huấn luyện tại nhà', 0, 6, GETDATE(), 5, N'Dịch vụ huấn luyện tận nơi tiện lợi', N'Huấn Luyện Tại Nhà');

