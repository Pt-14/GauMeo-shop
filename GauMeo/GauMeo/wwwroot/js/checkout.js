// Checkout Page JavaScript - Simplified and Clean Version

class CheckoutManager {
    constructor() {
        this.currentDiscount = 0;
        this.currentDiscountCode = '';
        this.discountCodes = {
            'GAUMEO10': { type: 'percentage', value: 10, minOrder: 100000 },
            'FREESHIP': { type: 'shipping', value: 0, minOrder: 200000 },
            'SAVE50K': { type: 'fixed', value: 50000, minOrder: 300000 },
            'NEWUSER': { type: 'percentage', value: 15, minOrder: 150000 }
        };
        
        this.shippingConfig = {
            freeShippingThreshold: 700000, // Free shipping for orders over 700k
            defaultFee: 30000
        };
        
        this.init();
    }

    init() {
        this.initStepNavigation();
        this.initPaymentMethods();
        this.initShippingMethods();
        this.initFormValidation();
        this.bindDiscountEvents();
        this.calculateTotals();
    }

    // Initialize step navigation
    initStepNavigation() {
        const btnNextStep = document.getElementById('btn-next-step');
        const btnPrevStep = document.getElementById('btn-prev-step');
        
        if (btnNextStep) {
            btnNextStep.addEventListener('click', (e) => {
                e.preventDefault();
                this.nextStep();
            });
        }
        
        if (btnPrevStep) {
            btnPrevStep.addEventListener('click', (e) => {
                e.preventDefault();
                this.prevStep();
            });
        }
    }

    // Navigate to next step
    nextStep() {
        if (this.validateStep1()) {
            this.showStep(2);
        }
    }

    // Navigate to previous step
    prevStep() {
        this.showStep(1);
    }

    // Show specific step
    showStep(stepNumber) {
        // Hide all step contents
        document.querySelectorAll('.checkout-step-content').forEach(step => {
            step.style.display = 'none';
        });
        
        // Show target step
        const targetStep = document.getElementById(`step-content-${stepNumber}`);
        if (targetStep) {
            targetStep.style.display = 'block';
        }
        
        // Update step indicators
        document.querySelectorAll('.step').forEach((step, index) => {
            if (index + 1 <= stepNumber) {
                step.classList.add('active');
            } else {
                step.classList.remove('active');
            }
        });

        // Scroll to top
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    // Validate step 1 (delivery information)
    validateStep1() {
        const requiredFields = ['CustomerName', 'CustomerPhone', 'ShippingAddress'];
        let isValid = true;
        
        requiredFields.forEach(fieldName => {
            const field = document.querySelector(`input[name="${fieldName}"], textarea[name="${fieldName}"]`);
            if (field && !field.value.trim()) {
                this.showFieldError(field, 'Vui lòng điền thông tin này');
                isValid = false;
            } else if (field) {
                this.hideFieldError(field);
            }
        });
        
        // Validate phone number
        const phoneField = document.querySelector('input[name="CustomerPhone"]');
        if (phoneField && phoneField.value.trim()) {
            if (!this.isValidPhone(phoneField.value.trim())) {
                this.showFieldError(phoneField, 'Số điện thoại không hợp lệ');
                isValid = false;
            }
        }
        
        // Validate email if provided
        const emailField = document.querySelector('input[name="CustomerEmail"]');
        if (emailField && emailField.value.trim()) {
            if (!this.isValidEmail(emailField.value.trim())) {
                this.showFieldError(emailField, 'Email không hợp lệ');
                isValid = false;
            }
        }
        
        if (!isValid) {
            this.showNotification('Vui lòng điền đầy đủ thông tin bắt buộc và kiểm tra lại.', 'error');
        }
        
        return isValid;
    }

    // Initialize payment methods
    initPaymentMethods() {
        const paymentOptions = document.querySelectorAll('.payment-option input[type="radio"]');
        
        paymentOptions.forEach(radio => {
            radio.addEventListener('change', () => {
                // Remove active class from all options
                document.querySelectorAll('.payment-option').forEach(option => {
                    option.classList.remove('active');
                });
                
                // Add active class to selected option
                radio.closest('.payment-option').classList.add('active');
            });
            
            // Click on payment option to select
            const paymentOption = radio.closest('.payment-option');
            paymentOption.addEventListener('click', () => {
                if (!radio.checked) {
                    radio.checked = true;
                    radio.dispatchEvent(new Event('change'));
                }
            });
        });
    }

    // Initialize shipping methods
    initShippingMethods() {
        const shippingOptions = document.querySelectorAll('.shipping-option input[type="radio"]');
        
        shippingOptions.forEach(radio => {
            radio.addEventListener('change', () => {
                // Remove active class from all options
                document.querySelectorAll('.shipping-option').forEach(option => {
                    option.classList.remove('active');
                });
                
                // Add active class to selected option
                radio.closest('.shipping-option').classList.add('active');
                
                // Recalculate totals with new shipping price
                this.calculateTotals();
            });
            
            // Click on shipping option to select
            const shippingOption = radio.closest('.shipping-option');
            shippingOption.addEventListener('click', () => {
                if (!radio.checked) {
                    radio.checked = true;
                    radio.dispatchEvent(new Event('change'));
                }
            });
        });
    }

    // Initialize form validation
    initFormValidation() {
        // Real-time validation for required fields
        const requiredFields = document.querySelectorAll('input[required], textarea[required]');
        
        requiredFields.forEach(field => {
            field.addEventListener('blur', () => {
                if (!field.value.trim()) {
                    this.showFieldError(field, 'Trường này là bắt buộc');
                } else {
                    this.hideFieldError(field);
                }
            });
        });

        // Phone validation
        const phoneField = document.querySelector('input[name="CustomerPhone"]');
        if (phoneField) {
            phoneField.addEventListener('input', () => {
                if (phoneField.value.trim() && !this.isValidPhone(phoneField.value.trim())) {
                    this.showFieldError(phoneField, 'Số điện thoại không hợp lệ');
                } else {
                    this.hideFieldError(phoneField);
                }
            });
        }

        // Email validation
        const emailField = document.querySelector('input[name="CustomerEmail"]');
        if (emailField) {
            emailField.addEventListener('blur', () => {
                if (emailField.value.trim() && !this.isValidEmail(emailField.value.trim())) {
                    this.showFieldError(emailField, 'Email không hợp lệ');
                } else {
                    this.hideFieldError(emailField);
                }
            });
        }
    }

    // Bind discount events
    bindDiscountEvents() {
        // Toggle discount form
        const discountToggle = document.getElementById('discount-toggle');
        const discountForm = document.getElementById('discount-form');
        
        if (discountToggle && discountForm) {
        discountToggle.addEventListener('click', () => {
            if (discountForm.style.display === 'none' || discountForm.style.display === '') {
                discountForm.style.display = 'block';
            } else {
                discountForm.style.display = 'none';
            }
        });
        }

        // Apply discount code
        const applyDiscountBtn = document.getElementById('apply-discount');
        if (applyDiscountBtn) {
        applyDiscountBtn.addEventListener('click', () => {
            this.applyDiscountCode();
        });
        }

        // Enter key for discount code
        const discountInput = document.getElementById('discount-code');
        if (discountInput) {
        discountInput.addEventListener('keypress', (e) => {
            if (e.key === 'Enter') {
                this.applyDiscountCode();
            }
        });
        }
    }

    // Apply discount code
    applyDiscountCode() {
        const discountInput = document.getElementById('discount-code');
        if (!discountInput) return;

        const code = discountInput.value.trim().toUpperCase();

        if (!code) {
            this.showDiscountMessage('Vui lòng nhập mã giảm giá', 'error');
            return;
        }

        const discount = this.discountCodes[code];
        
        if (!discount) {
            this.showDiscountMessage('Mã giảm giá không hợp lệ', 'error');
            return;
        }

        // Get current subtotal
        const subtotalInput = document.querySelector('input[name="SubTotal"]');
        const subtotal = subtotalInput ? parseFloat(subtotalInput.value) : 0;

        if (subtotal < discount.minOrder) {
            this.showDiscountMessage(`Đơn hàng tối thiểu ${this.formatPrice(discount.minOrder)} để sử dụng mã này`, 'error');
            return;
        }

        this.currentDiscount = discount.value;
        this.currentDiscountCode = code;
        
        this.showDiscountMessage('Áp dụng mã giảm giá thành công!', 'success');
        this.calculateTotals();
    }

    // Show discount message
    showDiscountMessage(message, type) {
        const messageElement = document.getElementById('discount-message');
        if (messageElement) {
            messageElement.textContent = message;
            messageElement.className = `discount-message ${type}`;
            messageElement.style.display = 'block';
        }
    }

    // Calculate totals based on server data
    calculateTotals() {
        const subtotalElement = document.getElementById('subtotal');
        const shippingElement = document.getElementById('shipping-fee');
        const totalElement = document.getElementById('total-amount');
        
        if (!subtotalElement || !shippingElement || !totalElement) {
            console.warn('Missing pricing elements in DOM');
            return;
        }

        // Get values from hidden fields
        const subtotalInput = document.querySelector('input[name="SubTotal"]');
        const shippingInput = document.querySelector('input[name="ShippingFee"]');
        const totalInput = document.querySelector('input[name="TotalAmount"]');
        
        if (subtotalInput && shippingInput && totalInput) {
            const subtotal = parseFloat(subtotalInput.value) || 0;
            let shippingFee = parseFloat(shippingInput.value) || 0;
        let discountAmount = 0;

            // Apply discount if applicable
        if (this.currentDiscount > 0) {
            const discount = this.discountCodes[this.currentDiscountCode];
                
            if (discount) {
                if (discount.type === 'percentage') {
                    discountAmount = Math.floor(subtotal * discount.value / 100);
                } else if (discount.type === 'fixed') {
                    discountAmount = Math.min(discount.value, subtotal);
                } else if (discount.type === 'shipping') {
                    shippingFee = 0;
                }
            }
        }

            // Free shipping for orders over threshold
        if (subtotal >= this.shippingConfig.freeShippingThreshold) {
            shippingFee = 0;
        }

            const total = subtotal + shippingFee - discountAmount;

            // Update UI
            subtotalElement.textContent = this.formatPrice(subtotal);
            shippingElement.innerHTML = shippingFee === 0 ? '<span class="free-text">Miễn phí</span>' : this.formatPrice(shippingFee);
            totalElement.textContent = this.formatPrice(total);

            // Update hidden fields
            shippingInput.value = shippingFee;
            totalInput.value = total;

            // Show/hide discount row
            const discountRow = document.getElementById('discount-row');
            if (discountRow) {
                if (discountAmount > 0) {
                    const discountAmountElement = document.getElementById('discount-amount');
                    if (discountAmountElement) {
                        discountAmountElement.textContent = `-${this.formatPrice(discountAmount)}`;
                    }
                    discountRow.style.display = 'flex';
                } else {
                    discountRow.style.display = 'none';
                }
            }
        }
    }

    // Utility methods
    formatPrice(price) {
        return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
    }

    isValidEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }

    isValidPhone(phone) {
        const phoneRegex = /^[0-9]{10,11}$/;
        return phoneRegex.test(phone.replace(/[\s\-\(\)]/g, ''));
    }

    showFieldError(field, message) {
    // Remove existing error
        this.hideFieldError(field);
        
        // Add error class
        field.classList.add('error');
        
        // Create error message
        const errorElement = document.createElement('span');
        errorElement.className = 'field-validation-error';
        errorElement.textContent = message;
        
        // Insert after field
        field.parentNode.insertBefore(errorElement, field.nextSibling);
    }

    hideFieldError(field) {
        field.classList.remove('error');
        const errorElement = field.parentNode.querySelector('.field-validation-error');
        if (errorElement) {
            errorElement.remove();
        }
    }

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
    
    // Auto remove after 5 seconds
        setTimeout(() => {
            if (notification.parentNode) {
                notification.remove();
            }
    }, 5000);
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    // Only initialize if we're on checkout page
    if (document.querySelector('.checkout-container')) {
        new CheckoutManager();
    }
});

// CSS for styling
const style = document.createElement('style');
style.textContent = `
    .error {
        border-color: #dc3545 !important;
        box-shadow: 0 0 0 3px rgba(220, 53, 69, 0.1) !important;
    }
    
    .field-validation-error {
        color: #dc3545;
        font-size: 0.875rem;
        margin-top: 4px;
        display: block;
    }
    
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
                    align-items: center;
    }
    
                .notification-close {
                    background: none;
                    border: none;
        font-size: 20px;
                    cursor: pointer;
        color: #666;
        margin-left: 12px;
    }
    
    @keyframes slideIn {
        from { transform: translateX(100%); opacity: 0; }
        to { transform: translateX(0); opacity: 1; }
    }
    
    .discount-message {
        margin-top: 8px;
        padding: 8px;
        border-radius: 4px;
        font-size: 0.875rem;
    }
    
    .discount-message.success {
        background-color: #d4edda;
        color: #155724;
        border: 1px solid #c3e6cb;
    }
    
    .discount-message.error {
        background-color: #f8d7da;
        color: #721c24;
        border: 1px solid #f5c6cb;
    }
`;
document.head.appendChild(style); 