-- Tạo test case cho Product ID 260 với discount
-- Product này có variants từ 230,000đ đến 280,000đ

-- 1. Set discount 15% cho product ID 260
UPDATE Products 
SET DiscountPercent = 15, IsOnSale = 1
WHERE Id = 260;

-- 2. Kiểm tra kết quả
SELECT p.Id, p.Name, p.Price, p.DiscountPercent, p.IsOnSale,
       COUNT(pv.Id) as VariantCount,
       MIN(p.Price + ISNULL(pv.PriceAdjustment, 0)) as MinVariantPrice,
       MAX(p.Price + ISNULL(pv.PriceAdjustment, 0)) as MaxVariantPrice
FROM Products p
LEFT JOIN ProductVariants pv ON p.Id = pv.ProductId AND pv.IsActive = 1
WHERE p.Id = 260
GROUP BY p.Id, p.Name, p.Price, p.DiscountPercent, p.IsOnSale;

-- Kết quả mong đợi:
-- MinVariantPrice: 230,000 (giá base + 0)
-- MaxVariantPrice: 280,000 (giá base + 50,000)
-- Với discount 15%:
-- Dao động hiển thị: 195,500đ - 238,000đ (sau discount)
-- Dao động gạch ngang: 230,000đ - 280,000đ (trước discount)
