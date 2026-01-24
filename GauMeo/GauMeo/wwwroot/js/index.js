// Hero slogan
(function() {
  const sloganEl = document.querySelector('.hero-slogan');
  const slogans = [
      'GauMeo Shop – Thế giới thú cưng của bạn.',
      'Sản phẩm chất lượng cho mọi thú cưng.',
      'Dịch vụ chăm sóc thú cưng chuẩn salon!',
      'Cam kết chất lượng - giao hàng tận nơi.',
      'Đồng hành cùng bạn chăm sóc thú cưng.',
      'Mang hạnh phúc đến từng mái ấm.'
  ];
  let current = 0;
  let charIndex = 0;
  let deleting = false;

  function typeSlogan() {
      const text = slogans[current];
      if (!deleting) {
          sloganEl.textContent = text.slice(0, charIndex + 1);
          charIndex++;
          if (charIndex === text.length) {
              deleting = true;
              setTimeout(typeSlogan, 2500);
              return;
          }
      } else {
          sloganEl.textContent = text.slice(0, charIndex - 1);
          charIndex--;
          if (charIndex === 0) {
              deleting = false;
              current = (current + 1) % slogans.length;
          }
      }
      setTimeout(typeSlogan, deleting ? 50 : 90);
  }

  // Chỉ chạy khi DOM đã load
  if (document.readyState === 'loading') {
      document.addEventListener('DOMContentLoaded', typeSlogan);
  } else {
      typeSlogan();
  }
})(); 

// Slider cho sản phẩm bán chạy & khuyến mãi
// Hiển thị theo từng "trang" 4 sản phẩm giống nhau cho mọi block
function setupProductSlider(sliderSelector) {
  document.querySelectorAll(sliderSelector).forEach(function (slider) {
    if (!slider) return;

    const list = slider.querySelector('.product-list');
    if (!list) return;

    // Chỉ lấy các thẻ product-card, bỏ qua các phần tử khác (ví dụ: .no-products)
    const cards = Array.from(list.querySelectorAll('.product-card'));
    const prevBtn = slider.querySelector('.product-slider-btn.prev');
    const nextBtn = slider.querySelector('.product-slider-btn.next');

    const visible = 4;

    // Nếu số sản phẩm ≤ số hiển thị thì không cần slider
    if (!cards.length || cards.length <= visible) {
      if (prevBtn) prevBtn.style.display = 'none';
      if (nextBtn) nextBtn.style.display = 'none';
      return;
    }

    const totalPages = Math.ceil(cards.length / visible);
    let currentPage = 0;

    function update() {
      cards.forEach((card, index) => {
        const pageIndex = Math.floor(index / visible);
        card.style.display = pageIndex === currentPage ? 'flex' : 'none';
      });

      if (prevBtn) {
        const isFirst = currentPage === 0;
        prevBtn.disabled = isFirst;
        prevBtn.style.opacity = isFirst ? '0.3' : '1';
      }

      if (nextBtn) {
        const isLast = currentPage === totalPages - 1;
        nextBtn.disabled = isLast;
        nextBtn.style.opacity = isLast ? '0.3' : '1';
      }
    }

    if (prevBtn) {
      prevBtn.onclick = function () {
        if (currentPage > 0) {
          currentPage--;
          update();
        }
      };
    }

    if (nextBtn) {
      nextBtn.onclick = function () {
        if (currentPage < totalPages - 1) {
          currentPage++;
          update();
        }
      };
    }

    update();
  });
}

// Call setup function when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
  setupProductSlider('.product-slider');
});

// Xử lý tìm kiếm
document.addEventListener('DOMContentLoaded', function() {
  // Xử lý nút xóa tìm kiếm
  const searchClear = document.querySelector('.search-clear');
  const searchInput = document.getElementById('search_topnav');
  
  if (searchClear && searchInput) {
    // Xử lý sự kiện click vào nút xóa
    searchClear.addEventListener('click', function(e) {
      e.preventDefault();
      e.stopPropagation();
      searchInput.value = '';
      searchInput.focus();
    });
    
    // Xử lý form submit
    const searchForm = document.querySelector('.search-form');
    if (searchForm) {
      searchForm.addEventListener('submit', function(e) {
        e.preventDefault();
        if (searchInput.value.trim()) {
          alert('Đã tìm kiếm: ' + searchInput.value);
          // Thêm code điều hướng đến trang kết quả tìm kiếm sau này
          // window.location.href = '/search?q=' + encodeURIComponent(searchInput.value);
        }
      });
    }
  }

  // Khởi tạo các chức năng khác cho trang web
  initScrollAnimations();
  initAddToCartButtons();

  // User dropdown menu hover
  const userWrapper = document.querySelector('.user-dropdown-wrapper');
  const userMenu = userWrapper?.querySelector('.user-dropdown-menu');
  if (!userWrapper || !userMenu) return;

  // Hiện dropdown khi hover
  userWrapper.addEventListener('mouseenter', function() {
    userMenu.classList.add('show');
  });

  // Ẩn dropdown khi mouseleave
  userWrapper.addEventListener('mouseleave', function() {
    userMenu.classList.remove('show');
  });

  // Ẩn dropdown khi click ra ngoài
  document.addEventListener('click', function(e) {
    if (!userWrapper.contains(e.target)) {
      userMenu.classList.remove('show');
    }
  });
});

// Smooth scroll function for hero button
function scrollToOffers() {
  const element = document.getElementById('why-choose-us');
  if (element) {
    const headerHeight = 80; // Chiều cao header để không bị che
    const elementPosition = element.getBoundingClientRect().top;
    const offsetPosition = elementPosition + window.pageYOffset - headerHeight;
    
    window.scrollTo({
      top: offsetPosition,
      behavior: 'smooth'
    });
  }
}

// Intersection Observer cho animations
function initScrollAnimations() {
  const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
  };

  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add('animate-in');
      }
    });
  }, observerOptions);

  // Thêm animation cho offers banner
  const offersSection = document.querySelector('.offers-banner');
  if (offersSection) {
    offersSection.classList.add('fade-in-up');
    observer.observe(offersSection);
  }

  // Thêm animation cho service cards
  const serviceCards = document.querySelectorAll('.service-card');
  serviceCards.forEach((card, index) => {
    card.classList.add('fade-in-up');
    card.style.animationDelay = `${index * 0.1}s`;
    observer.observe(card);
  });

  // Thêm animation cho benefit cards
  const benefitCards = document.querySelectorAll('.benefit-card');
  benefitCards.forEach((card, index) => {
    card.classList.add('fade-in-up');
    card.style.animationDelay = `${index * 0.1}s`;
    observer.observe(card);
  });

  // Thêm animation cho product cards
  const productCards = document.querySelectorAll('.product-card');
  productCards.forEach((card, index) => {
    card.classList.add('fade-in-up');
    card.style.animationDelay = `${(index % 4) * 0.1}s`;
    observer.observe(card);
  });
}

// Thêm scroll to offers function vào window object
window.scrollToOffers = scrollToOffers;

// Add to cart functionality for home page
// Uses global functions from cart-helper.js: addToCart(), addToCartWithVariantCheck(), updateCartBadge(), showCartNotification()
function initAddToCartButtons() {
  const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
  
  addToCartButtons.forEach(button => {
    button.addEventListener('click', function(e) {
      e.preventDefault();
      e.stopPropagation();
      
      const productId = parseInt(this.dataset.productId);
      const productCard = this.closest('.product-card');
      const hasVariants = productCard?.dataset.hasVariants === 'true';
      
      // Use global function from cart-helper.js
      addToCartWithVariantCheck(productId, hasVariants, this);
    });
  });
}

