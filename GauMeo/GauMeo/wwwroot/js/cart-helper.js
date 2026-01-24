/**
 * Cart Helper - Shared cart functionality for all pages
 * Include this file in _Layout.cshtml for global access
 */

// ============================================
// ANTI-FORGERY TOKEN
// ============================================
function getAntiForgeryToken() {
    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
    return tokenInput ? tokenInput.value : '';
}

// ============================================
// ADD TO CART WITH VARIANT CHECK
// Use this for listing pages (Home, Product/Index, Brand/Detail, Promotion)
// ============================================
function addToCartWithVariantCheck(productId, hasVariants, button = null) {
    if (hasVariants) {
        // Nếu sản phẩm có biến thể, chuyển đến trang chi tiết để chọn
        window.location.href = `/Product/Detail/${productId}`;
        return;
    }
    
    // Nếu không có biến thể, thêm trực tiếp vào giỏ hàng
    addToCart(productId, 1, {}, button);
}

// ============================================
// ADD TO CART - Main Function
// ============================================
async function addToCart(productId, quantity = 1, selectedVariants = {}, button = null) {
    // Find button if not provided
    if (!button) {
        button = document.querySelector(`[data-product-id="${productId}"].add-to-cart-btn`) ||
                 document.querySelector(`.add-to-cart-btn[onclick*="${productId}"]`);
    }
    
    let originalContent = '';
    
    try {
        // Show loading state
        if (button) {
            originalContent = button.innerHTML;
            button.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Đang thêm...';
            button.disabled = true;
        }

        const response = await fetch('/Cart/AddToCart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: JSON.stringify({
                productId: parseInt(productId),
                quantity: quantity,
                selectedVariants: selectedVariants
            })
        });

        const data = await response.json();

        if (data.success) {
            // Update cart badge
            updateCartBadge(data.cartCount);
            
            // Show success state on button
            if (button) {
                button.innerHTML = '<i class="fas fa-check"></i> Đã thêm';
                button.style.background = '#28a745';
                button.style.borderColor = '#28a745';
                
                // Reset button after 2 seconds
                setTimeout(() => {
                    button.innerHTML = originalContent;
                    button.style.background = '';
                    button.style.borderColor = '';
                    button.disabled = false;
                }, 2000);
            }
            
            // Show notification
            showCartNotification(data.message || 'Đã thêm sản phẩm vào giỏ hàng!', 'success');
            
            return { success: true, cartCount: data.cartCount };
        } else {
            throw new Error(data.message || 'Không thể thêm sản phẩm vào giỏ hàng!');
        }
    } catch (error) {
        console.error('Error adding to cart:', error);
        
        // Show error state on button
        if (button) {
            button.innerHTML = '<i class="fas fa-times"></i> Lỗi';
            button.style.background = '#dc3545';
            button.style.borderColor = '#dc3545';
            
            // Reset button after 2 seconds
            setTimeout(() => {
                button.innerHTML = originalContent;
                button.style.background = '';
                button.style.borderColor = '';
                button.disabled = false;
            }, 2000);
        }
        
        showCartNotification(error.message || 'Có lỗi xảy ra, vui lòng thử lại!', 'error');
        
        return { success: false, error: error.message };
    }
}

// ============================================
// UPDATE CART BADGE
// ============================================
function updateCartBadge(count) {
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
    
    // Add bounce animation
    const headerBadge = document.querySelector('.header-cart-badge');
    if (headerBadge && count > 0) {
        headerBadge.style.transform = 'scale(1.3)';
        setTimeout(() => {
            headerBadge.style.transform = 'scale(1)';
        }, 200);
    }
}

// Fetch and update cart count from server
async function refreshCartBadge() {
    try {
        const response = await fetch('/Cart/GetCartCount');
        const data = await response.json();
        updateCartBadge(data.count);
    } catch (error) {
        console.error('Error fetching cart count:', error);
    }
}

// ============================================
// NOTIFICATION SYSTEM
// ============================================
function showCartNotification(message, type = 'success') {
    // Remove existing notifications
    const existingNotifications = document.querySelectorAll('.cart-notification');
    existingNotifications.forEach(n => n.remove());

    const notification = document.createElement('div');
    notification.className = `cart-notification cart-notification--${type}`;
    
    const iconSvg = type === 'success' 
        ? '<svg width="20" height="20" fill="currentColor" viewBox="0 0 16 16"><path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425a.247.247 0 0 1 .02-.022Z"/></svg>'
        : '<svg width="20" height="20" fill="currentColor" viewBox="0 0 16 16"><path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/><path d="M7.002 11a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 4.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 4.995z"/></svg>';

    notification.innerHTML = `
        <div class="cart-notification__content">
            <span class="cart-notification__icon">${iconSvg}</span>
            <span class="cart-notification__message">${message}</span>
            <button class="cart-notification__close" onclick="this.parentElement.parentElement.remove()">
                <svg width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                    <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z"/>
                </svg>
            </button>
        </div>
    `;

    document.body.appendChild(notification);

    // Animate in
    requestAnimationFrame(() => {
        notification.classList.add('cart-notification--show');
    });

    // Auto remove after 4 seconds
    setTimeout(() => {
        if (notification.parentElement) {
            notification.classList.remove('cart-notification--show');
            setTimeout(() => notification.remove(), 300);
        }
    }, 4000);
}

// ============================================
// INJECT STYLES
// ============================================
(function injectCartHelperStyles() {
    if (document.getElementById('cart-helper-styles')) return;
    
    const styles = document.createElement('style');
    styles.id = 'cart-helper-styles';
    styles.textContent = `
        .cart-notification {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 10000;
            opacity: 0;
            transform: translateX(100%);
            transition: all 0.3s ease;
        }
        
        .cart-notification--show {
            opacity: 1;
            transform: translateX(0);
        }
        
        .cart-notification__content {
            display: flex;
            align-items: center;
            gap: 12px;
            padding: 14px 18px;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
            border-left: 4px solid #28a745;
        }
        
        .cart-notification--error .cart-notification__content {
            border-left-color: #dc3545;
        }
        
        .cart-notification__icon {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 32px;
            height: 32px;
            border-radius: 50%;
            background: #d4edda;
            color: #28a745;
        }
        
        .cart-notification--error .cart-notification__icon {
            background: #f8d7da;
            color: #dc3545;
        }
        
        .cart-notification__message {
            font-size: 14px;
            font-weight: 500;
            color: #333;
            max-width: 250px;
        }
        
        .cart-notification__close {
            background: none;
            border: none;
            cursor: pointer;
            padding: 4px;
            border-radius: 4px;
            color: #999;
            transition: all 0.2s ease;
        }
        
        .cart-notification__close:hover {
            background: #f5f5f5;
            color: #333;
        }
        
        /* Button loading animation */
        .add-to-cart-btn .fa-spinner {
            animation: fa-spin 1s infinite linear;
        }
        
        @keyframes fa-spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        
        @media (max-width: 768px) {
            .cart-notification {
                right: 10px;
                left: 10px;
            }
            
            .cart-notification__content {
                width: 100%;
            }
        }
    `;
    
    document.head.appendChild(styles);
})();

// ============================================
// INITIALIZE ON DOM READY
// ============================================
document.addEventListener('DOMContentLoaded', function() {
    // Refresh cart badge on page load
    refreshCartBadge();
});

// Export for modules (optional)
if (typeof module !== 'undefined' && module.exports) {
    module.exports = { addToCart, updateCartBadge, refreshCartBadge, showCartNotification };
}
