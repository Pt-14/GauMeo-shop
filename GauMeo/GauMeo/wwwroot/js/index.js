// Hero slogan
(function() {
  const sloganEl = document.querySelector('.hero-slogan');
  const slogans = [
      'Sản phẩm chất lượng cho mọi thú cưng.',
      'Đồng hành cùng bạn chăm sóc thú cưng.',
      'Đảm bảo chất lượng và giao hàng tận nơi.',
      'Mang hạnh phúc đến từng mái ấm.',
      'GauMeo Shop – Thế giới thú cưng của bạn.'
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
function setupProductSlider(sliderSelector) {
  document.querySelectorAll(sliderSelector).forEach(function(slider) {
    const list = slider.querySelector('.product-list');
    const cards = Array.from(list.children);
    const prevBtn = slider.querySelector('.product-slider-btn.prev');
    const nextBtn = slider.querySelector('.product-slider-btn.next');
    const visible = 4;
    let start = 0;
    function update() {
      cards.forEach((card, i) => {
        card.style.display = (i >= start && i < start + visible) ? 'flex' : 'none';
      });
      prevBtn.disabled = start === 0;
      nextBtn.disabled = start + visible >= cards.length;
    }
    prevBtn.onclick = function() {
      if (start > 0) {
        start--;
        update();
      }
    };
    nextBtn.onclick = function() {
      if (start + visible < cards.length) {
        start++;
        update();
      }
    };
    update();
  });
}
setupProductSlider('.product-slider');

// DEMO ALERT FOR NON-WORKING BUTTONS
document.querySelectorAll('a[href="#"], .footer-newsletter button, .footer-col a, .service-card, .category-card').forEach(function(el) {
  el.addEventListener('click', function(e) {
    e.preventDefault();
    alert('Chức năng này chỉ là demo!');
  });
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
  initProductSliders();

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

// Xử lý product slider
function initProductSliders() {
  const sliders = document.querySelectorAll('.product-slider');
  
  sliders.forEach(slider => {
    const prevBtn = slider.querySelector('.prev');
    const nextBtn = slider.querySelector('.next');
    const productList = slider.querySelector('.product-list');
    
    if (!prevBtn || !nextBtn || !productList) return;
    
    // Bật/tắt nút prev
    function updateButtonStates() {
      if (productList.scrollLeft <= 0) {
        prevBtn.setAttribute('disabled', 'disabled');
      } else {
        prevBtn.removeAttribute('disabled');
      }
      
      const isAtEnd = productList.scrollLeft + productList.clientWidth >= productList.scrollWidth - 5;
      if (isAtEnd) {
        nextBtn.setAttribute('disabled', 'disabled');
      } else {
        nextBtn.removeAttribute('disabled');
      }
    }
    
    // Xử lý sự kiện click nút
    prevBtn.addEventListener('click', function() {
      productList.scrollBy({ left: -300, behavior: 'smooth' });
      setTimeout(updateButtonStates, 500);
    });
    
    nextBtn.addEventListener('click', function() {
      productList.scrollBy({ left: 300, behavior: 'smooth' });
      setTimeout(updateButtonStates, 500);
    });
    
    // Khởi tạo trạng thái nút
    updateButtonStates();
    
    // Theo dõi sự kiện scroll
    productList.addEventListener('scroll', updateButtonStates);
  });
}

// FAQ Accordion functionality
function initFAQ() {
  const faqItems = document.querySelectorAll('.faq-item');
  faqItems.forEach(item => {
    const question = item.querySelector('.faq-question');
    const answer = item.querySelector('.faq-answer');
    if (!question || !answer) return;
    question.addEventListener('click', function() {
      const isActive = item.classList.contains('active');
      // Close all others
      faqItems.forEach(otherItem => {
        if (otherItem !== item) {
          otherItem.classList.remove('active');
          const otherAnswer = otherItem.querySelector('.faq-answer');
          const otherBtn = otherItem.querySelector('.faq-question');
          if (otherAnswer) otherAnswer.style.maxHeight = '0';
          if (otherBtn) otherBtn.setAttribute('aria-expanded', 'false');
        }
      });
      // Toggle current
      if (isActive) {
        item.classList.remove('active');
        answer.style.maxHeight = '0';
        question.setAttribute('aria-expanded', 'false');
      } else {
        item.classList.add('active');
        answer.style.maxHeight = 'none';
        const contentHeight = answer.scrollHeight;
        answer.style.maxHeight = '0';
        setTimeout(() => {
          answer.style.maxHeight = contentHeight + 'px';
        }, 10);
        question.setAttribute('aria-expanded', 'true');
      }
    });
  });
}

// Khởi tạo FAQ khi DOM đã load
document.addEventListener('DOMContentLoaded', function() {
  initFAQ();
});

// Khởi tạo FAQ ngay lập tức nếu DOM đã sẵn sàng
if (document.readyState === 'loading') {
  document.addEventListener('DOMContentLoaded', initFAQ);
} else {
  initFAQ();
}
