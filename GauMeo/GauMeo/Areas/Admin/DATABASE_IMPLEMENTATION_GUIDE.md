# 📋 Hướng dẫn triển khai Database cho Admin Area

## 🎯 Tổng quan
Admin area hiện đang sử dụng **mock data** để tránh lỗi. Tài liệu này hướng dẫn triển khai database thật từng bước một cách an toàn.

## 📁 Cấu trúc files đã tạo

### Admin Assets (Đã di chuyển vào folder riêng)
```
wwwroot/
├── css/admin/admin.css  ✅ Moved
└── js/admin/admin.js    ✅ Moved
```

### Controllers (Đang dùng mock data)
```
Areas/Admin/Controllers/
├── HomeController.cs      ✅ Mock data
├── ProductController.cs   ✅ Mock data  
├── UserController.cs      ✅ Mock data
├── OrderController.cs     ✅ Mock data
└── CategoryController.cs  ✅ Mock data
```

## 🚀 Kế hoạch triển khai từng bước

### Bước 1: Chuẩn bị Database Connection
```csharp
// Kiểm tra appsettings.json có connection string chưa
"ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=GauMeoShop;..."
}
```

### Bước 2: Triển khai từng Controller

#### 2.1 ProductController ✅ (Ưu tiên 1)
```csharp
// File: Areas/Admin/Controllers/ProductController.cs
// Thay thế mock data bằng:

public async Task<IActionResult> Index()
{
    var products = await _context.Products
        .Include(p => p.Category)
        .Include(p => p.Brand)
        .Include(p => p.ProductImages)
        .OrderByDescending(p => p.CreatedAt)
        .ToListAsync();
    return View(products);
}
```

#### 2.2 CategoryController (Ưu tiên 2)
```csharp
// Bỏ comment các dòng database query
// Thay mock data bằng real data
```

#### 2.3 UserController (Ưu tiên 3)
```csharp
// Đã sẵn sàng với Identity
// Cần kiểm tra Identity setup
```

#### 2.4 OrderController (Ưu tiên 4)
```csharp
// Cần có Order model hoàn thiện
// Cần test relationships
```

### Bước 3: Kiểm tra Models

#### Models cần kiểm tra:
- [ ] `Product.cs` - Relationships với Category, Brand
- [ ] `Category.cs` - Navigation properties
- [ ] `Brand.cs` - Navigation properties  
- [ ] `Order.cs` - User relationship
- [ ] `ApplicationUser.cs` - Identity setup

### Bước 4: Migration và Seeding

```bash
# 1. Create migration
dotnet ef migrations add "AdminAreaSetup"

# 2. Update database
dotnet ef database update

# 3. Seed sample data (optional)
```

### Bước 5: Testing từng module

#### Test ProductController:
1. ✅ Trang Index hiển thị danh sách
2. ⏳ Create product
3. ⏳ Edit product
4. ⏳ Delete product
5. ⏳ Upload images

#### Test CategoryController:
1. ⏳ CRUD operations
2. ⏳ Check product relationships

## 🔧 Script để chuyển từ Mock sang Real data

### HomeController.cs
```csharp
// Thay đổi trong Dashboard():
var stats = new
{
    TotalProducts = await _context.Products.CountAsync(),
    TotalOrders = await _context.Orders.CountAsync(),
    TotalUsers = await _context.Users.CountAsync(),
    PendingOrders = await _context.Orders.Where(o => o.Status == "Pending").CountAsync(),
    TodayRevenue = await _context.Orders
        .Where(o => o.OrderDate.Date == DateTime.Today && o.Status == "Completed")
        .SumAsync(o => o.TotalAmount)
};
```

### ProductController.cs
```csharp
// Bỏ comment tất cả dòng có "// Mock" 
// Và uncomment các dòng database query
```

## ⚠️ Lưu ý quan trọng

### 1. Database Safety
- **Luôn backup** database trước khi migrate
- Test trên **development environment** trước
- Kiểm tra **performance** với data lớn

### 2. Error Handling  
- Bọc database calls trong `try-catch`
- Log errors cho debugging
- Hiển thị thông báo user-friendly

### 3. Security
- Validate input data
- Prevent SQL injection
- Check user permissions

## 📋 Checklist triển khai

### Phase 1: Foundation
- [ ] Database connection string
- [ ] Models relationships
- [ ] Identity setup
- [ ] Migrations applied

### Phase 2: Core Features
- [ ] ProductController with real data
- [ ] CategoryController with real data  
- [ ] File upload functionality
- [ ] Image management

### Phase 3: Advanced Features
- [ ] UserController with Identity
- [ ] OrderController with full CRUD
- [ ] Reports with real data
- [ ] Dashboard statistics

### Phase 4: Polish
- [ ] Error handling
- [ ] Performance optimization
- [ ] Security review
- [ ] Testing

## 🆘 Troubleshooting

### Lỗi thường gặp:
1. **Connection string** sai
2. **Model relationships** không đúng
3. **Migration** conflicts  
4. **Identity** setup issues

### Debug steps:
1. Check `ApplicationDbContext.cs`
2. Verify models in `Models/` folder
3. Check `Program.cs` DI setup
4. Test connection in controller constructor

---

**Liên hệ để hỗ trợ triển khai từng bước!** 🚀 