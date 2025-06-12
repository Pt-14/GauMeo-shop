// Product Index Page JavaScript

document.addEventListener('DOMContentLoaded', function() {
    initializeProductPage();
});

function initializeProductPage() {
    initializeSorting();
    initializeProductCards();
    initializeCartButtons();
    initializeWishlistButtons();
    initializeQuickViewButtons();
}

// Sort functionality
function initializeSorting() {
    const sortSelect = document.querySelector('.sort-select');
    if (!sortSelect) return;

    sortSelect.addEventListener('change', function() {
        const sortValue = this.value;
        const productsGrid = document.getElementById('productsGrid');
        const productCards = Array.from(productsGrid.querySelectorAll('.product-card'));

        // Sort product cards based on selected option
        productCards.sort((a, b) => {
            switch (sortValue) {
                case 'price-asc':
                    return getProductPrice(a) - getProductPrice(b);
                case 'price-desc':
                    return getProductPrice(b) - getProductPrice(a);
                case 'name-asc':
                    return getProductName(a).localeCompare(getProductName(b));
                case 'name-desc':
                    return getProductName(b).localeCompare(getProductName(a));
                case 'rating':
                    return getProductRating(b) - getProductRating(a);
                case 'popular':
                default:
                    return 0; // Keep original order for popular
            }
        });

        // Re-append sorted cards
        productCards.forEach(card => {
            productsGrid.appendChild(card);
        });

        // Add animation
        productCards.forEach((card, index) => {
            card.style.opacity = '0';
            card.style.transform = 'translateY(20px)';
            setTimeout(() => {
                card.style.transition = 'all 0.3s ease';
                card.style.opacity = '1';
                card.style.transform = 'translateY(0)';
            }, index * 50);
        });
    });
}

// Helper functions for sorting
function getProductPrice(card) {
    const priceElement = card.querySelector('.current-price');
    if (!priceElement) return 0;
    
    const priceText = priceElement.textContent.replace(/[^\d]/g, '');
    return parseInt(priceText) || 0;
}

function getProductName(card) {
    const nameElement = card.querySelector('.product-name');
    return nameElement ? nameElement.textContent.trim() : '';
}

function getProductRating(card) {
    const ratingElement = card.querySelector('.rating-text');
    if (!ratingElement) return 0;
    
    const ratingText = ratingElement.textContent.replace(/[^\d.]/g, '');
    return parseFloat(ratingText) || 0;
}

// Product card interactions
function initializeProductCards() {
    const productCards = document.querySelectorAll('.product-card');
    
    productCards.forEach(card => {
        // Add click event for navigation to detail page
        card.addEventListener('click', function(e) {
            // Don't navigate if clicking on buttons
            if (e.target.closest('button')) return;
            
            const productId = this.getAttribute('data-product-id');
            if (productId) {
                window.location.href = `/Product/Detail?id=${productId}`;
            }
        });

        // Add keyboard navigation
        card.addEventListener('keydown', function(e) {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                this.click();
            }
        });

        // Make cards focusable
        card.setAttribute('tabindex', '0');
    });
}

// Add to cart functionality
function initializeCartButtons() {
    const cartButtons = document.querySelectorAll('.add-to-cart-btn');
    
    cartButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.stopPropagation(); // Prevent card click event
            
            const productId = this.getAttribute('data-product-id');
            addToCart(productId);
        });
    });
}

function addToCart(productId) {
    // Show loading state
    const button = document.querySelector(`[data-product-id="${productId}"].add-to-cart-btn`);
    if (button) {
        const originalText = button.innerHTML;
        button.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Đang thêm...';
        button.disabled = true;

        // Simulate API call
        setTimeout(() => {
            button.innerHTML = '<i class="fas fa-check"></i> Đã thêm';
            button.style.background = '#28a745';
            
            // Update cart badge
            updateCartBadge();
            
            // Show notification
            showNotification('Sản phẩm đã được thêm vào giỏ hàng!', 'success');
            
            // Reset button after 2 seconds
            setTimeout(() => {
                button.innerHTML = originalText;
                button.style.background = '';
                button.disabled = false;
            }, 2000);
        }, 800);
    }
}

// Wishlist functionality
function initializeWishlistButtons() {
    const wishlistButtons = document.querySelectorAll('.add-to-wishlist-btn');
    
    wishlistButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.stopPropagation(); // Prevent card click event
            
            const productId = this.getAttribute('data-product-id');
            toggleWishlist(productId, this);
        });
    });
}

function toggleWishlist(productId, button) {
    const isInWishlist = button.classList.contains('in-wishlist');
    
    if (isInWishlist) {
        button.classList.remove('in-wishlist');
        button.innerHTML = '<i class="far fa-heart"></i>';
        button.style.color = '';
        showNotification('Đã xóa khỏi danh sách yêu thích', 'info');
    } else {
        button.classList.add('in-wishlist');
        button.innerHTML = '<i class="fas fa-heart"></i>';
        button.style.color = '#e74c3c';
        showNotification('Đã thêm vào danh sách yêu thích!', 'success');
    }
}

// Quick view functionality
function initializeQuickViewButtons() {
    const quickViewButtons = document.querySelectorAll('.quick-view-btn');
    
    quickViewButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.stopPropagation(); // Prevent card click event
            
            const productId = this.getAttribute('data-product-id');
            showQuickView(productId);
        });
    });
}

function showQuickView(productId) {
    // Create modal overlay
    const modal = document.createElement('div');
    modal.className = 'quick-view-modal';
    modal.innerHTML = `
        <div class="quick-view-content">
            <div class="quick-view-header">
                <h3>Xem nhanh sản phẩm</h3>
                <button class="close-btn" onclick="this.closest('.quick-view-modal').remove()">
                    <i class="fas fa-times"></i>
                </button>
            </div>
            <div class="quick-view-body">
                <div class="loading">
                    <i class="fas fa-spinner fa-spin"></i>
                    <p>Đang tải thông tin sản phẩm...</p>
                </div>
            </div>
        </div>
    `;

    // Add to DOM
    document.body.appendChild(modal);

    // Add click outside to close
    modal.addEventListener('click', function(e) {
        if (e.target === modal) {
            modal.remove();
        }
    });

    // Simulate loading product data
    setTimeout(() => {
        const mockProductData = generateMockProductData(productId);
        modal.querySelector('.quick-view-body').innerHTML = mockProductData;
        
        // Initialize cart button in modal
        const modalCartBtn = modal.querySelector('.add-to-cart-btn');
        if (modalCartBtn) {
            modalCartBtn.addEventListener('click', function() {
                addToCart(productId);
                modal.remove();
            });
        }
    }, 1000);
}

function generateMockProductData(productId) {
    return `
        <div class="quick-view-product">
            <div class="quick-view-image">
                <img src="/images/products/placeholder.jpg" alt="Product Image">
            </div>
            <div class="quick-view-info">
                <h4>Sản phẩm #${productId}</h4>
                <div class="quick-view-price">
                    <span class="price">450.000 đ</span>
                </div>
                <div class="quick-view-description">
                    <p>Đây là mô tả ngắn gọn về sản phẩm. Sản phẩm chất lượng cao, phù hợp cho thú cưng của bạn.</p>
                </div>
                <div class="quick-view-actions">
                    <button class="add-to-cart-btn" data-product-id="${productId}">
                        <i class="fas fa-shopping-cart"></i>
                        Thêm vào giỏ
                    </button>
                    <button class="view-detail-btn" onclick="window.location.href='/Product/Detail?id=${productId}'">
                        Xem chi tiết
                    </button>
                </div>
            </div>
        </div>
    `;
}

// Helper functions
function updateCartBadge() {
    const cartBadge = document.querySelector('.header-cart-badge');
    if (cartBadge) {
        const currentCount = parseInt(cartBadge.textContent) || 0;
        cartBadge.textContent = currentCount + 1;
        
        // Add animation
        cartBadge.style.transform = 'scale(1.3)';
        setTimeout(() => {
            cartBadge.style.transform = 'scale(1)';
        }, 200);
    }
}

function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <div class="notification-content">
            <i class="fas fa-${type === 'success' ? 'check-circle' : type === 'error' ? 'exclamation-circle' : 'info-circle'}"></i>
            <span>${message}</span>
        </div>
    `;

    // Add to DOM
    document.body.appendChild(notification);

    // Show animation
    setTimeout(() => {
        notification.classList.add('show');
    }, 100);

    // Auto remove after 3 seconds
    setTimeout(() => {
        notification.classList.remove('show');
        setTimeout(() => {
            notification.remove();
        }, 300);
    }, 3000);
}

// Add notification and modal styles
const styles = `
    .quick-view-modal {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(0, 0, 0, 0.7);
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 9999;
        opacity: 0;
        animation: fadeIn 0.3s ease forwards;
    }

    .quick-view-content {
        background: white;
        border-radius: 12px;
        max-width: 600px;
        width: 90%;
        max-height: 80vh;
        overflow-y: auto;
        transform: scale(0.8);
        animation: scaleIn 0.3s ease forwards;
    }

    .quick-view-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 20px;
        border-bottom: 1px solid #eee;
    }

    .close-btn {
        background: none;
        border: none;
        font-size: 1.2rem;
        cursor: pointer;
        color: #666;
        padding: 8px;
        border-radius: 50%;
        transition: all 0.2s ease;
    }

    .close-btn:hover {
        background: #f5f5f5;
        color: #333;
    }

    .quick-view-body {
        padding: 20px;
    }

    .loading {
        text-align: center;
        padding: 40px 20px;
        color: #666;
    }

    .loading i {
        font-size: 2rem;
        margin-bottom: 15px;
        color: #007bff;
    }

    .quick-view-product {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 20px;
    }

    .quick-view-image img {
        width: 100%;
        height: 250px;
        object-fit: cover;
        border-radius: 8px;
    }

    .quick-view-price {
        font-size: 1.5rem;
        font-weight: 700;
        color: #e74c3c;
        margin: 15px 0;
    }

    .quick-view-actions {
        display: flex;
        gap: 10px;
        margin-top: 20px;
    }

    .view-detail-btn {
        flex: 1;
        padding: 10px;
        background: #6c757d;
        color: white;
        border: none;
        border-radius: 8px;
        cursor: pointer;
        transition: background 0.2s ease;
    }

    .view-detail-btn:hover {
        background: #5a6268;
    }

    .notification {
        position: fixed;
        top: 20px;
        right: 20px;
        background: white;
        padding: 15px 20px;
        border-radius: 8px;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
        z-index: 10000;
        transform: translateX(100%);
        transition: transform 0.3s ease;
        border-left: 4px solid #007bff;
    }

    .notification.notification-success {
        border-left-color: #28a745;
    }

    .notification.notification-error {
        border-left-color: #dc3545;
    }

    .notification.show {
        transform: translateX(0);
    }

    .notification-content {
        display: flex;
        align-items: center;
        gap: 10px;
    }

    .notification-content i {
        color: #007bff;
    }

    .notification-success .notification-content i {
        color: #28a745;
    }

    .notification-error .notification-content i {
        color: #dc3545;
    }

    @keyframes fadeIn {
        to { opacity: 1; }
    }

    @keyframes scaleIn {
        to { transform: scale(1); }
    }

    @media (max-width: 768px) {
        .quick-view-product {
            grid-template-columns: 1fr;
        }
        
        .quick-view-actions {
            flex-direction: column;
        }
    }

    /* Dark mode styles */
    body.dark .quick-view-content {
        background: #2d3748;
        color: #e2e8f0;
    }

    body.dark .quick-view-header {
        border-bottom-color: #4a5568;
    }

    body.dark .close-btn {
        color: #a0aec0;
    }

    body.dark .close-btn:hover {
        background: #4a5568;
        color: #e2e8f0;
    }

    body.dark .notification {
        background: #2d3748;
        color: #e2e8f0;
    }
`;

// Add styles to head
const styleSheet = document.createElement('style');
styleSheet.textContent = styles;
document.head.appendChild(styleSheet); 