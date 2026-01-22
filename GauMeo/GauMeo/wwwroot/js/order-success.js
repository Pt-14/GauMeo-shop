// Order Success Page JavaScript

class OrderSuccessManager {
    constructor() {
        // Get order data from window object instead of localStorage
        this.orderData = window.orderData || null;
        
        this.init();
    }

    init() {
        if (this.orderData) {
            this.displayOrderInformation();
            this.bindEvents();
        } else {
            console.warn('No order data available');
        }
    }

    // Display all order information
    displayOrderInformation() {
        this.updateOrderDetails();
        this.updateCustomerInfo();
        this.displayOrderItems();
    }

    // Update order details section
    updateOrderDetails() {
        // Order number
        const orderNumberElements = document.querySelectorAll('.order-number .value, .order-number');
        orderNumberElements.forEach(element => {
            if (element.classList.contains('value') || !element.querySelector('.value')) {
                element.textContent = this.orderData.orderNumber;
            }
        });

        // Order date
        const orderDateElements = document.querySelectorAll('.order-date, [data-order-date]');
        orderDateElements.forEach(element => {
            const date = new Date(this.orderData.createdAt);
            element.textContent = date.toLocaleDateString('vi-VN', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit'
            });
        });

        // Payment method
        const paymentMethodElements = document.querySelectorAll('.payment-method, [data-payment-method]');
        paymentMethodElements.forEach(element => {
            const methodText = this.getPaymentMethodText(this.orderData.paymentMethod);
            element.textContent = methodText;
        });

        // Total amount
        const totalAmountElements = document.querySelectorAll('.total-amount, [data-total-amount]');
        totalAmountElements.forEach(element => {
            element.textContent = this.formatPrice(this.orderData.totalAmount);
        });

        // Status
        const statusElements = document.querySelectorAll('.status, [data-status]');
        statusElements.forEach(element => {
            element.textContent = this.getStatusText(this.orderData.status);
        });
    }

    // Update customer information
    updateCustomerInfo() {
        // Customer name
        const customerNameElements = document.querySelectorAll('.customer-name, [data-customer-name]');
        customerNameElements.forEach(element => {
            element.textContent = this.orderData.customerName;
        });

        // Customer phone
        const customerPhoneElements = document.querySelectorAll('.customer-phone, [data-customer-phone]');
        customerPhoneElements.forEach(element => {
            element.textContent = this.orderData.customerPhone;
        });

        // Customer email
        if (this.orderData.customerEmail) {
            const customerEmailElements = document.querySelectorAll('.customer-email, [data-customer-email]');
            customerEmailElements.forEach(element => {
                element.textContent = this.orderData.customerEmail;
            });
        }

        // Customer address
        const customerAddressElements = document.querySelectorAll('.customer-address, [data-customer-address]');
        customerAddressElements.forEach(element => {
            element.textContent = this.orderData.shippingAddress;
        });

        // Customer notes
        if (this.orderData.notes && this.orderData.notes.trim()) {
            const customerNoteElements = document.querySelectorAll('.customer-note, [data-customer-note]');
            customerNoteElements.forEach(element => {
                element.textContent = this.orderData.notes;
                // Show parent container if it was hidden
                const container = element.closest('.info-row, .customer-note-container');
                if (container) {
                    container.style.display = 'flex';
                }
            });
        }
    }

    // Display order items (if needed for custom display)
    displayOrderItems() {
        const orderItemsContainer = document.querySelector('.custom-order-items, .success-order-items');
        
        if (!orderItemsContainer || !this.orderData.orderItems || this.orderData.orderItems.length === 0) {
            return;
        }

        const itemsHTML = this.orderData.orderItems.map(item => {
            let variantsHTML = '';
            if (item.selectedVariants) {
                try {
                    const variants = JSON.parse(item.selectedVariants);
                    if (variants && Object.keys(variants).length > 0) {
                        variantsHTML = Object.entries(variants).map(([key, value]) => 
                            `<span class="variant-tag">${key}: ${value}</span>`
                        ).join(' ');
                    }
                } catch (e) {
                    console.warn('Error parsing variants:', e);
                }
            }

            return `
                <div class="success-order-item">
                    <div class="success-item-image">
                        <img src="${item.productImageUrl}" alt="${item.productName}" style="width:100%;height:100%;object-fit:cover;">
                    </div>
                    <div class="success-item-info">
                        <div class="success-item-name">${item.productName}</div>
                        ${variantsHTML ? `<div class="success-item-variants">${variantsHTML}</div>` : ''}
                        <div class="success-item-price">
                            <span class="success-item-quantity">Số lượng: ${item.quantity}</span>
                            <span class="success-item-total">${this.formatPrice(item.subTotal)}</span>
                        </div>
                    </div>
                </div>
            `;
        }).join('');

        orderItemsContainer.innerHTML = itemsHTML;
    }

    // Get payment method text
    getPaymentMethodText(method) {
        const methodMap = {
            'COD': 'Thanh toán khi nhận hàng (COD)',
            'BankTransfer': 'Chuyển khoản ngân hàng',
            'EWallet': 'Ví điện tử',
            'cod': 'Thanh toán khi nhận hàng (COD)',
            'bank': 'Chuyển khoản ngân hàng',
            'momo': 'Ví MoMo',
            'zalopay': 'ZaloPay'
        };
        return methodMap[method] || 'Thanh toán khi nhận hàng';
    }

    // Get status text
    getStatusText(status) {
        const statusMap = {
            'Pending': 'Đang chờ xử lý',
            'Confirmed': 'Đã xác nhận',
            'Processing': 'Đang xử lý',
            'Shipping': 'Đang giao hàng',
            'Delivered': 'Đã giao hàng',
            'Cancelled': 'Đã hủy'
        };
        return statusMap[status] || status;
    }

    // Format price
    formatPrice(price) {
        return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
    }

    // Bind events
    bindEvents() {
        // Track order button
        const trackOrderBtn = document.querySelector('.btn-track-order');
        if (trackOrderBtn) {
            trackOrderBtn.addEventListener('click', (e) => {
                e.preventDefault();
                this.trackOrder();
            });
        }

        // Continue shopping button
        const continueShoppingBtns = document.querySelectorAll('.btn-continue-shopping, .btn-primary');
        continueShoppingBtns.forEach(btn => {
            if (btn.textContent.includes('Tiếp tục mua sắm') || btn.href?.includes('/Home/Index') || btn.href?.includes('/Product')) {
                btn.addEventListener('click', (e) => {
                    // Allow normal navigation, no need to prevent default
                    console.log('Navigating to continue shopping...');
                });
            }
        });

        // Orders page button
        const ordersPageBtns = document.querySelectorAll('.btn-outline, a[href*="/Account/Orders"]');
        ordersPageBtns.forEach(btn => {
            btn.addEventListener('click', (e) => {
                // Allow normal navigation
                console.log('Navigating to orders page...');
            });
        });

        // Home button
        const homeBtns = document.querySelectorAll('.btn-home, a[href*="/Home"]');
        homeBtns.forEach(btn => {
            btn.addEventListener('click', (e) => {
                // Allow normal navigation
                console.log('Navigating to home...');
            });
        });
    }

    // Track order functionality
    trackOrder() {
        if (!this.orderData || !this.orderData.orderNumber) {
            this.showNotification('Không tìm thấy thông tin đơn hàng', 'error');
            return;
        }

        const statusText = this.getStatusText(this.orderData.status);
        const message = `Mã đơn hàng: ${this.orderData.orderNumber}\n\nTrạng thái hiện tại: ${statusText}\n\nChúng tôi sẽ liên hệ với bạn khi có cập nhật về đơn hàng.`;
        
        this.showNotification(message, 'info');
        
        // In the future, this could navigate to a dedicated order tracking page
        // window.location.href = `/Order/Track/${this.orderData.orderNumber}`;
    }

    // Show notification
    showNotification(message, type = 'info') {
        // Create notification element
        const notification = document.createElement('div');
        notification.className = `notification notification-${type}`;
        notification.innerHTML = `
            <div class="notification-content">
                <span class="notification-message">${message}</span>
                <button class="notification-close" onclick="this.parentElement.parentElement.remove()">×</button>
            </div>
        `;
    
        // Add to page
        document.body.appendChild(notification);
    
        // Auto remove after 8 seconds
        setTimeout(() => {
            if (notification.parentNode) {
                notification.remove();
            }
        }, 8000);
    }
}

// Initialize order success manager when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    new OrderSuccessManager();
});

// Add notification styles
const style = document.createElement('style');
style.textContent = `
    .notification {
        position: fixed;
        top: 20px;
        right: 20px;
        max-width: 400px;
        background: white;
        border: 1px solid #ddd;
        border-radius: 8px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.15);
        z-index: 1000;
        animation: slideIn 0.3s ease-out;
    }
    
    .notification-info { border-left: 4px solid #007bff; }
    .notification-success { border-left: 4px solid #28a745; }
    .notification-error { border-left: 4px solid #dc3545; }
    
    .notification-content {
        padding: 16px;
        display: flex;
        justify-content: space-between;
        align-items: flex-start;
    }
    
    .notification-message {
        white-space: pre-line;
        flex: 1;
        margin-right: 10px;
    }
    
    .notification-close {
        background: none;
        border: none;
        font-size: 20px;
        cursor: pointer;
        color: #666;
        flex-shrink: 0;
    }
    
    @keyframes slideIn {
        from { transform: translateX(100%); opacity: 0; }
        to { transform: translateX(0); opacity: 1; }
    }
`;
document.head.appendChild(style);

// Export for other modules if needed
window.OrderSuccessManager = OrderSuccessManager; 