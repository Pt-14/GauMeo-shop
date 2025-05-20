// Xử lý slider
document.addEventListener('DOMContentLoaded', function() {
  // Slider chính
  const slider = document.querySelector('.slider');
  if (slider) {
    const slides = slider.querySelectorAll('.slide');
    const prevBtn = slider.querySelector('.prev');
    const nextBtn = slider.querySelector('.next');
    let currentSlide = 0;

    function showSlide(index) {
      slides.forEach(slide => slide.classList.remove('active'));
      slides[index].classList.add('active');
    }

    function nextSlide() {
      currentSlide = (currentSlide + 1) % slides.length;
      showSlide(currentSlide);
    }

    function prevSlide() {
      currentSlide = (currentSlide - 1 + slides.length) % slides.length;
      showSlide(currentSlide);
    }

    if (prevBtn) prevBtn.addEventListener('click', prevSlide);
    if (nextBtn) nextBtn.addEventListener('click', nextSlide);

    // Tự động chuyển slide
    setInterval(nextSlide, 5000);
  }

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
        e.preventDefault();
        menu.classList.toggle('show');
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

// Xử lý giỏ hàng
document.addEventListener('DOMContentLoaded', function() {
  const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
  const cartBadge = document.querySelector('.header-cart-badge');
  let cartCount = parseInt(cartBadge?.textContent || '0');

  addToCartButtons.forEach(button => {
    button.addEventListener('click', () => {
      cartCount++;
      if (cartBadge) cartBadge.textContent = cartCount;
      
      // Thêm animation
      button.classList.add('added');
      setTimeout(() => button.classList.remove('added'), 1000);
    });
  });
}); 