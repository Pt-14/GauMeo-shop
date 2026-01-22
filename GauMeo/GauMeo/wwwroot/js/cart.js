// Cart Management JavaScript
document.addEventListener('DOMContentLoaded', function() {
  setupCartFunctionality();
  
  function setupCartFunctionality() {
    setupQuantityControls();
    setupRemoveButtons();
    setupClearAllButton();
    setupContinueShoppingButton();
  }
  
  function setupQuantityControls() {
    // Quantity plus buttons
    document.querySelectorAll('.qty-btn.plus').forEach(btn => {
      btn.addEventListener('click', async function() {
        const cartItemId = parseInt(this.dataset.cartItemId);
        const input = document.querySelector(`.qty-input[data-cart-item-id="${cartItemId}"]`);
        const newQuantity = parseInt(input.value) + 1;
        
        await updateCartItem(cartItemId, newQuantity);
      });
    });
    
    // Quantity minus buttons
    document.querySelectorAll('.qty-btn.minus').forEach(btn => {
      btn.addEventListener('click', async function() {
        const cartItemId = parseInt(this.dataset.cartItemId);
        const input = document.querySelector(`.qty-input[data-cart-item-id="${cartItemId}"]`);
        const currentQuantity = parseInt(input.value);
        
        if (currentQuantity > 1) {
          const newQuantity = currentQuantity - 1;
          await updateCartItem(cartItemId, newQuantity);
        }
      });
    });
    
    // Quantity input change
    document.querySelectorAll('.qty-input').forEach(input => {
      input.addEventListener('change', async function() {
        const cartItemId = parseInt(this.dataset.cartItemId);
        const newQuantity = parseInt(this.value);
        
        if (newQuantity >= 1) {
          await updateCartItem(cartItemId, newQuantity);
        } else {
          this.value = 1;
        }
      });
    });
  }
  
  function setupRemoveButtons() {
    document.querySelectorAll('.cart-remove').forEach(btn => {
      btn.addEventListener('click', async function() {
        const cartItemId = parseInt(this.dataset.cartItemId);
        
        if (confirm('Bạn có chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng?')) {
          await removeCartItem(cartItemId);
        }
      });
    });
  }
  
  function setupClearAllButton() {
    const clearAllBtn = document.querySelector('.clear-all-btn');
    if (clearAllBtn) {
      clearAllBtn.addEventListener('click', async function() {
        if (confirm('Bạn có chắc chắn muốn xóa tất cả sản phẩm khỏi giỏ hàng?')) {
          await clearAllCart();
        }
      });
    }
  }

  function setupContinueShoppingButton() {
    const continueShoppingBtn = document.querySelector('#continue-shopping');
    if (continueShoppingBtn) {
      continueShoppingBtn.addEventListener('click', function() {
        window.location.href = '/Product';
      });
    }
  }
  
  async function updateCartItem(cartItemId, quantity) {
    try {
      showLoading(true);
      
      const response = await fetch('/Cart/UpdateCartItem', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'RequestVerificationToken': getAntiForgeryToken()
        },
        body: JSON.stringify({
          cartItemId: cartItemId,
          quantity: quantity
        })
      });
      
      const data = await response.json();
      
      if (data.success) {
        // Update UI
        updateCartItemUI(cartItemId, quantity);
        updateCartSummary(data.cartTotal, data.cartCount);
        showNotification('Đã cập nhật giỏ hàng!', 'success');
      } else {
        showNotification(data.message || 'Không thể cập nhật sản phẩm!', 'error');
      }
    } catch (error) {
      console.error('Error updating cart:', error);
      showNotification('Có lỗi xảy ra, vui lòng thử lại!', 'error');
    } finally {
      showLoading(false);
    }
  }
  
  async function removeCartItem(cartItemId) {
    try {
      showLoading(true);
      
      const response = await fetch('/Cart/RemoveCartItem', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'RequestVerificationToken': getAntiForgeryToken()
        },
        body: JSON.stringify({
          cartItemId: cartItemId
        })
      });
      
      const data = await response.json();
      
      if (data.success) {
        // Remove item from UI
        removeCartItemFromUI(cartItemId);
        updateCartSummary(data.cartTotal, data.cartCount);
        showNotification('Đã xóa sản phẩm khỏi giỏ hàng!', 'success');
        
        // Check if cart is empty
        if (data.cartCount === 0) {
          showEmptyCart();
        }
      } else {
        showNotification(data.message || 'Không thể xóa sản phẩm!', 'error');
      }
    } catch (error) {
      console.error('Error removing cart item:', error);
      showNotification('Có lỗi xảy ra, vui lòng thử lại!', 'error');
    } finally {
      showLoading(false);
    }
  }

  async function clearAllCart() {
    try {
      showLoading(true);
      
      const response = await fetch('/Cart/ClearCart', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'RequestVerificationToken': getAntiForgeryToken()
        }
      });
      
      const data = await response.json();
      
      if (data.success) {
        // Redirect to refresh the page and show empty cart
        showNotification(data.message || 'Đã xóa hết tất cả sản phẩm!', 'success');
        setTimeout(() => {
          window.location.reload();
        }, 1000);
      } else {
        showNotification(data.message || 'Không thể xóa giỏ hàng!', 'error');
      }
    } catch (error) {
      console.error('Error clearing cart:', error);
      showNotification('Có lỗi xảy ra, vui lòng thử lại!', 'error');
    } finally {
      showLoading(false);
    }
  }
  
  function updateCartItemUI(cartItemId, quantity) {
    const cartItem = document.querySelector(`[data-cart-item-id="${cartItemId}"]`);
    if (!cartItem) return;
    
    const qtyInput = cartItem.querySelector('.qty-input');
    const subtotalEl = cartItem.querySelector('.subtotal');
    
    if (qtyInput) {
      qtyInput.value = quantity;
    }
    
    if (subtotalEl) {
      const unitPriceText = cartItem.querySelector('.price').textContent;
      const unitPrice = parseFloat(unitPriceText.replace(/[^\d]/g, ''));
      const subtotal = unitPrice * quantity;
      subtotalEl.textContent = formatCurrency(subtotal);
    }
  }
  
  function removeCartItemFromUI(cartItemId) {
    const cartItem = document.querySelector(`[data-cart-item-id="${cartItemId}"]`);
    if (cartItem) {
      cartItem.remove();
    }
  }
  
  function updateCartSummary(total, count) {
    const subtotalEl = document.querySelector('.cart-subtotal');
    const totalEl = document.querySelector('.cart-total');
    
    if (subtotalEl) {
      subtotalEl.textContent = formatCurrency(total);
    }
    
    if (totalEl) {
      totalEl.textContent = formatCurrency(total);
    }
    
    // Update cart badge
    updateCartBadge(count);
    
    // Update product count text
    const productCountText = document.querySelector('.cart-total-row span');
    if (productCountText && productCountText.textContent.includes('sản phẩm')) {
      productCountText.textContent = `Tổng sản phẩm (${count} sản phẩm)`;
    }
  }
  
  function updateCartBadge(count) {
    const cartBadges = document.querySelectorAll('.header-cart-badge, .cart-count, .cart-badge');
    cartBadges.forEach(badge => {
      badge.textContent = count || 0;
    });
  }
  
  function showEmptyCart() {
    const cartItems = document.querySelector('.cart-items');
    const cartSummary = document.querySelector('.cart-summary-block');
    const cartNote = document.querySelector('.cart-note-block');
    
    if (cartItems) {
      cartItems.innerHTML = `
        <div class="cart-empty">
          <div class="empty-cart-icon">
            <svg width="64" height="64" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1">
              <circle cx="9" cy="21" r="1"></circle>
              <circle cx="20" cy="21" r="1"></circle>
              <path d="m1 1 4 4 10 2 2 8-12 1M7 13l-2-2m0 0L3 9m2 2 2 2"></path>
            </svg>
          </div>
          <h3>Giỏ hàng trống</h3>
          <p>Hãy thêm sản phẩm vào giỏ hàng để bắt đầu mua sắm!</p>
          <a href="/Product" class="btn">Xem sản phẩm</a>
        </div>
      `;
    }
    
    if (cartSummary) {
      cartSummary.style.display = 'none';
    }
    
    if (cartNote) {
      cartNote.style.display = 'none';
    }
  }
  
  function formatCurrency(amount) {
    return Math.round(amount).toLocaleString('vi-VN') + ' đ';
  }
  
  function getAntiForgeryToken() {
    const token = document.querySelector('input[name="__RequestVerificationToken"]');
    return token ? token.value : '';
  }
  
  function showLoading(show) {
    // You can implement a loading overlay here if needed
    if (show) {
      document.body.style.cursor = 'wait';
    } else {
      document.body.style.cursor = 'default';
    }
  }
  
  function showNotification(message, type = 'success') {
    // Remove existing notifications
    const existingNotifications = document.querySelectorAll('.notification');
    existingNotifications.forEach(notification => notification.remove());
    
    // Create notification
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
      <div class="notification-content">
        <div class="notification-icon">
          ${type === 'success' ? 
            '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="20 6 9 17 4 12"></polyline></svg>' : 
            '<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="10"></circle><line x1="15" y1="9" x2="9" y2="15"></line><line x1="9" y1="9" x2="15" y2="15"></line></svg>'
          }
        </div>
        <span class="notification-message">${message}</span>
      </div>
    `;
    
    // Add to document
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
  }
}); 