// Product Detail JavaScript - Clean Version (No Question functionality)
document.addEventListener('DOMContentLoaded', function() {
  // ===========================================
  // 1. VARIANT SELECTION & PRICE UPDATE
  // ===========================================
  const variantButtons = document.querySelectorAll('.variant-btn');
  const currentPriceEl = document.querySelector('.current-price');
  const originalPriceEl = document.querySelector('.original-price');
  const discountEl = document.querySelector('.discount');

  // Initialize - variants are already selected from server-side rendering
  updatePrice();
  setupVariantSelection();
  setupQuantityControls();
  setupThumbnailNavigation();
  setupProductInfoTabs();
  setupReviewTabs();
  setupRelatedProductsSlider();
  setupWishlistFunctionality();
  setupAddToCartFunctionality();
  setupReviewSystem();

  function updatePrice() {
    const selectedVariants = document.querySelectorAll('.variant-btn.selected');
    console.log('updatePrice called, selected variants:', selectedVariants.length);
    
    if (selectedVariants.length > 0) {
      // Calculate total price adjustments from all selected variants
      let totalPriceAdjustment = 0;
      let basePrice = 0;
      
      // Get base price from the first variant (should be the same for all variants of same product)
      const firstVariant = selectedVariants[0];
      if (firstVariant && firstVariant.dataset.originalPrice) {
        basePrice = parseInt(firstVariant.dataset.originalPrice);
        console.log('Base price from first variant:', basePrice);
      }
      
      // Sum up all price adjustments from selected variants
      selectedVariants.forEach((variant, index) => {
        if (variant.dataset.priceAdjustment) {
          const adjustment = parseInt(variant.dataset.priceAdjustment);
          totalPriceAdjustment += adjustment;
          console.log(`Variant ${index + 1} adjustment:`, adjustment);
        }
      });
      
      // Calculate final price (base price + adjustments)
      const finalPrice = basePrice + totalPriceAdjustment;
      console.log('Final price calculation:', basePrice, '+', totalPriceAdjustment, '=', finalPrice);
      
      // Check if product has discount from the visible discount element
      const discountEl = document.querySelector('.discount');
      const hasDiscount = discountEl && discountEl.style.display !== 'none' && discountEl.textContent;
      
      if (hasDiscount) {
        const discountPercent = parseInt(discountEl.textContent.match(/\d+/)?.[0] || 0);
        console.log('Has discount:', discountPercent + '%');
        
        // Calculate discounted price for this variant combination
        const discountedPrice = Math.round(finalPrice * (100 - discountPercent) / 100);
        
        // Update display: show original price crossed out and discounted price
        if (originalPriceEl) {
          originalPriceEl.textContent = finalPrice.toLocaleString('vi-VN') + 'đ';
          originalPriceEl.style.display = 'inline';
          console.log('Updated original price display:', originalPriceEl.textContent);
        }
        
        if (currentPriceEl) {
          currentPriceEl.textContent = discountedPrice.toLocaleString('vi-VN') + 'đ';
          console.log('Updated current price display:', currentPriceEl.textContent);
        }
        
        if (discountEl) {
          discountEl.style.display = 'inline';
        }
      } else {
        // No discount: show only final variant price
        console.log('No discount, showing final price:', finalPrice);
        
        if (currentPriceEl) {
          currentPriceEl.textContent = finalPrice.toLocaleString('vi-VN') + 'đ';
          console.log('Updated current price display:', currentPriceEl.textContent);
        }
        
        // Hide discount elements
        if (originalPriceEl) originalPriceEl.style.display = 'none';
        if (discountEl) discountEl.style.display = 'none';
      }
    } else {
      // No variants selected - this shouldn't happen normally since variants are pre-selected
      console.log('No variants selected');
    }
  }

  // ===========================================
  // 2. THUMBNAIL NAVIGATION
  // ===========================================
  function setupThumbnailNavigation() {
    const thumbImages = document.querySelectorAll('.thumb-list img');
    const thumbUpBtn = document.querySelector('.thumb-up');
    const thumbDownBtn = document.querySelector('.thumb-down');
    const mainImg = document.querySelector('.main-img');
    const visibleCount = 8;
    let currentIndex = 0;

    function updateThumbnailView() {
      thumbImages.forEach((img, index) => {
        img.style.display = (index >= currentIndex && index < currentIndex + visibleCount) ? 'block' : 'none';
      });
      
      if (thumbUpBtn && thumbDownBtn) {
        // Hide navigation buttons if 8 or fewer images
        if (thumbImages.length <= visibleCount) {
          thumbUpBtn.classList.add('hidden');
          thumbDownBtn.classList.add('hidden');
        } else {
          thumbUpBtn.classList.remove('hidden');
          thumbDownBtn.classList.remove('hidden');
          thumbUpBtn.disabled = currentIndex === 0;
          thumbDownBtn.disabled = currentIndex + visibleCount >= thumbImages.length;
        }
      }
    }

    if (thumbUpBtn) {
      thumbUpBtn.addEventListener('click', function() {
        if (currentIndex > 0) {
          currentIndex--;
          updateThumbnailView();
        }
      });
    }

    if (thumbDownBtn) {
      thumbDownBtn.addEventListener('click', function() {
        if (currentIndex + visibleCount < thumbImages.length) {
          currentIndex++;
          updateThumbnailView();
        }
      });
    }

    // Thumbnail click to change main image
    thumbImages.forEach(img => {
      img.addEventListener('click', function() {
        thumbImages.forEach(thumb => thumb.classList.remove('active'));
        this.classList.add('active');
        if (mainImg) {
          mainImg.src = this.src;
        }
      });
    });

    // Initialize thumbnail navigation
    if (thumbImages.length > 0) {
      updateThumbnailView();
    }
  }

  // ===========================================
  // 3. PRODUCT INFO TABS
  // ===========================================
  function setupProductInfoTabs() {
    const tabBtns = document.querySelectorAll('.tab-btn');
    const tabContents = document.querySelectorAll('.tab-content');
    
    tabBtns.forEach(btn => {
      btn.addEventListener('click', function() {
        // Remove active class from all buttons and contents
        tabBtns.forEach(b => b.classList.remove('active'));
        tabContents.forEach(c => c.classList.remove('active'));
        
        // Add active class to clicked button
        this.classList.add('active');
        
        // Show corresponding content
        const tabId = this.getAttribute('data-tab');
        document.getElementById(tabId).classList.add('active');
      });
    });
  }

  // ===========================================
  // 4. ACCORDION FUNCTIONALITY
  // ===========================================
  const accordionTitles = document.querySelectorAll('.accordion-title');
  
  accordionTitles.forEach(title => {
    title.addEventListener('click', function() {
      const item = this.parentElement;
      const iconOpen = this.querySelector('.icon-open');
      const iconClose = this.querySelector('.icon-close');
      
      item.classList.toggle('open');
      
      if (item.classList.contains('open')) {
        if (iconOpen) iconOpen.style.display = 'none';
        if (iconClose) iconClose.style.display = 'block';
      } else {
        if (iconOpen) iconOpen.style.display = 'block';
        if (iconClose) iconClose.style.display = 'none';
      }
    });
  });

  // Initialize accordion icons
  document.querySelectorAll('.accordion-item').forEach(item => {
    const iconOpen = item.querySelector('.icon-open');
    const iconClose = item.querySelector('.icon-close');
    
    if (item.classList.contains('open')) {
      if (iconOpen) iconOpen.style.display = 'none';
      if (iconClose) iconClose.style.display = 'block';
    } else {
      if (iconOpen) iconOpen.style.display = 'block';
      if (iconClose) iconClose.style.display = 'none';
    }
  });

  // ===========================================
  // 5. REVIEW TABS (Question functionality removed)
  // ===========================================
  function setupReviewTabs() {
    const tabItems = document.querySelectorAll('.tab-item[role="tab"]');
    const tabContents = document.querySelectorAll('.item.content');
    const reviewTrigger = document.querySelector('.review-container-trigger');
    const reviewContainer = document.querySelector('.review-form-container');
    const reviewForms = document.querySelector('.review-forms');
    const sectionTitle = document.getElementById('section-title');
    const sectionCount = document.getElementById('section-count');
    const ratingStars = document.getElementById('rating-stars-display');
    
    // Track form state
    let isReviewFormOpen = false;
    
    // Initialize - show review form container by default but closed
    if (reviewContainer) {
      reviewContainer.classList.add('active');
    }
    
    // Review button click handler
    if (reviewTrigger) {
      reviewTrigger.addEventListener('click', function(e) {
        e.preventDefault();
        
        // Update section title and show stars
        if (sectionTitle) sectionTitle.textContent = 'Đánh giá';
        if (sectionCount) sectionCount.textContent = '(154 Đánh giá)';
        if (ratingStars) {
          ratingStars.classList.remove('hide-stars');
          ratingStars.classList.add('show-stars');
        }
        
        // Update button states
        reviewTrigger.classList.add('active');
        
        // Toggle review form
        if (isReviewFormOpen) {
          // Close review form
          if (reviewForms) {
            reviewForms.classList.remove('show');
            setTimeout(() => {
              if (reviewContainer) reviewContainer.classList.remove('active');
            }, 400);
          }
          isReviewFormOpen = false;
        } else {
          // Open review form
          if (reviewForms && reviewContainer) {
            reviewContainer.classList.add('active');
            setTimeout(() => {
              reviewForms.classList.add('show');
            }, 50);
          }
          isReviewFormOpen = true;
        }
      });
    }
    
    // Tab switching for reviews content only
    tabItems.forEach(function(tab) {
      tab.addEventListener('click', function(e) {
        e.preventDefault();
        
        const targetId = tab.getAttribute('aria-controls');
        
        // Update tab states
        tabItems.forEach(function(t) {
          t.classList.remove('active');
          t.setAttribute('aria-selected', 'false');
          t.setAttribute('aria-expanded', 'false');
          
          // Update checkbox display
          const filledCheckbox = t.querySelector('.checkbox-filled');
          const outlineCheckbox = t.querySelector('.checkbox-outline');
          if (filledCheckbox) filledCheckbox.style.display = 'none';
          if (outlineCheckbox) outlineCheckbox.style.display = 'block';
        });
        
        tab.classList.add('active');
        tab.setAttribute('aria-selected', 'true');
        tab.setAttribute('aria-expanded', 'true');
        
        // Update checkbox display for active tab
        const activeFilledCheckbox = tab.querySelector('.checkbox-filled');
        const activeOutlineCheckbox = tab.querySelector('.checkbox-outline');
        if (activeFilledCheckbox) activeFilledCheckbox.style.display = 'block';
        if (activeOutlineCheckbox) activeOutlineCheckbox.style.display = 'none';
        
        // Switch content
        tabContents.forEach(function(content) {
          content.style.display = 'none';
        });
        
        const targetContent = document.getElementById(targetId);
        if (targetContent) {
          targetContent.style.display = 'block';
        }
      });
    });
    
    // Star rating functionality
    const stars = document.querySelectorAll('#star-review-rating .star');
    let selectedRating = 0;
    
    stars.forEach(function(star, index) {
      star.addEventListener('click', function(e) {
        e.preventDefault();
        selectedRating = index + 1;
        updateStarDisplay();
      });
      
      star.addEventListener('mouseenter', function() {
        highlightStars(index + 1);
      });
    });
    
    const starContainer = document.getElementById('star-review-rating');
    if (starContainer) {
      starContainer.addEventListener('mouseleave', function() {
        updateStarDisplay();
      });
    }
    
    function highlightStars(rating) {
      stars.forEach(function(star, index) {
        if (index < rating) {
          star.classList.add('star-full');
          star.classList.remove('star-empty');
        } else {
          star.classList.add('star-empty');
          star.classList.remove('star-full');
        }
      });
    }
    
    function updateStarDisplay() {
      highlightStars(selectedRating);
    }
    
    // Image upload and preview functionality for review only
    setupImagePreview('review-file', 'review-images-preview');
  }

  // ===========================================
  // 6. IMAGE PREVIEW FUNCTIONALITY
  // ===========================================
  function setupImagePreview(fileInputId, previewContainerId) {
    const fileInput = document.getElementById(fileInputId);
    const previewContainer = document.getElementById(previewContainerId);
    let filesArray = [];
    
    if (!fileInput || !previewContainer) return;
    
    fileInput.addEventListener('change', function(e) {
      const files = Array.from(e.target.files);
      
      files.forEach(file => {
        if (file.type.startsWith('image/')) {
          filesArray.push(file);
          addImagePreview(file, previewContainer, filesArray);
        }
      });
    });
    
    function addImagePreview(file, container, filesArray) {
      const reader = new FileReader();
      
      reader.onload = function(e) {
        const previewDiv = document.createElement('div');
        previewDiv.className = 'image-preview';
        
        const img = document.createElement('img');
        img.src = e.target.result;
        img.alt = file.name;
        
        const removeBtn = document.createElement('button');
        removeBtn.type = 'button';
        removeBtn.className = 'remove-image';
        removeBtn.innerHTML = '×';
        
        removeBtn.addEventListener('click', function() {
          const index = filesArray.indexOf(file);
          if (index > -1) {
            filesArray.splice(index, 1);
          }
          previewDiv.remove();
        });
        
        previewDiv.appendChild(img);
        previewDiv.appendChild(removeBtn);
        container.appendChild(previewDiv);
      };
      
      reader.readAsDataURL(file);
    }
  }

  // ===========================================
  // 7. RELATED PRODUCTS SLIDER
  // ===========================================
  function setupRelatedProductsSlider() {
    const productList = document.querySelector('.product-list');
    const prevBtn = document.querySelector('.product-slider-btn.prev');
    const nextBtn = document.querySelector('.product-slider-btn.next');
    
    if (!productList || !prevBtn || !nextBtn) return;
    
      function updateSliderButtons() {
        const scrollLeft = productList.scrollLeft;
        const maxScroll = productList.scrollWidth - productList.clientWidth;
        
        prevBtn.disabled = scrollLeft <= 0;
        nextBtn.disabled = scrollLeft >= maxScroll;
      }
      
      prevBtn.addEventListener('click', function() {
        productList.scrollBy({
          left: -300,
          behavior: 'smooth'
        });
      });
      
      nextBtn.addEventListener('click', function() {
        productList.scrollBy({
          left: 300,
          behavior: 'smooth'
        });
      });
      
      productList.addEventListener('scroll', updateSliderButtons);
      updateSliderButtons();
  }

  // ===========================================
  // 8. VARIANT SELECTION
  // ===========================================
  function setupVariantSelection() {
    if (variantButtons.length > 0) {
      variantButtons.forEach(btn => {
        btn.addEventListener('click', function() {
          const variantType = this.dataset.variantType;
          
          // Remove selected class from other variants of the same type
          document.querySelectorAll(`.variant-btn[data-variant-type="${variantType}"]`)
            .forEach(b => b.classList.remove('selected'));
          
          // Add selected class to clicked variant
          this.classList.add('selected');
          
          // Update price if this variant affects price
          updatePrice();
        });
      });
    }
  }

  // ===========================================
  // 9. QUANTITY CONTROLS
  // ===========================================
  function setupQuantityControls() {
    const qtyInput = document.querySelector('.qty-input');
    const minusBtn = document.querySelector('.qty-btn.minus');
    const plusBtn = document.querySelector('.qty-btn.plus');
    
    function updateQuantityButtons() {
      if (!qtyInput || !minusBtn || !plusBtn) return;
      
      const currentQty = parseInt(qtyInput.value);
      const minQty = parseInt(qtyInput.min) || 1;
      const maxQty = parseInt(qtyInput.max) || 999;
      
      minusBtn.disabled = currentQty <= minQty;
      plusBtn.disabled = currentQty >= maxQty;
    }
    
    if (minusBtn) {
      minusBtn.addEventListener('click', function() {
        if (qtyInput) {
          const currentQty = parseInt(qtyInput.value);
          const minQty = parseInt(qtyInput.min) || 1;
          
          if (currentQty > minQty) {
            qtyInput.value = currentQty - 1;
            updateQuantityButtons();
          }
        }
      });
    }
    
    if (plusBtn) {
      plusBtn.addEventListener('click', function() {
        if (qtyInput) {
          const currentQty = parseInt(qtyInput.value);
          const maxQty = parseInt(qtyInput.max) || 999;
          
          if (currentQty < maxQty) {
            qtyInput.value = currentQty + 1;
            updateQuantityButtons();
          }
        }
      });
    }
    
    if (qtyInput) {
      qtyInput.addEventListener('input', updateQuantityButtons);
      updateQuantityButtons(); // Initialize
    }
  }

  // ===========================================
  // 10. WISHLIST FUNCTIONALITY
  // ===========================================
  function setupWishlistFunctionality() {
    const favoriteBtn = document.querySelector('.favorite-btn');
    
    if (!favoriteBtn) return;
    
    const productId = favoriteBtn.getAttribute('data-product-id');
    const productName = favoriteBtn.getAttribute('data-product-name');
    
    // Load initial state
    loadWishlistCount();
    checkWishlistStatus(productId);
    
    favoriteBtn.addEventListener('click', function(e) {
      e.preventDefault();
      toggleWishlist(productId, productName);
    });
  }
  
  function loadWishlistCount() {
    fetch('/api/wishlist/count')
      .then(response => {
        if (response.status === 401 || response.status === 404) {
          updateWishlistCounter(0);
          return null;
        }
        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
          updateWishlistCounter(0);
          return null;
        }
        return response.json();
      })
      .then(data => {
        if (data) {
          updateWishlistCounter(data.count || 0);
        }
      })
      .catch(error => {
        updateWishlistCounter(0);
      });
  }
  
  function checkWishlistStatus(productId) {
    if (!productId) return;
    
    fetch(`/api/wishlist/check/${productId}`)
      .then(response => {
        if (response.status === 401) {
          showNotification('Vui lòng đăng nhập để thêm vào danh sách yêu thích', 'error');
          return null;
        }
        if (response.status === 404) {
          return null;
        }
        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
          return null;
        }
        return response.json();
      })
      .then(data => {
        if (data) {
          updateWishlistButtonState(data.isInWishlist);
        }
      })
      .catch(error => {
        updateWishlistButtonState(false);
      });
  }
  
  function toggleWishlist(productId, productName) {
    if (!productId) return;
    
    const favoriteBtn = document.querySelector('.favorite-btn');
    const isCurrentlyInWishlist = favoriteBtn.classList.contains('in-wishlist');
    
    const url = isCurrentlyInWishlist ? '/api/wishlist/remove' : '/api/wishlist/add';
    const method = 'POST';
    
    fetch(url, {
      method: method,
      headers: {
        'Content-Type': 'application/json',
        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
      },
      body: JSON.stringify({ productId: parseInt(productId) })
    })
    .then(response => {
      if (response.status === 401) {
        showNotification('Vui lòng đăng nhập để thêm vào danh sách yêu thích', 'error');
        return null;
      }
      if (response.status === 404) {
        return null;
      }
      const contentType = response.headers.get('content-type');
      if (!contentType || !contentType.includes('application/json')) {
        return null;
      }
      return response.json();
    })
    .then(data => {
      if (data) {
        if (data.success) {
          updateWishlistButtonState(!isCurrentlyInWishlist);
          updateWishlistCounter(data.count);
          
          const action = isCurrentlyInWishlist ? 'đã xóa khỏi' : 'đã thêm vào';
          const message = isCurrentlyInWishlist 
            ? `${productName} ${action} danh sách yêu thích`
            : `${productName} ${action} danh sách yêu thích. <a href="/Account/Profile?tab=my-wishlist" style="color: #007bff; text-decoration: underline;">Xem danh sách yêu thích</a>`;
          
          showNotification(message);
        } else {
          showNotification(data.message || 'Có lỗi xảy ra', 'error');
        }
      }
    })
    .catch(error => {
      // Không hiện toast lỗi khi chưa đăng nhập hoặc lỗi API
    });
  }
  
  function updateWishlistButtonState(isInWishlist) {
    const favoriteBtn = document.querySelector('.favorite-btn');
    if (!favoriteBtn) return;
    
    if (isInWishlist) {
      favoriteBtn.classList.add('in-wishlist');
      favoriteBtn.setAttribute('title', 'Xóa khỏi yêu thích');
    } else {
      favoriteBtn.classList.remove('in-wishlist');
      favoriteBtn.setAttribute('title', 'Yêu thích');
    }
  }
  
  function updateWishlistCounter(count) {
    const counter = document.querySelector('.wishlist-count');
    if (counter) {
      if (count > 0) {
        counter.textContent = count;
        counter.style.display = 'block';
      } else {
        counter.style.display = 'none';
      }
    }
    
    // Also update other wishlist counters if they exist
    const otherCounters = document.querySelectorAll('.header-wishlist-count, .mobile-wishlist-count');
    otherCounters.forEach(counter => {
      if (count > 0) {
        counter.textContent = count;
        counter.style.display = 'block';
      } else {
        counter.style.display = 'none';
      }
    });
  }
  
  // ===========================================
  // 11. NOTIFICATION SYSTEM
  // ===========================================
  function showNotification(message, type = 'success') {
    // Remove existing notifications
    const existingNotifications = document.querySelectorAll('.notification');
    existingNotifications.forEach(notification => notification.remove());
    
    // Create notification
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
      <div class="notification-content">
        <div class="notification-message">${message}</div>
        <button class="notification-close">&times;</button>
      </div>
    `;
    
    // Add to page
    document.body.appendChild(notification);
    
    // Show notification
    setTimeout(() => {
      notification.classList.add('show');
    }, 100);
    
    // Auto hide after 3 seconds
    setTimeout(() => {
      notification.classList.remove('show');
      setTimeout(() => {
        notification.remove();
      }, 300);
    }, 3000);
    
    // Close button
    const closeBtn = notification.querySelector('.notification-close');
    closeBtn.addEventListener('click', () => {
      notification.classList.remove('show');
      setTimeout(() => {
        notification.remove();
      }, 300);
    });
    
    // Handle links in notification
    const links = notification.querySelectorAll('a');
    links.forEach(link => {
      link.addEventListener('click', (e) => {
        e.preventDefault();
        const href = link.getAttribute('href');
        if (href) {
          window.location.href = href;
        }
      });
    });
  }

  // ===========================================
  // 9. ADD TO CART FUNCTIONALITY
  // ===========================================
  function setupAddToCartFunctionality() {
    const addToCartBtn = document.querySelector('.add-to-cart');
    const buyNowBtn = document.querySelector('.buy-now');
    
    if (addToCartBtn) {
      addToCartBtn.addEventListener('click', async function() {
        await addToCart(false);
      });
    }
    
    if (buyNowBtn) {
      buyNowBtn.addEventListener('click', async function() {
        await addToCart(true);
      });
    }
    
    // Handle related products add-to-cart buttons
    const relatedProductBtns = document.querySelectorAll('.add-to-cart-btn');
    relatedProductBtns.forEach(btn => {
      btn.addEventListener('click', async function() {
        const productId = parseInt(this.dataset.productId);
        await addRelatedProductToCart(productId);
      });
    });
  }

  async function addToCart(buyNow = false) {
    try {
      const productId = parseInt(document.querySelector('.add-to-cart').dataset.productId);
      const quantity = parseInt(document.querySelector('.qty-input').value) || 1;
      
      // Get selected variants
      const selectedVariants = {};
      document.querySelectorAll('.variant-btn.selected').forEach(btn => {
        const variantType = btn.dataset.variantType;
        const variantName = btn.textContent.trim();
        selectedVariants[variantType] = variantName;
      });
      
      // Show loading state
      const button = buyNow ? document.querySelector('.buy-now') : document.querySelector('.add-to-cart');
      const originalText = button.textContent;
      button.textContent = 'Đang xử lý...';
      button.disabled = true;
      
      const response = await fetch('/Cart/AddToCart', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'RequestVerificationToken': getAntiForgeryToken()
        },
        body: JSON.stringify({
          productId: productId,
          quantity: quantity,
          selectedVariants: selectedVariants
        })
      });
      
      const data = await response.json();
      
      if (data.success) {
        // Update cart count in header
        updateCartBadge(data.cartCount);
        
        if (buyNow) {
          // Redirect to checkout
          window.location.href = '/Cart/Checkout';
        } else {
          // Show success notification
          showNotification(data.message || 'Đã thêm sản phẩm vào giỏ hàng!', 'success');
        }
      } else {
        showNotification(data.message || 'Không thể thêm sản phẩm vào giỏ hàng!', 'error');
      }
    } catch (error) {
      console.error('Error adding to cart:', error);
      showNotification('Có lỗi xảy ra, vui lòng thử lại!', 'error');
    } finally {
      // Restore button state
      const button = buyNow ? document.querySelector('.buy-now') : document.querySelector('.add-to-cart');
      const originalText = buyNow ? 'Mua ngay' : 'Thêm vào giỏ hàng';
      button.textContent = originalText;
      button.disabled = false;
    }
  }

  async function addRelatedProductToCart(productId) {
    try {
      // Show loading state
      const button = document.querySelector(`.add-to-cart-btn[data-product-id="${productId}"]`);
      const originalText = button.textContent;
      button.textContent = 'Đang xử lý...';
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
        showNotification(data.message || 'Đã thêm sản phẩm vào giỏ hàng!', 'success');
      } else {
        showNotification(data.message || 'Không thể thêm sản phẩm vào giỏ hàng!', 'error');
      }
    } catch (error) {
      console.error('Error adding related product to cart:', error);
      showNotification('Có lỗi xảy ra, vui lòng thử lại!', 'error');
    } finally {
      // Restore button state
      const button = document.querySelector(`.add-to-cart-btn[data-product-id="${productId}"]`);
      button.textContent = 'Thêm vào giỏ hàng';
      button.disabled = false;
    }
  }

  function getAntiForgeryToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]')?.value || '';
  }

  function updateCartBadge(count) {
    // Update all cart badges
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
  }

  // ===========================================
  // 12. REVIEW SYSTEM
  // ===========================================
  function setupReviewSystem() {
    const reviewForm = document.getElementById('review-form');
    const reviewContainer = document.getElementById('reviews-container');
    const loadingSpinner = document.getElementById('loading-spinner');
    const noReviews = document.getElementById('no-reviews');
    let currentPage = 1;
    let selectedRating = 0;
    
    // Auto-fill user info if logged in
    if (window.isUserAuthenticated === true) {
      const nameInput = document.getElementById('review-name');
      const emailInput = document.getElementById('review-email');
      
      if (nameInput && window.currentUserName) {
        nameInput.value = window.currentUserName;
      }
      if (emailInput && window.currentUserEmail) {
        emailInput.value = window.currentUserEmail;
      }
    }
    
    // Handle star rating
    const ratingInput = document.getElementById('selected-rating');
    const stars = document.querySelectorAll('#star-review-rating .star');
    const ratingText = document.getElementById('rating-text');
    
    stars.forEach((star, index) => {
      star.addEventListener('click', function(e) {
        e.preventDefault();
        selectedRating = parseInt(star.dataset.rating);
        if (ratingInput) ratingInput.value = selectedRating;
        updateStarRating(selectedRating);
        updateRatingText(selectedRating);
      });
      
      star.addEventListener('mouseenter', function() {
        const hoverRating = parseInt(star.dataset.rating);
        updateStarRating(hoverRating);
      });
    });
    
    const starContainer = document.getElementById('star-review-rating');
    if (starContainer) {
      starContainer.addEventListener('mouseleave', function() {
        updateStarRating(selectedRating);
      });
    }
    
    function updateStarRating(rating) {
      stars.forEach((star, index) => {
        if (index < rating) {
          star.classList.add('star-full');
          star.classList.remove('star-empty');
        } else {
          star.classList.add('star-empty');
          star.classList.remove('star-full');
        }
      });
    }
    
    function updateRatingText(rating) {
      const texts = ['', 'Rất tệ', 'Tệ', 'Bình thường', 'Tốt', 'Tuyệt vời'];
      if (ratingText) {
        ratingText.textContent = texts[rating] || 'Chọn số sao';
      }
    }
    
    // Handle form submission
    if (reviewForm) {
      reviewForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        if (selectedRating === 0) {
          alert('Vui lòng chọn số sao đánh giá');
          return;
        }
        
        const formData = new FormData(this);
        const submitBtn = document.querySelector('.submit-btn');
        
        try {
          if (submitBtn) {
            submitBtn.disabled = true;
            submitBtn.innerHTML = '<span>Đang gửi...</span>';
          }
          
          const response = await fetch(`/api/product/${window.productId}/reviews`, {
            method: 'POST',
            body: formData,
            headers: {
              'RequestVerificationToken': getAntiForgeryToken()
            }
          });
          
          const result = await response.json();
          
          if (result.success) {
            showNotification('Đánh giá của bạn đã được gửi thành công!', 'success');
            reviewForm.reset();
            selectedRating = 0;
            updateStarRating(0);
            updateRatingText(0);
            if (ratingInput) ratingInput.value = '0';
            
            // Clear image previews
            const imagePreview = document.getElementById('review-images-preview');
            if (imagePreview) imagePreview.innerHTML = '';
            
            // Reload reviews
            loadReviews();
          } else {
            showNotification(result.message || 'Có lỗi xảy ra khi gửi đánh giá', 'error');
          }
        } catch (error) {
          console.error('Error submitting review:', error);
          showNotification('Có lỗi xảy ra khi gửi đánh giá', 'error');
        } finally {
          if (submitBtn) {
            submitBtn.disabled = false;
            submitBtn.innerHTML = '<span>Đăng</span><svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M5 12h14M12 5l7 7-7 7"/></svg>';
          }
        }
      });
    }
    
    // Load reviews with sorting
    function loadReviews(page = 1, sortBy = 'recommended') {
      $.get(`/api/product/${window.productId}/reviews?page=${page}&sortBy=${sortBy}`, function(data) {
        if (data.reviews && data.reviews.length > 0) {
          const reviewsHtml = data.reviews.map(review => {
            const starsHtml = Array(5).fill(0).map((_, i) => {
              return i < review.rating 
                ? `<span class="star star-full"><svg viewBox="0 0 512 512" width="16" height="16"><path d="M512 197.4c0-9.8-7.1-18.1-16.7-19.8l-155.7-25.2-69.6-146.2c-4.1-8.6-12.7-14-22-14s-17.9 5.4-22 14l-69.6 146.2-155.7 25.2c-9.6 1.7-16.7 10-16.7 19.8 0 6.2 2.9 12.1 7.9 16l119.2 104.2-34.7 153.2c-2.1 9.5 1.6 19.2 9.3 24.9 7.7 5.7 18 6.2 26.2 1.2l135.1-81.2 135.1 81.2c3.7 2.2 7.8 3.3 11.9 3.3 4.2 0 8.3-1.3 12-3.8 7.7-5.7 11.4-15.4 9.3-24.9l-34.7-153.2 119.2-104.2c5-4 7.9-9.9 7.9-16z" fill="#EDA100" stroke="#EDA100"/></svg></span>`
                : `<span class="star star-empty"><svg viewBox="0 0 512 512" width="16" height="16"><path d="M512 197.4c0-9.8-7.1-18.1-16.7-19.8l-155.7-25.2-69.6-146.2c-4.1-8.6-12.7-14-22-14s-17.9 5.4-22 14l-69.6 146.2-155.7 25.2c-9.6 1.7-16.7 10-16.7 19.8 0 6.2 2.9 12.1 7.9 16l119.2 104.2-34.7 153.2c-2.1 9.5 1.6 19.2 9.3 24.9 7.7 5.7 18 6.2 26.2 1.2l135.1-81.2 135.1 81.2c3.7 2.2 7.8 3.3 11.9 3.3 4.2 0 8.3-1.3 12-3.8 7.7-5.7 11.4-15.4 9.3-24.9l-34.7-153.2 119.2-104.2c5-4 7.9-9.9 7.9-16z" fill="#dddddd" stroke="#dddddd"/></svg></span>`;
            }).join('');

            const imagesHtml = review.imageUrls && review.imageUrls.length > 0
              ? `<div class="review-images">${review.imageUrls.map(url => `<img src="${url}" alt="Review image" class="review-image">`).join('')}</div>`
              : '';

            return `
              <div class="yotpo-list__item" data-rating="${review.rating}" data-date="${review.createdAt}">
                <div class="yotpo-list__item-row">
                  <div class="yotpo-list__item-col">
                    <div class="yotpo-list__item-rating">
                      ${starsHtml}
                    </div>
                    <p class="yotpo-list__item-name">${review.customerName}</p>
                  </div>
                  <div class="yotpo-list__item-content-row">
                    <div class="yotpo-list__item-content">
                      <p>${review.comment}</p>
                      ${imagesHtml}
                    </div>
                    <div class="yotpo-list__item-createdat">${new Date(review.createdAt).toLocaleDateString('vi-VN')}</div>
                  </div>
                </div>
              </div>
            `;
          }).join('');

          $('#reviews-container').html(reviewsHtml);

          // Update rating summary
          const stats = data.statistics;
          if (stats) {
            const avgRating = stats.averageRating;
            const totalReviews = stats.totalReviews;
            
            // Update rating stars
            const ratingStarsHtml = Array(5).fill(0).map((_, i) => {
              if (avgRating >= i + 1) {
                return `<span class="star star-full"><svg viewBox="0 0 512 512" width="16" height="16"><path d="M512 197.4c0-9.8-7.1-18.1-16.7-19.8l-155.7-25.2-69.6-146.2c-4.1-8.6-12.7-14-22-14s-17.9 5.4-22 14l-69.6 146.2-155.7 25.2c-9.6 1.7-16.7 10-16.7 19.8 0 6.2 2.9 12.1 7.9 16l119.2 104.2-34.7 153.2c-2.1 9.5 1.6 19.2 9.3 24.9 7.7 5.7 18 6.2 26.2 1.2l135.1-81.2 135.1 81.2c3.7 2.2 7.8 3.3 11.9 3.3 4.2 0 8.3-1.3 12-3.8 7.7-5.7 11.4-15.4 9.3-24.9l-34.7-153.2 119.2-104.2c5-4 7.9-9.9 7.9-16z" fill="#EDA100" stroke="#EDA100"/></svg></span>`;
              } else if (avgRating > i) {
                return `<span class="star star-half"><svg viewBox="0 0 512 512" width="16" height="16"><path d="M512 197.4c0-9.8-7.1-18.1-16.7-19.8l-155.7-25.2-69.6-146.2c-4.1-8.6-12.7-14-22-14s-17.9 5.4-22 14l-69.6 146.2-155.7 25.2c-9.6 1.7-16.7 10-16.7 19.8 0 6.2 2.9 12.1 7.9 16l119.2 104.2-34.7 153.2c-2.1 9.5 1.6 19.2 9.3 24.9 7.7 5.7 18 6.2 26.2 1.2l135.1-81.2 135.1 81.2c3.7 2.2 7.8 3.3 11.9 3.3 4.2 0 8.3-1.3 12-3.8 7.7-5.7 11.4-15.4 9.3-24.9l-34.7-153.2 119.2-104.2c5-4 7.9-9.9 7.9-16z" fill="#EDA100" stroke="#EDA100"/><defs><linearGradient id="half"><stop offset="50%" stop-color="#EDA100"/><stop offset="50%" stop-color="#E0E0E0"/></linearGradient></defs></svg></span>`;
              } else {
                return `<span class="star star-empty"><svg viewBox="0 0 512 512" width="16" height="16"><path d="M512 197.4c0-9.8-7.1-18.1-16.7-19.8l-155.7-25.2-69.6-146.2c-4.1-8.6-12.7-14-22-14s-17.9 5.4-22 14l-69.6 146.2-155.7 25.2c-9.6 1.7-16.7 10-16.7 19.8 0 6.2 2.9 12.1 7.9 16l119.2 104.2-34.7 153.2c-2.1 9.5 1.6 19.2 9.3 24.9 7.7 5.7 18 6.2 26.2 1.2l135.1-81.2 135.1 81.2c3.7 2.2 7.8 3.3 11.9 3.3 4.2 0 8.3-1.3 12-3.8 7.7-5.7 11.4-15.4 9.3-24.9l-34.7-153.2 119.2-104.2c5-4 7.9-9.9 7.9-16z" fill="#dddddd" stroke="#dddddd"/></svg></span>`;
              }
            }).join('');

            $('#rating-stars-display').html(ratingStarsHtml);
            $('#section-count').text(`(${totalReviews} đánh giá)`);
          }

          // Update pagination if needed
          if (data.totalPages > 1) {
            const paginationHtml = generatePagination(data.currentPage, data.totalPages);
            $('#pagination-container').html(paginationHtml).show();
          } else {
            $('#pagination-container').hide();
          }
        } else {
          $('#reviews-container').html('<div class="no-reviews"><p>Chưa có đánh giá nào cho sản phẩm này.</p><p>Hãy là người đầu tiên đánh giá!</p></div>');
          $('#pagination-container').hide();
        }
      });
    }
    
    // Review filter handling
    $('.review-filter-select').on('change', function() {
      const sortBy = $(this).val();
      loadReviews(1, sortBy);
    });
  }
}); 