// Slider banner
(function() {
  var slides = document.querySelectorAll('.slider .slide');
  var current = 0;
  var total = slides.length;
  var interval = null;
  function show(idx) {
    slides.forEach((s, i) => s.classList.toggle('active', i === idx));
    current = idx;
  }
  function next() {
    show((current + 1) % total);
  }
  function prev() {
    show((current - 1 + total) % total);
  }
  document.querySelector('.slider-btn.next').onclick = next;
  document.querySelector('.slider-btn.prev').onclick = prev;
  interval = setInterval(next, 3000);
  // Dừng khi hover slider
  document.querySelector('.slider').addEventListener('mouseenter', function() {
    clearInterval(interval);
  });
  document.querySelector('.slider').addEventListener('mouseleave', function() {
    interval = setInterval(next, 3000);
  });
  show(0);
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
      this.style.display = 'none'; // Ẩn nút xóa sau khi xóa nội dung
    });
    
    // Hiển thị nút xóa khi có nội dung
    searchInput.addEventListener('input', function() {
      searchClear.style.display = this.value ? 'block' : 'none';
    });
    
    // Kiểm tra ban đầu
    searchClear.style.display = 'none';
    
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
  initSlider();
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

// Xử lý slider banner
function initSlider() {
  const slides = document.querySelectorAll('.slide');
  const prevBtn = document.querySelector('.slider-btn.prev');
  const nextBtn = document.querySelector('.slider-btn.next');
  let currentSlide = 0;
  
  if (!slides.length || !prevBtn || !nextBtn) return;
  
  function showSlide(index) {
    slides.forEach(slide => slide.classList.remove('active'));
    slides[index].classList.add('active');
  }
  
  prevBtn.addEventListener('click', function() {
    currentSlide = currentSlide > 0 ? currentSlide - 1 : slides.length - 1;
    showSlide(currentSlide);
  });
  
  nextBtn.addEventListener('click', function() {
    currentSlide = currentSlide < slides.length - 1 ? currentSlide + 1 : 0;
    showSlide(currentSlide);
  });
  
  // Auto slide
  setInterval(function() {
    currentSlide = currentSlide < slides.length - 1 ? currentSlide + 1 : 0;
    showSlide(currentSlide);
  }, 5000);
}

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