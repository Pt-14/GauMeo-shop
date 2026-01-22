// Payment Page JavaScript

class PaymentManager {
    constructor() {
        this.orderData = this.getOrderDataFromStorage();
        this.selectedPaymentMethod = 'cod';
        
        this.init();
    }

    init() {
        this.loadOrderSummary();
        this.bindEvents();
        this.generateOrderNumber();
    }

    // Load order data from localStorage
    getOrderDataFromStorage() {
        const orderData = localStorage.getItem('orderData');
        return orderData ? JSON.parse(orderData) : null;
    }

    // Load order summary
    loadOrderSummary() {
        if (!this.orderData) {
            alert('Không có thông tin đơn hàng. Vui lòng thực hiện lại từ đầu.');
            window.location.href = '/Cart/Index';
            return;
        }

        // Display order items
        const orderItemsContainer = document.querySelector('.order-items');
        if (this.orderData.items && this.orderData.items.length > 0) {
            const itemsHTML = this.orderData.items.map(item => `
                <div class="order-item">
                    <div class="order-item-image">
                        ${item.image ? `<img src="${item.image}" alt="${item.name}" style="width:100%;height:100%;object-fit:cover;">` : 'Hình ảnh'}
                    </div>
                    <div class="order-item-info">
                        <div class="order-item-name">${item.name}</div>
                        <div class="order-item-details">
                            ${item.variant ? `Loại: ${item.variant}` : ''}
                            ${item.size ? ` • Kích thước: ${item.size}` : ''}
                        </div>
                        <div class="order-item-price">
                            <span class="order-item-quantity">SL: ${item.quantity}</span>
                            <span class="order-item-total">${this.formatPrice(item.price * item.quantity)}</span>
                        </div>
                    </div>
                </div>
            `).join('');
            orderItemsContainer.innerHTML = itemsHTML;
        }

        // Display price summary
        document.getElementById('subtotal').textContent = this.formatPrice(this.orderData.subtotal);
        document.getElementById('shipping-fee').textContent = this.orderData.shippingFee === 0 ? 'Miễn phí' : this.formatPrice(this.orderData.shippingFee);
        document.getElementById('total-amount').textContent = this.formatPrice(this.orderData.total);

        // Show discount if exists
        if (this.orderData.discount > 0) {
            const discountRow = document.getElementById('discount-row');
            document.getElementById('discount-amount').textContent = `-${this.formatPrice(this.orderData.discount)}`;
            discountRow.style.display = 'flex';
        }
    }

    // Format price
    formatPrice(price) {
        return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
    }

    // Bind events
    bindEvents() {
        // Payment method change
        const paymentMethods = document.querySelectorAll('input[name="payment"]');
        paymentMethods.forEach(method => {
            method.addEventListener('change', (e) => {
                this.updatePaymentMethod(e.target);
            });
        });

        // Complete order button
        const completeBtn = document.querySelector('.btn-complete-order');
        completeBtn.addEventListener('click', () => {
            this.completeOrder();
        });
    }

    // Update payment method
    updatePaymentMethod(selectedMethod) {
        // Remove active class from all methods
        document.querySelectorAll('.payment-method').forEach(method => {
            method.classList.remove('active');
        });

        // Add active class to selected method
        const methodContainer = selectedMethod.closest('.payment-method');
        methodContainer.classList.add('active');

        // Update selected payment method
        this.selectedPaymentMethod = selectedMethod.value;

        // Show/hide bank info
        const bankInfo = document.getElementById('bank-info');
        const transferContent = document.getElementById('transfer-content');
        
        if (selectedMethod.value === 'bank') {
            bankInfo.style.display = 'block';
            transferContent.textContent = `GauMeo ${this.orderNumber || '[Mã đơn hàng]'}`;
        } else {
            bankInfo.style.display = 'none';
        }
    }

    // Generate order number
    generateOrderNumber() {
        const timestamp = Date.now();
        const random = Math.floor(Math.random() * 1000).toString().padStart(3, '0');
        this.orderNumber = `GM${timestamp}${random}`;
        
        // Update transfer content if bank payment is selected
        const transferContent = document.getElementById('transfer-content');
        if (transferContent) {
            transferContent.textContent = `GauMeo ${this.orderNumber}`;
        }
    }

    // Complete order
    completeOrder() {
        if (!this.orderData) {
            alert('Không có thông tin đơn hàng');
            return;
        }

        // Show loading state
        const completeBtn = document.querySelector('.btn-complete-order');
        const originalText = completeBtn.textContent;
        completeBtn.textContent = 'Đang xử lý...';
        completeBtn.disabled = true;

        // Prepare final order data
        const finalOrderData = {
            orderNumber: this.orderNumber,
            ...this.orderData,
            paymentMethod: this.selectedPaymentMethod,
            status: 'pending',
            paymentStatus: this.selectedPaymentMethod === 'cod' ? 'pending' : 'waiting_payment',
            createdAt: new Date().toISOString()
        };

        // Simulate API call
        setTimeout(() => {
            // Save order to localStorage
            const orders = JSON.parse(localStorage.getItem('orders') || '[]');
            orders.push(finalOrderData);
            localStorage.setItem('orders', JSON.stringify(orders));

            // Clear order data and cart
            localStorage.removeItem('orderData');
            localStorage.removeItem('shoppingCart');

            // Save order for success page
            localStorage.setItem('lastOrder', JSON.stringify(finalOrderData));

            // Redirect to success page
            window.location.href = '/Cart/OrderSuccess';
        }, 2000);
    }
}

// Initialize payment manager when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    new PaymentManager();
});

// Export for other modules if needed
window.PaymentManager = PaymentManager; 