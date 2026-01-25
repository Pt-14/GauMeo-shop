# 🐾 GauMeo Shop - E-commerce Platform

E-commerce platform for pet shop built with ASP.NET Core MVC (.NET 8.0).

## 📁 Project Structure

```
GauMeo-shop/
├── GauMeo.sln              # Solution file
├── README.md                # This file
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
└── docs/                   # Documentation (if any)
```

## 🚀 System Requirements

- **.NET SDK 8.0** or higher
- **SQL Server** (or SQL Server Express/LocalDB)
- **Visual Studio 2022** or **VS Code** with C# extension
- **Git** (to clone repository)

## ⚙️ Installation and Setup

### 1. Clone Repository
```bash
git clone <repository-url>
cd GauMeo-shop
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Configure Database

Create `src/GauMeo/appsettings.json` from `appsettings.Example.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=GauMeoDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Run Migrations
```bash
cd src/GauMeo
dotnet ef database update
```

### 5. Run Application
```bash
# From root folder
dotnet run --project src/GauMeo/GauMeo.csproj

# Or from Visual Studio: F5
```

Application will run at: `https://localhost:5001` or `http://localhost:5000`

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core MVC 8.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity + Google OAuth
- **Frontend**: Razor Views, CSS3, JavaScript
- **Architecture**: MVC Pattern with Areas

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

### Run Tests (if any)
```bash
dotnet test
```

### Create New Migration
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

**Note**: This project is currently under development. Some features may not be fully completed.
