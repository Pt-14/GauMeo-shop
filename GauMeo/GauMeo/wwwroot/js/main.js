// Xử lý slider
document.addEventListener('DOMContentLoaded', function() {
  // Slider sản phẩm
  const productSlider = document.querySelector('.product-slider');
  if (productSlider) {
    const productList = productSlider.querySelector('.product-list');
    const prevBtn = productSlider.querySelector('.prev');
    const nextBtn = productSlider.querySelector('.next');
    const products = productList.querySelectorAll('.product-card');
    let currentPosition = 0;
    const productsPerView = 4;
    const maxPosition = Math.max(0, products.length - productsPerView);

    function updateSliderPosition() {
      const cardWidth = products[0].offsetWidth;
      const gap = 20; // Khoảng cách giữa các sản phẩm
      const offset = currentPosition * (cardWidth + gap);
      productList.style.transform = `translateX(-${offset}px)`;
      
      // Cập nhật trạng thái nút
      if (prevBtn) prevBtn.disabled = currentPosition === 0;
      if (nextBtn) nextBtn.disabled = currentPosition >= maxPosition;
    }

    if (prevBtn) {
      prevBtn.addEventListener('click', () => {
        if (currentPosition > 0) {
          currentPosition--;
          updateSliderPosition();
        }
      });
    }

    if (nextBtn) {
      nextBtn.addEventListener('click', () => {
        if (currentPosition < maxPosition) {
          currentPosition++;
          updateSliderPosition();
        }
      });
    }

    // Cập nhật vị trí khi resize window
    window.addEventListener('resize', updateSliderPosition);
    updateSliderPosition();
  }
});

// Xử lý dropdown menu
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

// Đã di chuyển logic xử lý giỏ hàng vào index.js