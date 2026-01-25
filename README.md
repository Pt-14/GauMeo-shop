# 🐾 GauMeo Shop - E-commerce Platform

E-commerce platform cho shop thú cưng được xây dựng với ASP.NET Core MVC (.NET 8.0).

## 📁 Cấu trúc Project

```
GauMeo-shop/
├── GauMeo.sln              # Solution file
├── README.md                # File này
├── .gitignore              # Git ignore rules
│
├── src/                    # Source code
│   └── GauMeo/            # Main web application
│       ├── GauMeo.csproj
│       ├── Program.cs
│       ├── Areas/         # Admin area
│       ├── Controllers/   # MVC Controllers
│       ├── Models/        # Data models
│       ├── Views/         # Razor views
│       ├── Data/          # DbContext & Data layer
│       ├── Migrations/    # EF Core migrations
│       ├── wwwroot/       # Static files (CSS, JS, images)
│       └── ...
│
└── docs/                   # Documentation (nếu có)
```

## 🚀 Yêu cầu Hệ thống

- **.NET SDK 8.0** hoặc cao hơn
- **SQL Server** (hoặc SQL Server Express/LocalDB)
- **Visual Studio 2022** hoặc **VS Code** với C# extension
- **Git** (để clone repository)

## ⚙️ Cài đặt và Chạy

### 1. Clone Repository
```bash
git clone <repository-url>
cd GauMeo-shop
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Cấu hình Database

Tạo file `src/GauMeo/appsettings.json` từ `appsettings.Example.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=GauMeoDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Chạy Migrations
```bash
cd src/GauMeo
dotnet ef database update
```

### 5. Chạy Application
```bash
# Từ root folder
dotnet run --project src/GauMeo/GauMeo.csproj

# Hoặc từ Visual Studio: F5
```

Application sẽ chạy tại: `https://localhost:5001` hoặc `http://localhost:5000`

## 🛠️ Công nghệ Sử dụng

- **Framework**: ASP.NET Core MVC 8.0
- **Database**: SQL Server với Entity Framework Core
- **Authentication**: ASP.NET Core Identity + Google OAuth
- **Frontend**: Razor Views, CSS3, JavaScript
- **Architecture**: MVC Pattern với Areas

## 📦 Features

### Customer Features
- ✅ Product browsing & filtering
- ✅ Shopping cart
- ✅ Wishlist
- ✅ Product reviews & ratings
- ✅ Order management
- ✅ User authentication (Email/Google)
- ✅ Service booking

### Admin Features
- ✅ Product management
- ✅ Category & Brand management
- ✅ Order management
- ✅ User management
- ✅ Promotion management
- ✅ Service management
- ✅ Review management

## 📝 Development

### Build Project
```bash
dotnet build
```

### Run Tests (nếu có)
```bash
dotnet test
```

### Tạo Migration mới
```bash
cd src/GauMeo
dotnet ef migrations add MigrationName
```

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👤 Author

© 2025 GauMeo Petshop, Inc. All rights reserved by Fang.

---

**Note**: Đây là project đang trong quá trình phát triển. Một số tính năng có thể chưa hoàn thiện.
