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
function setupProductSlider(sliderSelector) {
  document.querySelectorAll(sliderSelector).forEach(function(slider) {
    const list = slider.querySelector('.product-list');
    const cards = Array.from(list.children);
    const prevBtn = slider.querySelector('.product-slider-btn.prev');
    const nextBtn = slider.querySelector('.product-slider-btn.next');
    
    // Skip if no cards or less than 5 cards (no need for slider)
    if (!cards.length || cards.length <= 4) {
      if (prevBtn) prevBtn.style.display = 'none';
      if (nextBtn) nextBtn.style.display = 'none';
      return;
    }
    
    const visible = 4;
    let start = 0;
    
    function update() {
      cards.forEach((card, i) => {
        card.style.display = (i >= start && i < start + visible) ? 'flex' : 'none';
      });
      
      if (prevBtn) {
        prevBtn.disabled = start === 0;
        prevBtn.style.opacity = start === 0 ? '0.3' : '1';
      }
      
      if (nextBtn) {
        nextBtn.disabled = start + visible >= cards.length;
        nextBtn.style.opacity = start + visible >= cards.length ? '0.3' : '1';
      }
    }
    
    if (prevBtn) {
      prevBtn.onclick = function() {
        if (start > 0) {
          start--;
          update();
        }
      };
    }
    
    if (nextBtn) {
      nextBtn.onclick = function() {
        if (start + visible < cards.length) {
          start++;
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
function initAddToCartButtons() {
  const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
  
  addToCartButtons.forEach(button => {
    button.addEventListener('click', async function(e) {
      e.preventDefault();
      e.stopPropagation();
      
      const productId = parseInt(this.dataset.productId);
      const productCard = this.closest('.product-card');
      const hasVariants = productCard.dataset.hasVariants === 'true';
      
      if (hasVariants) {
        // Nếu sản phẩm có biến thể, chuyển đến trang chi tiết
        window.location.href = `/Product/Detail/${productId}`;
        return;
      }
      
      // Nếu không có biến thể, thêm trực tiếp vào giỏ hàng
      await addToCartDirectly(productId, this);
    });
  });
}

async function addToCartDirectly(productId, button) {
  try {
    // Show loading state
    const originalText = button.innerHTML;
    button.innerHTML = '<span style="display: inline-flex; align-items: center; gap: 5px;"><svg width="16" height="16" fill="currentColor" class="animate-spin" viewBox="0 0 16 16"><path d="M11.534 7h3.932a.25.25 0 0 1 .192.41l-1.966 2.36a.25.25 0 0 1-.384 0l-1.966-2.36a.25.25 0 0 1 .192-.41zm-11 2h3.932a.25.25 0 0 0 .192-.41L2.692 6.23a.25.25 0 0 0-.384 0L.342 8.59A.25.25 0 0 0 .534 9z"/><path fill-rule="evenodd" d="M8 3c-1.552 0-2.94.707-3.857 1.818a.5.5 0 1 1-.771-.636A6.002 6.002 0 0 1 13.917 7H12.9A5.002 5.002 0 0 0 8 3zM3.1 9a5.002 5.002 0 0 0 8.757 2.182.5.5 0 1 1 .771.636A6.002 6.002 0 0 1 2.083 9H3.1z"/></svg>Đang thêm...</span>';
    button.disabled = true;

    const response = await fetch('/Cart/AddToCart', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'RequestVerificationToken': getAntiForgeryToken()
      },
      body: JSON.stringify({
        productId: productId,
        quantity: 1,
        selectedVariants: {}
      })
    });

    const data = await response.json();

    if (data.success) {
      // Update cart count in header
      updateCartBadge(data.cartCount);
      
      // Show success state
      button.innerHTML = '<span style="display: inline-flex; align-items: center; gap: 5px;"><svg width="16" height="16" fill="currentColor" viewBox="0 0 16 16"><path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425a.247.247 0 0 1 .02-.022Z"/></svg>Đã thêm</span>';
      button.style.background = '#28a745';
      
      // Show notification
      showNotification(data.message || 'Đã thêm sản phẩm vào giỏ hàng!', 'success');
      
      // Reset button after 2 seconds
      setTimeout(() => {
        button.innerHTML = originalText;
        button.style.background = '';
        button.disabled = false;
      }, 2000);
    } else {
      throw new Error(data.message || 'Không thể thêm sản phẩm vào giỏ hàng!');
    }
  } catch (error) {
    console.error('Error adding to cart:', error);
    
    // Show error state
    button.innerHTML = '<span style="display: inline-flex; align-items: center; gap: 5px;"><svg width="16" height="16" fill="currentColor" viewBox="0 0 16 16"><path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/><path d="M7.002 11a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 4.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 4.995z"/></svg>Lỗi</span>';
    button.style.background = '#dc3545';
    
    showNotification(error.message, 'error');
    
    // Reset button after 2 seconds
    setTimeout(() => {
      button.innerHTML = '<svg class="cart-icon" width="16" height="16" viewBox="0 0 576 512" fill="currentColor"><path d="M528.12 301.319l47.273-208C578.806 78.301 567.391 64 551.99 64H159.208l-9.166-44.81C147.758 8.021 137.93 0 126.529 0H24C10.745 0 0 10.745 0 24v16c0 13.255 10.745 24 24 24h69.883l70.248 343.435C147.325 417.1 136 435.222 136 456c0 30.928 25.072 56 56 56s56-25.072 56-56c0-15.674-6.447-29.835-16.824-40h209.647C430.447 426.165 424 440.326 424 456c0 30.928 25.072 56 56 56s56-25.072 56-56c0-22.172-12.888-41.332-31.579-50.405l5.517-24.276c3.413-15.018-8.002-29.319-23.403-29.319H218.117l-6.545-32h293.145c11.206 0 20.92-7.754 23.403-18.681z"></path></svg>Thêm vào giỏ hàng';
      button.style.background = '';
      button.disabled = false;
    }, 2000);
  }
}

function getAntiForgeryToken() {
  const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
  return tokenInput ? tokenInput.value : '';
}

function updateCartBadge(count) {
  // Update all cart badges (like in product-detail.js)
  const cartBadges = document.querySelectorAll('.header-cart-badge, .cart-count, .cart-badge');
  cartBadges.forEach(badge => {
    badge.textContent = count || 0;
    if (count && count > 0) {
      badge.classList.add('show');
      badge.style.display = 'flex';
    } else {
      badge.classList.remove('show');
      badge.style.display = 'none';
    }
  });
  
  // Add animation to header cart badge
  const headerCartBadge = document.querySelector('.header-cart-badge');
  if (headerCartBadge && count > 0) {
    headerCartBadge.style.transform = 'scale(1.3)';
    setTimeout(() => {
      headerCartBadge.style.transform = 'scale(1)';
    }, 200);
  }
}

function showNotification(message, type = 'success') {
  // Remove existing notifications
  const existingNotifications = document.querySelectorAll('.home-notification');
  existingNotifications.forEach(n => n.remove());

  const notification = document.createElement('div');
  notification.className = `home-notification ${type}`;
  notification.innerHTML = `
    <div class="notification-content">
      <div class="notification-icon">
        ${type === 'success' ? 
          '<svg width="20" height="20" fill="currentColor" viewBox="0 0 16 16"><path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425a.247.247 0 0 1 .02-.022Z"/></svg>' : 
          '<svg width="20" height="20" fill="currentColor" viewBox="0 0 16 16"><path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/><path d="M7.002 11a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 4.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 4.995z"/></svg>'
        }
      </div>
      <span class="notification-message">${message}</span>
      <button class="notification-close" onclick="this.parentElement.parentElement.remove()">
        <svg width="16" height="16" fill="currentColor" viewBox="0 0 16 16"><path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z"/></svg>
      </button>
    </div>
  `;

  // Add styles
  notification.style.cssText = `
    position: fixed;
    top: 20px;
    right: 20px;
    z-index: 10000;
    max-width: 400px;
    background: ${type === 'success' ? '#d4edda' : '#f8d7da'};
    color: ${type === 'success' ? '#155724' : '#721c24'};
    border: 1px solid ${type === 'success' ? '#c3e6cb' : '#f5c6cb'};
    border-radius: 8px;
    padding: 12px 16px;
    box-shadow: 0 4px 12px rgba(0,0,0,0.15);
    opacity: 0;
    transform: translateX(100%);
    transition: all 0.3s ease;
  `;

  notification.querySelector('.notification-content').style.cssText = `
    display: flex;
    align-items: center;
    gap: 10px;
  `;

  notification.querySelector('.notification-close').style.cssText = `
    background: none;
    border: none;
    cursor: pointer;
    padding: 4px;
    border-radius: 4px;
    margin-left: auto;
    opacity: 0.7;
    transition: opacity 0.2s ease;
  `;

  document.body.appendChild(notification);

  // Animate in
  requestAnimationFrame(() => {
    notification.style.opacity = '1';
    notification.style.transform = 'translateX(0)';
  });

  // Auto remove after 4 seconds
  setTimeout(() => {
    if (notification.parentElement) {
      notification.style.opacity = '0';
      notification.style.transform = 'translateX(100%)';
      setTimeout(() => notification.remove(), 300);
    }
  }, 4000);
}

