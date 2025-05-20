PETBRIDGE/
├── app/
│   ├── controllers/
│   │   ├── admin/
│   │   │   ├── CustomerController.php     (từ manage_customer.php)
│   │   │   ├── PetShopController.php      (từ manage_petshop.php)
│   │   │   ├── ServiceController.php      (từ manage_services.php)
│   │   │   ├── ReviewController.php       (từ manage_reviews.php)
│   │   │   ├── EmailController.php        (từ manage_email.php)
│   │   │   └── BookingController.php      (từ manage_bookings.php)
│   │   │
│   │   ├── user/
│   │   │   ├── ProfileController.php      (từ profile.php)
│   │   │   ├── PetShopController.php      (từ petshop_details.php)
│   │   │   ├── BookingController.php      (từ booking.php)
│   │   │   └── WishlistController.php     (từ wishlist.php)
│   │   │
│   │   ├── AuthController.php             (từ login.php, register.php, logout.php)
│   │   └── HomeController.php             (từ index.php)
│   │
│   ├── models/
│   │   ├── User.php                       (xử lý users table)
│   │   ├── PetShop.php                    (xử lý petshops table)
│   │   ├── Service.php                    (xử lý services table)
│   │   ├── Booking.php                    (xử lý bookings table)
│   │   ├── Review.php                     (xử lý reviews table)
│   │   ├── Favorite.php                   (xử lý favorites table)
│   │   ├── EmailTemplate.php              (xử lý email_templates table)
│   │   └── EmailLog.php                   (xử lý email_logs table)
│   │
│   ├── views/
│   │   ├── admin/
│   │   │   ├── customer/
│   │   │   │   ├── list.php              (từ manage_customer.php)
│   │   │   │   └── detail.php
│   │   │   ├── petshop/
│   │   │   │   ├── list.php              (từ manage_petshop.php)
│   │   │   │   └── edit.php
│   │   │   ├── service/
│   │   │   │   ├── list.php              (từ manage_services.php)
│   │   │   │   └── edit.php
│   │   │   ├── review/
│   │   │   │   └── list.php              (từ manage_reviews.php)
│   │   │   └── email/
│   │   │       └── list.php              (từ manage_email.php)
│   │   │
│   │   ├── user/
│   │   │   ├── profile/
│   │   │   │   ├── index.php             (từ profile.php)
│   │   │   │   └── edit.php
│   │   │   ├── petshop/
│   │   │   │   ├── list.php              (từ index.php)
│   │   │   │   └── detail.php            (từ petshop_details.php)
│   │   │   ├── booking/
│   │   │   │   ├── create.php            (từ booking.php)
│   │   │   │   └── list.php              (từ my_bookings.php)
│   │   │   └── wishlist/
│   │   │       └── index.php             (từ wishlist.php)
│   │   │
│   │   ├── auth/
│   │   │   ├── login.php                 (từ login.php)
│   │   │   ├── register.php              (từ register.php)
│   │   │   ├── forgot-password.php       (từ forgot_password.php)
│   │   │   └── reset-password.php        (từ reset_password.php)
│   │   │
│   │   └── layouts/
│   │       ├── header.php                (tách từ các file)
│   │       ├── footer.php                (tách từ các file)
│   │       └── main.php                  (layout chung)
│   │
│   ├── config/
│   │   ├── database.php                  (từ config/db.php)
│   │   ├── email.php                     (từ config/send_email.php)
│   │   └── routes.php                    (file mới để định nghĩa routes)
│   │
│   └── helpers/
│       ├── AuthHelper.php                (xử lý session, login)
│       ├── EmailHelper.php               (từ config/send_email.php)
│       └── FileHelper.php                (xử lý upload file)
│
├── public/
│   ├── index.php                         (entry point mới)
│   ├── css/
│   │   └── styles.css                    (giữ nguyên)
│   ├── js/
│   │   └── scripts.js                    (giữ nguyên)
│   ├── assets/
│   │   ├── images/                       (từ images/)
│   │   ├── avatar/                       (từ avatar/)
│   │   └── uploads/                      (thư mục upload)
│   │
│   └── api/                              (các file xử lý AJAX)
│       ├── user/
│       │   ├── fetch_details.php
│       │   └── toggle_favorite.php
│       ├── petshop/
│       │   └── fetch_deleted.php
│       └── admin/
│           ├── fetch_reviews.php
│           ├── fetch_favorites.php
│           └── fetch_deleted_users.php
│
├── vendor/                               (giữ nguyên)
├── .htaccess                            (cập nhật cho MVC)
├── composer.json                         (giữ nguyên)
└── README.md                            (cập nhật hướng dẫn)



hiện tại tôi có Cấu trúc hiện tại:
Sử dụng PHP thuần (vanilla PHP)
Có kết nối database thông qua PDO
Có xử lý session và authentication
Có phân quyền admin/user
Có xử lý upload file và hình ảnh
Có gửi email (sử dụng PHPMailer)
Các chức năng chính:
Quản lý người dùng (đăng ký, đăng nhập, quên mật khẩu)
Quản lý cửa hàng thú cưng (CRUD)
Quản lý đặt lịch
Quản lý yêu thích
Quản lý đánh giá
Gửi email thông báo
Layout và giao diện:
Header và footer giống nhau cho cả admin và user
CSS được tổ chức trong thư mục css/
JavaScript được tổ chức trong thư mục js/
Hình ảnh được lưu trong nhiều thư mục khác nhau
nhưng tôi để các file rời rạc không có cấu trúc thư mục bây giờ tôi muốn làm lại thành cấu trúc mô hình mvc được không