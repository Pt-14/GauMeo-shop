-- Insert Service Addons (Additional Services)

-- Service 1: Pet Spa & Grooming Addons
INSERT INTO ServiceAddons (Name, Price, Description, IsActive, DisplayOrder, CreatedAt, ServiceId)
VALUES 
(N'Cắt Móng Tay', 50000, N'Cắt và mài móng chuyên nghiệp, an toàn', 1, 1, GETDATE(), 1),
(N'Vệ Sinh Tai', 40000, N'Làm sạch tai an toàn và hiệu quả', 1, 2, GETDATE(), 1),
(N'Massage Thư Giãn', 120000, N'Massage giúp thú cưng thư giãn và giảm căng thẳng', 1, 3, GETDATE(), 1),
(N'Vệ Sinh Răng Miệng', 60000, N'Làm sạch răng miệng, khử mùi hôi', 1, 4, GETDATE(), 1),
(N'Xịt Nước Hoa', 30000, N'Nước hoa cao cấp dành riêng cho thú cưng', 1, 5, GETDATE(), 1),
(N'Đắp Mặt Nạ Lông', 180000, N'Mặt nạ dưỡng ẩm làm mềm lông', 1, 6, GETDATE(), 1),

-- Service 2: Pet Hotel Addons
(N'Đưa Đón Tận Nhà', 100000, N'Dịch vụ đưa đón thú cưng tận nơi', 1, 1, GETDATE(), 2),
(N'Spa Trong Lưu Trú', 200000, N'Tắm spa và grooming trong thời gian ở lại', 1, 2, GETDATE(), 2),
(N'Chăm Sóc Y Tế', 150000, N'Khám sức khỏe và chăm sóc y tế cơ bản', 1, 3, GETDATE(), 2),
(N'Báo Cáo Hình Ảnh', 50000, N'Gửi hình ảnh và video thú cưng hàng ngày', 1, 4, GETDATE(), 2),

-- Service 3: Pet Swimming Addons
(N'Huấn Luyện Bơi Cơ Bản', 150000, N'Hướng dẫn chó học bơi từ cơ bản', 1, 1, GETDATE(), 3),
(N'Vệ Sinh Sau Bơi', 80000, N'Tắm sạch và sấy khô hoàn toàn', 1, 2, GETDATE(), 3),
(N'Thuê Đồ Bơi', 30000, N'Áo phao và phụ kiện an toàn cho chó', 1, 3, GETDATE(), 3),
(N'Chụp Ảnh Dưới Nước', 100000, N'Chụp ảnh kỷ niệm cho chó trong bể bơi', 1, 4, GETDATE(), 3),

-- Service 4: Pet Daycare Addons
(N'Đưa Đón Hàng Ngày', 80000, N'Đưa đón thú cưng hàng ngày', 1, 1, GETDATE(), 4),
(N'Huấn Luyện Cơ Bản', 100000, N'Dạy các kỹ năng cơ bản trong ngày', 1, 2, GETDATE(), 4),
(N'Chăm Sóc Y Tế', 120000, N'Theo dõi sức khỏe và sơ cứu khi cần', 1, 3, GETDATE(), 4),
(N'Ăn Uống Đặc Biệt', 60000, N'Thức ăn dinh dưỡng theo chế độ riêng', 1, 4, GETDATE(), 4),

-- Service 5: Pet Training Addons
(N'Huấn Luyện Tại Nhà', 200000, N'Huấn luyện viên đến tận nhà hướng dẫn', 1, 1, GETDATE(), 5),
(N'Video Hướng Dẫn', 50000, N'Video chi tiết cách thực hành tại nhà', 1, 2, GETDATE(), 5),
(N'Tư Vấn Hành Vi', 150000, N'Tư vấn giải quyết vấn đề hành vi', 1, 3, GETDATE(), 5),
(N'Chứng Chỉ Hoàn Thành', 30000, N'Cấp chứng chỉ khi hoàn thành khóa học', 1, 4, GETDATE(), 5); 