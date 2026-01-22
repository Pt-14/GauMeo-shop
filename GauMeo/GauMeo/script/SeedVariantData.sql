-- 1. Xóa dữ liệu cũ trong ProductVariants (nếu cần)
DELETE FROM ProductVariants;

-- 2. Thêm biến thể cho sản phẩm chó
-- Frontline Spot-On for Dogs (ID 264)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ (0-10kg) - Hương nhẹ', N'size', 0.00, 1, 1, 1, GETDATE(), 264),
(N'Trung bình (10-20kg) - Hương nhẹ', N'size', 50000.00, 0, 1, 2, GETDATE(), 264),
(N'Lớn (20-40kg) - Hương nhẹ', N'size', 100000.00, 0, 1, 3, GETDATE(), 264);

-- Vet's Best Digestive Chews (ID 267)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'30 viên - Vị gà', N'flavor', 0.00, 1, 1, 1, GETDATE(), 267),
(N'30 viên - Vị bò', N'flavor', 10000.00, 0, 1, 2, GETDATE(), 267),
(N'60 viên - Vị gà', N'flavor', 50000.00, 0, 1, 3, GETDATE(), 267),
(N'60 viên - Vị bò', N'flavor', 60000.00, 0, 1, 4, GETDATE(), 267);

-- Bio-Groom Grooming Brush (ID 273)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ - Màu xanh lá', N'size', 0.00, 1, 1, 1, GETDATE(), 273),
(N'Lớn - Màu trắng', N'size', 30000.00, 0, 1, 2, GETDATE(), 273);

-- IRIS OHYAMA Chew Bone (ID 281)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ - Màu xanh', N'size', 0.00, 1, 1, 1, GETDATE(), 281),
(N'Nhỏ - Màu đỏ', N'size', 5000.00, 0, 1, 2, GETDATE(), 281),
(N'Lớn - Màu xanh', N'size', 40000.00, 0, 1, 3, GETDATE(), 281),
(N'Lớn - Màu đỏ', N'size', 45000.00, 0, 1, 4, GETDATE(), 281);

-- Petkit Smart Collar (ID 288)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'S - Màu đen', N'size', 0.00, 1, 1, 1, GETDATE(), 288),
(N'S - Màu xanh', N'size', 10000.00, 0, 1, 2, GETDATE(), 288),
(N'M - Màu đen', N'size', 20000.00, 0, 1, 3, GETDATE(), 288),
(N'M - Màu xanh', N'size', 30000.00, 0, 1, 4, GETDATE(), 288),
(N'L - Màu đen', N'size', 40000.00, 0, 1, 5, GETDATE(), 288),
(N'L - Màu xanh', N'size', 50000.00, 0, 1, 6, GETDATE(), 288);

-- 3. Thêm biến thể cho sản phẩm mèo
-- Frontline Spot-On for Cats (ID 294)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ (0-5kg) - Hương nhẹ', N'size', 0.00, 1, 1, 1, GETDATE(), 294),
(N'Trung bình (5-10kg) - Hương nhẹ', N'size', 50000.00, 0, 1, 2, GETDATE(), 294),
(N'Lớn (10-20kg) - Hương nhẹ', N'size', 100000.00, 0, 1, 3, GETDATE(), 294);

-- Vet's Best Hairball Control Chews (ID 297)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'30 viên - Vị cá', N'flavor', 0.00, 1, 1, 1, GETDATE(), 297),
(N'30 viên - Vị gà', N'flavor', 10000.00, 0, 1, 2, GETDATE(), 297),
(N'60 viên - Vị cá', N'flavor', 50000.00, 0, 1, 3, GETDATE(), 297),
(N'60 viên - Vị gà', N'flavor', 60000.00, 0, 1, 4, GETDATE(), 297);

-- Bio-Groom Cat Comb (ID 303)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ - Màu xanh lá', N'size', 0.00, 1, 1, 1, GETDATE(), 303),
(N'Lớn - Màu trắng', N'size', 30000.00, 0, 1, 2, GETDATE(), 303);

-- Kit Cat Jingle Ball (ID 311)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ - Màu xanh', N'size', 0.00, 1, 1, 1, GETDATE(), 311),
(N'Nhỏ - Màu đỏ', N'size', 5000.00, 0, 1, 2, GETDATE(), 311),
(N'Lớn - Màu xanh', N'size', 40000.00, 0, 1, 3, GETDATE(), 311),
(N'Lớn - Màu đỏ', N'size', 45000.00, 0, 1, 4, GETDATE(), 311);

-- Petkit Smart Cat Collar (ID 318)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'S - Màu đen', N'size', 0.00, 1, 1, 1, GETDATE(), 318),
(N'S - Màu xanh', N'size', 10000.00, 0, 1, 2, GETDATE(), 318),
(N'M - Màu đen', N'size', 20000.00, 0, 1, 3, GETDATE(), 318),
(N'M - Màu xanh', N'size', 30000.00, 0, 1, 4, GETDATE(), 318),
(N'L - Màu đen', N'size', 40000.00, 0, 1, 5, GETDATE(), 318),
(N'L - Màu xanh', N'size', 50000.00, 0, 1, 6, GETDATE(), 318);

-- Beaphar Care+ Grain-Free Chicken (ID 200)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'500g - Vị gà', N'size', 0.00, 1, 1, 1, GETDATE(), 200),
(N'1kg - Vị gà', N'size', 200000.00, 0, 1, 2, GETDATE(), 200),
(N'2kg - Vị gà', N'size', 500000.00, 0, 1, 3, GETDATE(), 200),
(N'500g - Vị bò', N'flavor', 10000.00, 0, 1, 4, GETDATE(), 200),
(N'1kg - Vị bò', N'flavor', 210000.00, 0, 1, 5, GETDATE(), 200);


-- Beaphar Anti-Flea Shampoo (ID 204)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'200ml - Hương bạc hà', N'size', 0.00, 1, 1, 1, GETDATE(), 204),
(N'500ml - Hương bạc hà', N'size', 100000.00, 0, 1, 2, GETDATE(), 204),
(N'200ml - Hương yến mạch', N'flavor', 5000.00, 0, 1, 3, GETDATE(), 204),
(N'500ml - Hương yến mạch', N'flavor', 105000.00, 0, 1, 4, GETDATE(), 204);

-- IRIS OHYAMA Ball Toy (ID 206)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ - Màu xanh', N'size', 0.00, 1, 1, 1, GETDATE(), 206),
(N'Nhỏ - Màu đỏ', N'size', 5000.00, 0, 1, 2, GETDATE(), 206),
(N'Lớn - Màu xanh', N'size', 40000.00, 0, 1, 3, GETDATE(), 206),
(N'Lớn - Màu đỏ', N'size', 45000.00, 0, 1, 4, GETDATE(), 206);

-- Petkit Smart Collar (ID 248)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'S - Màu đen', N'size', 0.00, 1, 1, 1, GETDATE(), 248),
(N'S - Màu xanh', N'size', 10000.00, 0, 1, 2, GETDATE(), 248),
(N'M - Màu đen', N'size', 20000.00, 0, 1, 3, GETDATE(), 248),
(N'M - Màu xanh', N'size', 30000.00, 0, 1, 4, GETDATE(), 248),
(N'L - Màu đen', N'size', 40000.00, 0, 1, 5, GETDATE(), 248),
(N'L - Màu xanh', N'size', 50000.00, 0, 1, 6, GETDATE(), 248);

-- Bio-Groom Feline Maintenance (ID 231)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'500g - Vị cá', N'size', 0.00, 1, 1, 1, GETDATE(), 231),
(N'1kg - Vị cá', N'size', 200000.00, 0, 1, 2, GETDATE(), 231),
(N'500g - Vị gà', N'flavor', 10000.00, 0, 1, 3, GETDATE(), 231),
(N'1kg - Vị gà', N'flavor', 210000.00, 0, 1, 4, GETDATE(), 231);

-- TropiClean Oatmeal Shampoo (ID 235)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'200ml - Hương yến mạch', N'size', 0.00, 1, 1, 1, GETDATE(), 235),
(N'500ml - Hương yến mạch', N'size', 100000.00, 0, 1, 2, GETDATE(), 235),
(N'200ml - Hương hoa', N'flavor', 5000.00, 0, 1, 3, GETDATE(), 235),
(N'500ml - Hương hoa', N'flavor', 105000.00, 0, 1, 4, GETDATE(), 235);

-- Kit Cat Feather Wand (ID 245)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'Nhỏ - Màu xanh', N'size', 0.00, 1, 1, 1, GETDATE(), 245),
(N'Nhỏ - Màu vàng', N'size', 5000.00, 0, 1, 2, GETDATE(), 245),
(N'Lớn - Màu xanh', N'size', 40000.00, 0, 1, 3, GETDATE(), 245),
(N'Lớn - Màu vàng', N'size', 45000.00, 0, 1, 4, GETDATE(), 245);

-- IRIS OHYAMA Pet Carrier (ID 260)
INSERT INTO ProductVariants (Name, Type, PriceAdjustment, IsDefault, IsActive, DisplayOrder, CreatedAt, ProductId)
VALUES
(N'S - Màu xám', N'size', 0.00, 1, 1, 1, GETDATE(), 260),
(N'S - Màu xanh', N'size', 10000.00, 0, 1, 2, GETDATE(), 260),
(N'M - Màu xám', N'size', 20000.00, 0, 1, 3, GETDATE(), 260),
(N'M - Màu xanh', N'size', 30000.00, 0, 1, 4, GETDATE(), 260),
(N'L - Màu xám', N'size', 40000.00, 0, 1, 5, GETDATE(), 260),
(N'L - Màu xanh', N'size', 50000.00, 0, 1, 6, GETDATE(), 260);