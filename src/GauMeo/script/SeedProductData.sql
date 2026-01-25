-- 1. Xoá dữ liệu cũ và reset identity
DELETE FROM ProductDescriptions;
DELETE FROM ProductImages;
DELETE FROM Products;

DBCC CHECKIDENT('Products', RESEED, 0);
DBCC CHECKIDENT('ProductImages', RESEED, 0);
DBCC CHECKIDENT('ProductDescriptions', RESEED, 0);

-- 2. Seed bảng Products
SET IDENTITY_INSERT Products ON;
INSERT INTO Products (
  Id, Name, ShortDescription, Price, OriginalPrice, DiscountPercent,
  Rating, ReviewCount, StockQuantity, IsActive, IsFeatured, IsOnSale,
  FreeShipping, PopularityScore, AnimalType, Tags, CreatedAt, UpdatedAt,
  BrandId, CategoryId
) VALUES
-- Dog products
(1, N'Royal Canin Golden Retriever', N'Khẩu phần khô breed‑specific, hỗ trợ lông & tim mạch', 850000, 1300000, 35, 0, 0, 50,
 1, 1, 0, 1, 95, 'dog', N'Royal Canin,Golden Retriever,breed‑specific', GETDATE(), GETDATE(), 7, 15),
(2, N'Pedigree Adult Complete', N'Khẩu phần khô cân bằng, vị gà & rau củ', 600000, 600000, 0, 0, 0, 75,
 1, 0, 0, 0, 78, 'dog', N'Pedigree,chó trưởng thành,dry dog food', GETDATE(), GETDATE(), 8, 15),
(3, N'Hill''s Prescription Diet', N'Thức ăn thảo thú y chuyên biệt', 1200000, 1333333, 10, 0, 0, 30,
 1, 1, 0, 1, 92, 'dog', N'Hill''s,prescription diet,therapeutic', GETDATE(), GETDATE(), 11, 18),
(4, N'Pedigree Dentastix', N'Xương nhai giúp sạch răng hàng ngày', 85000, 85000, 0, 0, 0, 120,
 1, 0, 0, 0, 85, 'dog', N'Pedigree,Dentastix,dental chew', GETDATE(), GETDATE(), 8, 22),
(5, N'Royal Canin Wet Food', N'Pate ướt cao cấp, giàu độ ẩm', 45000, 56250, 20, 0, 0, 200,
 1, 0, 1, 0, 88, 'dog', N'Royal Canin,wet food,pate', GETDATE(), GETDATE(), 7, 16),
(6, N'Vitamin Multi cho chó', N'Bổ sung vitamin & khoáng chất đa dạng', 180000, 180000, 0, 0, 0, 80,
 1, 0, 0, 0, 72, 'dog', N'Nutri‑Source,vitamin supplement,health', GETDATE(), GETDATE(), 12, 23),
(7, N'Sữa tắm Bio‑Groom', N'Sữa tắm cao cấp, nhẹ dịu cho da chó', 220000, 250000, 12, 0, 0, 60,
 1, 0, 0, 0, 80, 'dog', N'Bio‑Groom,shampoo,coat care', GETDATE(), GETDATE(), 1, 27),
(8, N'Đồ chơi bóng tennis', N'Bóng tennis kích thích vận động, bền bỉ', 65000, 65000, 0, 0, 0, 150,
 1, 0, 0, 0, 75, 'dog', N'Kit Cat,tennis ball,pet toy', GETDATE(), GETDATE(), 6, 31),
-- Cat products
(9, N'Whiskas Adult Tuna', N'Khẩu phần khô cá ngừ, bổ sung omega‑3', 280000, 350000, 20, 0, 0, 90,
 1, 0, 0, 0, 82, 'cat', N'Whiskas,tuna,dry cat food', GETDATE(), GETDATE(), 10, 39),
(10, N'Royal Canin Persian', N'Dành cho mèo Ba Tư, hỗ trợ lông dài', 950000, 950000, 0, 0, 0, 40,
 1, 0, 0, 1, 98, 'cat', N'Royal Canin,Persian,breed‑specific', GETDATE(), GETDATE(), 7, 42),
(11, N'Purina Pro Plan Salmon', N'Khẩu phần cá hồi cao cấp, hỗ trợ da & lông', 720000, 960000, 25, 0, 0, 55,
 1, 1, 0, 1, 90, 'cat', N'Purina,Pro Plan,salmon,dry cat food', GETDATE(), GETDATE(), 24, 39),
(12, N'Whiskas Pate Chicken', N'Pate gà mềm mại, dễ tiêu hóa', 38000, 38000, 0, 0, 0, 300,
 1, 0, 0, 0, 85, 'cat', N'Whiskas,pate,chicken,wet cat food', GETDATE(), GETDATE(), 10, 43),
(13, N'Sheba Perfect Portions', N'Wet food chia phần nhỏ, sang trọng', 55000, 64706, 15, 0, 0, 180,
 1, 0, 0, 0, 87, 'cat', N'Sheba,perfect portions,wet cat food', GETDATE(), GETDATE(), 16, 40),
(14, N'Vitamin Omega‑3 cho mèo', N'Supplement omega‑3, hỗ trợ lông da', 165000, 165000, 0, 0, 0, 70,
 1, 0, 0, 0, 70, 'cat', N'Hill''s,omega‑3,supplement', GETDATE(), GETDATE(), 11, 47),
(15, N'Sữa tắm mèo Natural', N'Sữa tắm dịu nhẹ, không sulfate, pH cân bằng', 190000, 211111, 10, 0, 0, 85,
 1, 0, 0, 0, 73, 'cat', N'Natural Core,cat shampoo,natural', GETDATE(), GETDATE(), 15, 51),
(16, N'Chuột nhồi bông Catnip', N'Đồ chơi nhồi catnip kích thích tự nhiên mèo', 45000, 45000, 0, 0, 0, 120,
 1, 0, 0, 0, 76, 'cat', N'Kit Cat,catnip toy,interactive', GETDATE(), GETDATE(), 6, 58);
SET IDENTITY_INSERT Products OFF;

-- 3. Seed bảng ProductImages
INSERT INTO ProductImages (ProductId, ImageUrl, AltText, IsMain, DisplayOrder, CreatedAt) VALUES
-- Ảnh chính
(1, '/images/products/dog1.jpg', 'Royal Canin Golden Retriever', 1, 1, GETDATE()),
(2, '/images/products/dog2.jpg', 'Pedigree Adult Complete', 1, 1, GETDATE()),
(3, '/images/products/dog3.jpg', 'Hill''s Prescription Diet', 1, 1, GETDATE()),
(4, '/images/products/dog4.jpg', 'Pedigree Dentastix', 1, 1, GETDATE()),
(5, '/images/products/dog5.jpg', 'Royal Canin Wet Food', 1, 1, GETDATE()),
(6, '/images/products/dog6.jpg', 'Vitamin Multi cho chó', 1, 1, GETDATE()),
(7, '/images/products/dog7.jpg', 'Sữa tắm Bio‑Groom', 1, 1, GETDATE()),
(8, '/images/products/dog8.jpg', 'Đồ chơi bóng tennis', 1, 1, GETDATE()),
(9, '/images/products/cat1.jpg', 'Whiskas Adult Tuna', 1, 1, GETDATE()),
(10, '/images/products/cat2.jpg', 'Royal Canin Persian', 1, 1, GETDATE()),
(11, '/images/products/cat3.jpg', 'Purina Pro Plan Salmon', 1, 1, GETDATE()),
(12, '/images/products/cat4.jpg', 'Whiskas Pate Chicken', 1, 1, GETDATE()),
(13, '/images/products/cat5.jpg', 'Sheba Perfect Portions', 1, 1, GETDATE()),
(14, '/images/products/cat6.jpg', 'Vitamin Omega‑3 cho mèo', 1, 1, GETDATE()),
(15, '/images/products/cat7.jpg', 'Sữa tắm mèo Natural', 1, 1, GETDATE()),
(16, '/images/products/cat8.jpg', 'Chuột nhồi bông Catnip', 1, 1, GETDATE()),
-- Ảnh phụ
(1, '/images/products/dog1_1.jpg', 'Royal Canin Golden Retriever - phụ 1', 0, 2, GETDATE()),
(1, '/images/products/dog1_2.jpg', 'Royal Canin Golden Retriever - phụ 2', 0, 3, GETDATE()),
(2, '/images/products/dog2_1.jpg', 'Pedigree Adult Complete - phụ 1', 0, 2, GETDATE()),
(3, '/images/products/dog3_1.jpg', 'Hill''s Prescription Diet - phụ 1', 0, 2, GETDATE()),
(5, '/images/products/dog5_1.jpg', 'Royal Canin Wet Food - phụ 1', 0, 2, GETDATE()),
(9, '/images/products/cat1_1.jpg', 'Whiskas Adult Tuna - phụ 1', 0, 2, GETDATE()),
(10, '/images/products/cat2_1.jpg', 'Royal Canin Persian - phụ 1', 0, 2, GETDATE()),
(11, '/images/products/cat3_1.jpg', 'Purina Pro Plan Salmon - phụ 1', 0, 2, GETDATE());

-- 4. Seed bảng ProductDescriptions
INSERT INTO ProductDescriptions (ProductId, Title, Content, DisplayOrder, IsActive, CreatedAt) VALUES
-- Royal Canin Golden Retriever
(1, N'Mô tả', N'Thiết kế dành riêng cho giống chó Golden Retriever, hỗ trợ lông & sức khỏe tim.', 1, 1, GETDATE()),
(1, N'Thành phần', N'Dầu cá, protein động vật, vitamin nhóm B, taurine, omega‑3 & 6.', 2, 1, GETDATE()),
(1, N'Hướng dẫn sử dụng', N'Chia 2‑3 bữa/ngày, theo trọng lượng. Luôn có nước sạch kèm theo.', 3, 1, GETDATE()),
-- Pedigree Adult Complete
(2, N'Mô tả', N'Dinh dưỡng cân bằng cho chó trưởng thành. Vị gà, rau củ dễ ăn.', 1, 1, GETDATE()),
(2, N'Thành phần', N'Ngũ cốc, thịt, chất béo, vitamin A‑D‑E và chất xơ.', 2, 1, GETDATE()),
(2, N'Lợi ích', N'Tăng miễn dịch, hỗ trợ tiêu hóa & lông mượt.', 3, 1, GETDATE()),
-- Hill's Prescription Diet
(3, N'Mô tả', N'Thức ăn kê toa hỗ trợ điều trị bệnh về gan/thận/dạ dày.', 1, 1, GETDATE()),
(3, N'Thành phần', N'Probiotic, chất xơ hòa tan, chất chống oxy hóa.', 2, 1, GETDATE()),
(3, N'Chỉ định', N'Sử dụng theo tư vấn bác sĩ thú y. Không dùng đại trà.', 3, 1, GETDATE()),
-- Pedigree Dentastix
(4, N'Mô tả', N'Xương nhai làm sạch răng, giảm mảng bám. Cho 1 thanh/ngày.', 1, 1, GETDATE()),
(4, N'Tác dụng', N'Giảm hơi thở hôi, cải thiện nướu. Dùng cho chó >10kg.', 2, 1, GETDATE()),
(4, N'Lưu ý', N'Giám sát khi ăn. Không thay thế thức ăn chính.', 3, 1, GETDATE()),
--Royal Canin Wet Food
(5, N'Mô tả', N'Pate mềm, hương vị hấp dẫn, phù hợp chó kén ăn hoặc khó tiêu.', 1, 1, GETDATE()),
(5, N'Thành phần', N'Thịt gia cầm, nước dùng, khoáng chất, vitamin, taurine.', 2, 1, GETDATE()),
(5, N'Hướng dẫn sử dụng', N'Mở lon, dùng ngay. Bảo quản lạnh sau mở và dùng trong 48h.', 3, 1, GETDATE()),

(6, N'Mô tả', N'Bổ sung đa vitamin, khoáng chất giúp chó phát triển toàn diện.', 1, 1, GETDATE()),
(6, N'Thành phần', N'Vitamin A, B1, B6, B12, D3, E, Kẽm, Canxi, Axit Folic.', 2, 1, GETDATE()),
(6, N'Cách dùng', N'Cho ăn kèm bữa chính hoặc như phần thưởng.', 3, 1, GETDATE()),

(7, N'Mô tả', N'Sữa tắm dịu nhẹ, không gây cay mắt, giúp lông mềm và sạch.', 1, 1, GETDATE()),
(7, N'Ưu điểm', N'Không paraben, pH cân bằng, dưỡng lông và da.', 2, 1, GETDATE()),
(7, N'Sử dụng', N'Xả ướt lông, thoa đều, massage nhẹ và xả sạch.', 3, 1, GETDATE()),

(8, N'Mô tả', N'Bóng chơi cho chó, chất liệu cao su tự nhiên, đàn hồi tốt.', 1, 1, GETDATE()),
(8, N'Đặc điểm', N'Không gây hại răng, màu sắc bắt mắt, kích cỡ tiêu chuẩn.', 2, 1, GETDATE()),
(8, N'Lưu ý', N'Thay thế nếu bóng rách. Không để chó nuốt.', 3, 1, GETDATE()),

(9, N'Mô tả', N'Thức ăn hạt cá ngừ, giàu đạm & omega-3 cho mèo trưởng thành.', 1, 1, GETDATE()),
(9, N'Thành phần', N'Cá ngừ, thịt gà, dầu cá, vitamin A, D, taurine.', 2, 1, GETDATE()),
(9, N'Lợi ích', N'Giúp sáng mắt, mượt lông, tiêu hóa khoẻ.', 3, 1, GETDATE()),

(10, N'Mô tả', N'Dành riêng cho mèo Ba Tư, viên hạt thiết kế chống văng.', 1, 1, GETDATE()),
(10, N'Thành phần', N'Bột thịt gia cầm, gạo, dầu cá, cellulose, biotin.', 2, 1, GETDATE()),
(10, N'Hướng dẫn sử dụng', N'Cho ăn theo khối lượng cơ thể. Dùng trực tiếp.', 3, 1, GETDATE()),

(11, N'Mô tả', N'Khẩu phần dinh dưỡng cao, cá hồi tươi hỗ trợ da và miễn dịch.', 1, 1, GETDATE()),
(11, N'Thành phần', N'Cá hồi, ngũ cốc, dầu cá, beta-caroten, kẽm.', 2, 1, GETDATE()),
(11, N'Ưu điểm', N'Tăng đề kháng, sáng lông, dễ tiêu hoá.', 3, 1, GETDATE()),

(12, N'Mô tả', N'Pate thịt gà mềm thơm, dễ ăn cho mèo con đến trưởng thành.', 1, 1, GETDATE()),
(12, N'Thành phần', N'Thịt gà, gan gà, gelatin, vitamin, taurine.', 2, 1, GETDATE()),
(12, N'Hướng dẫn sử dụng', N'Cho ăn trực tiếp. Không cần hâm nóng.', 3, 1, GETDATE()),

(13, N'Mô tả', N'Khẩu phần nhỏ, tiện dụng, tránh thừa. Hương vị đa dạng.', 1, 1, GETDATE()),
(13, N'Thành phần', N'Thịt cá, thịt gà, dầu hạt cải, vitamin E.', 2, 1, GETDATE()),
(13, N'Khuyên dùng', N'Dùng 1 khay/bữa. Phù hợp mèo kén ăn.', 3, 1, GETDATE()),

(14, N'Mô tả', N'Dầu cá giàu omega-3 giúp giảm rụng lông và sáng da.', 1, 1, GETDATE()),
(14, N'Thành phần', N'Dầu cá hồi, EPA, DHA, vitamin E.', 2, 1, GETDATE()),
(14, N'Hướng dẫn sử dụng', N'Nhỏ vào thức ăn 1-2 lần/ngày.', 3, 1, GETDATE()),

(15, N'Mô tả', N'Sữa tắm dịu nhẹ, không hương nhân tạo, an toàn da nhạy cảm.', 1, 1, GETDATE()),
(15, N'Thành phần', N'Thảo dược tự nhiên, không sulfate, chiết xuất hoa cúc.', 2, 1, GETDATE()),
(15, N'Cách dùng', N'Tắm với nước ấm, massage nhẹ, xả sạch.', 3, 1, GETDATE()),

(16, N'Mô tả', N'Đồ chơi catnip giúp giảm stress, kích thích bản năng săn mồi.', 1, 1, GETDATE()),
(16, N'Chất liệu', N'Vải mềm nhồi bông + catnip khô tự nhiên.', 2, 1, GETDATE()),
(16, N'Lưu ý', N'Không giặt máy. Nên thay mới nếu mèo cắn rách.', 3, 1, GETDATE());
