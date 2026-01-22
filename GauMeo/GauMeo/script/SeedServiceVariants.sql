-- Insert Service Variants (Pricing)

-- Service 1: Pet Spa & Grooming - Dog Pricing
INSERT INTO ServiceVariants (Name, Description, PetType, PetSize, Price, Duration, IsActive, DisplayOrder, CreatedAt, ServiceId)
VALUES 
(N'Gói Spa Cơ Bản', N'Tắm + Sấy + Cắt móng + Massage', 'dog', 'small', 200000, N'90 phút', 1, 1, GETDATE(), 1),
(N'Gói Spa Cơ Bản', N'Tắm + Sấy + Cắt móng + Massage', 'dog', 'medium', 250000, N'105 phút', 1, 2, GETDATE(), 1),
(N'Gói Spa Cơ Bản', N'Tắm + Sấy + Cắt móng + Massage', 'dog', 'large', 300000, N'120 phút', 1, 3, GETDATE(), 1),
(N'Gói Spa + Grooming', N'Spa đầy đủ + Cắt tỉa lông + Tạo kiểu', 'dog', 'small', 350000, N'120 phút', 1, 4, GETDATE(), 1),
(N'Gói Spa + Grooming', N'Spa đầy đủ + Cắt tỉa lông + Tạo kiểu', 'dog', 'medium', 450000, N'135 phút', 1, 5, GETDATE(), 1),
(N'Gói Spa + Grooming', N'Spa đầy đủ + Cắt tỉa lông + Tạo kiểu', 'dog', 'large', 600000, N'150 phút', 1, 6, GETDATE(), 1),

-- Service 1: Pet Spa & Grooming - Cat Pricing  
(N'Gói Spa Cơ Bản', N'Tắm + Sấy + Cắt móng + Massage', 'cat', 'small', 180000, N'75 phút', 1, 7, GETDATE(), 1),
(N'Gói Spa Cơ Bản', N'Tắm + Sấy + Cắt móng + Massage', 'cat', 'medium', 220000, N'90 phút', 1, 8, GETDATE(), 1),
(N'Gói Spa Cơ Bản', N'Tắm + Sấy + Cắt móng + Massage', 'cat', 'large', 260000, N'105 phút', 1, 9, GETDATE(), 1),
(N'Gói Spa + Grooming', N'Spa đầy đủ + Cắt tỉa lông + Tạo kiểu', 'cat', 'small', 300000, N'105 phút', 1, 10, GETDATE(), 1),
(N'Gói Spa + Grooming', N'Spa đầy đủ + Cắt tỉa lông + Tạo kiểu', 'cat', 'medium', 380000, N'120 phút', 1, 11, GETDATE(), 1),
(N'Gói Spa + Grooming', N'Spa đầy đủ + Cắt tỉa lông + Tạo kiểu', 'cat', 'large', 480000, N'135 phút', 1, 12, GETDATE(), 1),

-- Service 2: Pet Hotel - Dog Pricing
(N'Phòng Tiêu Chuẩn', N'Ăn uống + Vui chơi + Chăm sóc cơ bản + Báo cáo', 'dog', 'small', 120000, N'1 ngày', 1, 1, GETDATE(), 2),
(N'Phòng Tiêu Chuẩn', N'Ăn uống + Vui chơi + Chăm sóc cơ bản + Báo cáo', 'dog', 'medium', 150000, N'1 ngày', 1, 2, GETDATE(), 2),
(N'Phòng Tiêu Chuẩn', N'Ăn uống + Vui chơi + Chăm sóc cơ bản + Báo cáo', 'dog', 'large', 200000, N'1 ngày', 1, 3, GETDATE(), 2),
(N'Phòng VIP', N'Phòng riêng + Ăn cao cấp + Spa + Vui chơi + Báo cáo', 'dog', 'small', 250000, N'1 ngày', 1, 4, GETDATE(), 2),
(N'Phòng VIP', N'Phòng riêng + Ăn cao cấp + Spa + Vui chơi + Báo cáo', 'dog', 'medium', 350000, N'1 ngày', 1, 5, GETDATE(), 2),
(N'Phòng VIP', N'Phòng riêng + Ăn cao cấp + Spa + Vui chơi + Báo cáo', 'dog', 'large', 450000, N'1 ngày', 1, 6, GETDATE(), 2),

-- Service 2: Pet Hotel - Cat Pricing
(N'Phòng Tiêu Chuẩn', N'Ăn uống + Vui chơi + Chăm sóc cơ bản + Báo cáo', 'cat', 'small', 100000, N'1 ngày', 1, 7, GETDATE(), 2),
(N'Phòng Tiêu Chuẩn', N'Ăn uống + Vui chơi + Chăm sóc cơ bản + Báo cáo', 'cat', 'medium', 120000, N'1 ngày', 1, 8, GETDATE(), 2),
(N'Phòng Tiêu Chuẩn', N'Ăn uống + Vui chơi + Chăm sóc cơ bản + Báo cáo', 'cat', 'large', 150000, N'1 ngày', 1, 9, GETDATE(), 2),
(N'Phòng VIP', N'Phòng riêng + Ăn cao cấp + Spa + Vui chơi + Báo cáo', 'cat', 'small', 200000, N'1 ngày', 1, 10, GETDATE(), 2),
(N'Phòng VIP', N'Phòng riêng + Ăn cao cấp + Spa + Vui chơi + Báo cáo', 'cat', 'medium', 280000, N'1 ngày', 1, 11, GETDATE(), 2),
(N'Phòng VIP', N'Phòng riêng + Ăn cao cấp + Spa + Vui chơi + Báo cáo', 'cat', 'large', 350000, N'1 ngày', 1, 12, GETDATE(), 2),

-- Service 3: Pet Swimming - Only Dog Pricing
(N'Bơi Tự Do', N'Bơi tự do + Giám sát + Vệ sinh sau bơi + Sấy khô', 'dog', 'small', 100000, N'30 phút', 1, 1, GETDATE(), 3),
(N'Bơi Tự Do', N'Bơi tự do + Giám sát + Vệ sinh sau bơi + Sấy khô', 'dog', 'medium', 120000, N'45 phút', 1, 2, GETDATE(), 3),
(N'Bơi Tự Do', N'Bơi tự do + Giám sát + Vệ sinh sau bơi + Sấy khô', 'dog', 'large', 150000, N'60 phút', 1, 3, GETDATE(), 3),
(N'Bơi Có Hướng Dẫn', N'Hướng dẫn bơi + Vận động + Vệ sinh + Spa nhẹ + Sấy khô', 'dog', 'small', 180000, N'45 phút', 1, 4, GETDATE(), 3),
(N'Bơi Có Hướng Dẫn', N'Hướng dẫn bơi + Vận động + Vệ sinh + Spa nhẹ + Sấy khô', 'dog', 'medium', 220000, N'60 phút', 1, 5, GETDATE(), 3),
(N'Bơi Có Hướng Dẫn', N'Hướng dẫn bơi + Vận động + Vệ sinh + Spa nhẹ + Sấy khô', 'dog', 'large', 280000, N'75 phút', 1, 6, GETDATE(), 3),

-- Service 4: Pet Daycare - Dog Pricing
(N'Trông Giữ Nửa Ngày', N'Vui chơi + Ăn nhẹ + Giám sát + Báo cáo', 'dog', 'small', 100000, N'4 giờ', 1, 1, GETDATE(), 4),
(N'Trông Giữ Nửa Ngày', N'Vui chơi + Ăn nhẹ + Giám sát + Báo cáo', 'dog', 'medium', 120000, N'4 giờ', 1, 2, GETDATE(), 4),
(N'Trông Giữ Nửa Ngày', N'Vui chơi + Ăn nhẹ + Giám sát + Báo cáo', 'dog', 'large', 150000, N'4 giờ', 1, 3, GETDATE(), 4),
(N'Trông Giữ Cả Ngày', N'Vui chơi + 2 bữa ăn + Huấn luyện + Nghỉ ngơi + Báo cáo', 'dog', 'small', 180000, N'8 giờ', 1, 4, GETDATE(), 4),
(N'Trông Giữ Cả Ngày', N'Vui chơi + 2 bữa ăn + Huấn luyện + Nghỉ ngơi + Báo cáo', 'dog', 'medium', 220000, N'8 giờ', 1, 5, GETDATE(), 4),
(N'Trông Giữ Cả Ngày', N'Vui chơi + 2 bữa ăn + Huấn luyện + Nghỉ ngơi + Báo cáo', 'dog', 'large', 280000, N'8 giờ', 1, 6, GETDATE(), 4),

-- Service 4: Pet Daycare - Cat Pricing
(N'Trông Giữ Nửa Ngày', N'Vui chơi + Ăn nhẹ + Giám sát + Báo cáo', 'cat', 'small', 80000, N'4 giờ', 1, 7, GETDATE(), 4),
(N'Trông Giữ Nửa Ngày', N'Vui chơi + Ăn nhẹ + Giám sát + Báo cáo', 'cat', 'medium', 100000, N'4 giờ', 1, 8, GETDATE(), 4),
(N'Trông Giữ Nửa Ngày', N'Vui chơi + Ăn nhẹ + Giám sát + Báo cáo', 'cat', 'large', 120000, N'4 giờ', 1, 9, GETDATE(), 4),
(N'Trông Giữ Cả Ngày', N'Vui chơi + 2 bữa ăn + Huấn luyện + Nghỉ ngơi + Báo cáo', 'cat', 'small', 150000, N'8 giờ', 1, 10, GETDATE(), 4),
(N'Trông Giữ Cả Ngày', N'Vui chơi + 2 bữa ăn + Huấn luyện + Nghỉ ngơi + Báo cáo', 'cat', 'medium', 180000, N'8 giờ', 1, 11, GETDATE(), 4),
(N'Trông Giữ Cả Ngày', N'Vui chơi + 2 bữa ăn + Huấn luyện + Nghỉ ngơi + Báo cáo', 'cat', 'large', 220000, N'8 giờ', 1, 12, GETDATE(), 4),

-- Service 5: Pet Training - Dog Pricing
(N'Khóa Cơ Bản', N'Ngồi + Nằm + Đến + Ở lại + Tư vấn + Hướng dẫn chủ', 'dog', 'all', 600000, N'4 buổi', 1, 1, GETDATE(), 5),
(N'Khóa Cơ Bản (Nhóm)', N'Ngồi + Nằm + Đến + Ở lại + Tư vấn + Hướng dẫn chủ', 'dog', 'all', 350000, N'4 buổi', 1, 2, GETDATE(), 5),
(N'Khóa Nâng Cao', N'Cơ bản + Kỹ năng đặc biệt + Sửa hành vi + Theo dõi', 'dog', 'all', 900000, N'6 buổi', 1, 3, GETDATE(), 5),
(N'Khóa Nâng Cao (Nhóm)', N'Cơ bản + Kỹ năng đặc biệt + Sửa hành vi + Theo dõi', 'dog', 'all', 550000, N'6 buổi', 1, 4, GETDATE(), 5),
(N'Khóa Chuyên Sâu', N'Toàn diện + Hành vi phức tạp + Theo dõi dài hạn + Hỗ trợ sau khóa', 'dog', 'all', 1400000, N'8 buổi', 1, 5, GETDATE(), 5),
(N'Khóa Chuyên Sâu (Nhóm)', N'Toàn diện + Hành vi phức tạp + Theo dõi dài hạn + Hỗ trợ sau khóa', 'dog', 'all', 900000, N'8 buổi', 1, 6, GETDATE(), 5),

-- Service 5: Pet Training - Cat Pricing
(N'Khóa Cơ Bản cho Mèo', N'Nghe lời + Sử dụng cát vệ sinh + Không cào phá + Hướng dẫn chủ', 'cat', 'all', 500000, N'4 buổi', 1, 7, GETDATE(), 5),
(N'Khóa Cơ Bản cho Mèo (Nhóm)', N'Nghe lời + Sử dụng cát vệ sinh + Không cào phá + Hướng dẫn chủ', 'cat', 'all', 300000, N'4 buổi', 1, 8, GETDATE(), 5),
(N'Khóa Nâng Cao cho Mèo', N'Cơ bản + Tricks đơn giản + Xã hội hóa + Theo dõi', 'cat', 'all', 750000, N'6 buổi', 1, 9, GETDATE(), 5),
(N'Khóa Nâng Cao cho Mèo (Nhóm)', N'Cơ bản + Tricks đơn giản + Xã hội hóa + Theo dõi', 'cat', 'all', 450000, N'6 buổi', 1, 10, GETDATE(), 5),
(N'Khóa Đặc Biệt cho Mèo', N'Toàn diện + Hành vi phức tạp + Tricks nâng cao + Hỗ trợ sau khóa', 'cat', 'all', 1000000, N'8 buổi', 1, 11, GETDATE(), 5),
(N'Khóa Đặc Biệt cho Mèo (Nhóm)', N'Toàn diện + Hành vi phức tạp + Tricks nâng cao + Hỗ trợ sau khóa', 'cat', 'all', 650000, N'8 buổi', 1, 12, GETDATE(), 5); 