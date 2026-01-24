// Xử lý dropdown menu cho header navigation
document.addEventListener('DOMContentLoaded', function() {
  const dropdowns = document.querySelectorAll('.nav-dropdown');
  
  dropdowns.forEach(dropdown => {
    const link = dropdown.querySelector('a');
    const menu = dropdown.querySelector('.dropdown-menu');
    
    if (link && menu) {
      link.addEventListener('click', (e) => {
        // Chỉ preventDefault nếu href là '#'
        if (link.getAttribute('href') === '#') {
          e.preventDefault();
          menu.classList.toggle('show');
        }
        // Nếu là link thật (ví dụ /Brand/Index) thì cho phép chuyển trang
      });
      // Đóng menu khi click ra ngoài
      document.addEventListener('click', (e) => {
        if (!dropdown.contains(e.target)) {
          menu.classList.remove('show');
        }
      });
    }
  });
});

// Lưu ý:
// - Logic slider sản phẩm và xử lý giỏ hàng đã được di chuyển sang index.js