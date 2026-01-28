/**
 * Cart & Checkout JavaScript - Loaded on cart, checkout, order success, and payment pages
 * Contains: Cart management, Checkout flow, Order success, Payment processing
 */

(function() {
    'use strict';

    // Wait for DOM to be ready
    document.addEventListener('DOMContentLoaded', function() {
        initializeCartPage();
        initializeCheckoutPage();
        initializeOrderSuccessPage();
        initializePaymentPage();
    });

    // ============================================
    // 1. CART PAGE MANAGEMENT (from cart.js)
    // ============================================
    function initializeCartPage() {
        // Only initialize if we're on cart page
        if (!document.querySelector('.cart-container, .cart-items')) return;

        setupCartFunctionality();
    }

    function setupCartFunctionality() {
        setupQuantityControls();
        setupRemoveButtons();
        setupClearAllButton();
        setupContinueShoppingButton();
        updateMinusButtonsState();
    }

    function setupQuantityControls() {
        // Quantity plus buttons
        document.querySelectorAll('.qty-btn.plus').forEach(btn => {
            btn.addEventListener('click', async function() {
                const cartItemId = parseInt(this.dataset.cartItemId);
                const input = document.querySelector(`.qty-input[data-cart-item-id="${cartItemId}"]`);
                if (input) {
                    const currentQty = parseInt(input.value) || 1;
                    const maxStock = parseInt(input.getAttribute('data-stock')) || 999;
                    
                    if (currentQty >= maxStock) {
                        showNotification(`Chỉ còn ${maxStock} sản phẩm trong kho!`, 'warning');
                        return;
                    }
                    
                    const newQuantity = currentQty + 1;
                    await updateCartItem(cartItemId, newQuantity);
                }
            });
        });
        
        // Quantity minus buttons
        document.querySelectorAll('.qty-btn.minus').forEach(btn => {
            btn.addEventListener('click', async function() {
                const cartItemId = parseInt(this.dataset.cartItemId);
                const input = document.querySelector(`.qty-input[data-cart-item-id="${cartItemId}"]`);
                if (input) {
                    const currentQuantity = parseInt(input.value) || 1;
                    
                    // Only allow decrease if quantity > 1
                    if (currentQuantity > 1) {
                        const newQuantity = currentQuantity - 1;
                        await updateCartItem(cartItemId, newQuantity);
                    }
                    // If quantity is 1, do nothing (button is disabled)
                }
            });
        });
        
        // Quantity input change
        document.querySelectorAll('.qty-input').forEach(input => {
            input.addEventListener('input', function() {
                const maxStock = parseInt(this.getAttribute('data-stock')) || 999;
                const value = parseInt(this.value) || 1;
                
                if (value > maxStock) {
                    this.value = maxStock;
                    showNotification(`Số lượng tối đa là ${maxStock} sản phẩm!`, 'warning');
                }
                if (value < 1) {
                    this.value = 1;
                }
            });
            
            input.addEventListener('change', async function() {
                const cartItemId = parseInt(this.dataset.cartItemId);
                const newQuantity = parseInt(this.value) || 1;
                
                if (newQuantity >= 1) {
                    await updateCartItem(cartItemId, newQuantity);
                } else {
                    this.value = 1;
                    await updateCartItem(cartItemId, 1);
                }
            });
        });
    }

    function setupRemoveButtons() {
        document.querySelectorAll('.cart-remove').forEach(btn => {
            btn.addEventListener('click', async function() {
                const cartItemId = parseInt(this.dataset.cartItemId);
                const cartItem = document.querySelector(`[data-cart-item-id="${cartItemId}"]`);
                const productName = cartItem ? cartItem.querySelector('.cart-item-name')?.textContent?.trim() || 'sản phẩm này' : 'sản phẩm này';
                
                showConfirmModal(
                    'Xóa sản phẩm',
                    `Bạn có chắc chắn muốn xóa sản phẩm ${productName} khỏi giỏ hàng?`,
                    async () => {
                        await removeCartItem(cartItemId);
                    }
                );
            });
        });
    }

    function updateMinusButtonsState() {
        document.querySelectorAll('.qty-input').forEach(input => {
            const cartItemId = parseInt(input.dataset.cartItemId);
            const minusBtn = document.querySelector(`.qty-btn.minus[data-cart-item-id="${cartItemId}"]`);
            const quantity = parseInt(input.value);
            
            if (minusBtn) {
                if (quantity <= 1) {
                    minusBtn.disabled = true;
                } else {
                    minusBtn.disabled = false;
                }
            }
        });
    }

    function setupClearAllButton() {
        const clearAllBtn = document.querySelector('.clear-all-btn');
        if (clearAllBtn) {
            clearAllBtn.addEventListener('click', async function() {
                showConfirmModal(
                    'Xóa tất cả sản phẩm',
                    'Bạn có chắc chắn muốn xóa tất cả sản phẩm khỏi giỏ hàng?',
                    async () => {
                        await clearAllCart();
                    }
                );
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
                removeCartItemFromUI(cartItemId);
                updateCartSummary(data.cartTotal, data.cartCount);
                showNotification('Đã xóa sản phẩm khỏi giỏ hàng!', 'success');
                
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
        const cartItem = document.querySelector(`.cart-item[data-cart-item-id="${cartItemId}"]`);
        if (!cartItem) return;
        
        const qtyInput = cartItem.querySelector('.qty-input');
        const minusBtn = cartItem.querySelector('.qty-btn.minus');
        const plusBtn = cartItem.querySelector('.qty-btn.plus');
        const subtotalEl = cartItem.querySelector('.subtotal');
        
        if (qtyInput) {
            qtyInput.value = quantity;
            const maxStock = parseInt(qtyInput.getAttribute('data-stock')) || 999;
            
            // Update plus button based on stock
            if (plusBtn) {
                plusBtn.disabled = quantity >= maxStock || maxStock <= 0;
            }
        }
        
        // Update minus button state
        if (minusBtn) {
            minusBtn.disabled = quantity <= 1;
        }
        
        if (subtotalEl) {
            // Try to find price - could be .price or .discounted-price
            const priceEl = cartItem.querySelector('.discounted-price') || cartItem.querySelector('.price');
            if (!priceEl) {
                console.warn('Could not find price element for cart item:', cartItemId);
                return;
            }
            
            const unitPriceText = priceEl.textContent;
            const unitPrice = parseFloat(unitPriceText.replace(/[^\d]/g, ''));
            const subtotal = unitPrice * quantity;
            subtotalEl.textContent = formatCurrency(subtotal);
        }
    }

    function removeCartItemFromUI(cartItemId) {
        const cartItem = document.querySelector(`.cart-item[data-cart-item-id="${cartItemId}"]`);
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
        
        // Update cart badge (use global function from cart-helper.js if available)
        if (typeof updateCartBadge === 'function') {
            updateCartBadge(count);
        } else {
            const cartBadges = document.querySelectorAll('.header-cart-badge, .cart-count, .cart-badge');
            cartBadges.forEach(badge => {
                badge.textContent = count || 0;
            });
        }
        
        // Update product count text
        const productCountText = document.querySelector('.cart-total-row span');
        if (productCountText && productCountText.textContent.includes('sản phẩm')) {
            productCountText.textContent = `Tổng sản phẩm (${count} sản phẩm)`;
        }
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

    // ============================================
    // 2. CHECKOUT PAGE (from checkout.js)
    // ============================================
    function initializeCheckoutPage() {
        // Only initialize if we're on checkout page
        if (!document.querySelector('.checkout-container')) return;

        new CheckoutManager();
    }

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
                freeShippingThreshold: 700000,
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

        nextStep() {
            if (this.validateStep1()) {
                this.showStep(2);
            }
        }

        prevStep() {
            this.showStep(1);
        }

        showStep(stepNumber) {
            document.querySelectorAll('.checkout-step-content').forEach(step => {
                step.style.display = 'none';
            });
            
            const targetStep = document.getElementById(`step-content-${stepNumber}`);
            if (targetStep) {
                targetStep.style.display = 'block';
            }
            
            document.querySelectorAll('.step').forEach((step, index) => {
                if (index + 1 <= stepNumber) {
                    step.classList.add('active');
                } else {
                    step.classList.remove('active');
                }
            });

            window.scrollTo({ top: 0, behavior: 'smooth' });
        }

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
            
            const phoneField = document.querySelector('input[name="CustomerPhone"]');
            if (phoneField && phoneField.value.trim()) {
                if (!this.isValidPhone(phoneField.value.trim())) {
                    this.showFieldError(phoneField, 'Số điện thoại không hợp lệ');
                    isValid = false;
                }
            }
            
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

        initPaymentMethods() {
            const paymentOptions = document.querySelectorAll('.payment-option input[type="radio"]');
            
            paymentOptions.forEach(radio => {
                radio.addEventListener('change', () => {
                    document.querySelectorAll('.payment-option').forEach(option => {
                        option.classList.remove('active');
                    });
                    radio.closest('.payment-option').classList.add('active');
                });
                
                const paymentOption = radio.closest('.payment-option');
                paymentOption.addEventListener('click', () => {
                    if (!radio.checked) {
                        radio.checked = true;
                        radio.dispatchEvent(new Event('change'));
                    }
                });
            });
        }

        initShippingMethods() {
            const shippingOptions = document.querySelectorAll('.shipping-option input[type="radio"]');
            
            shippingOptions.forEach(radio => {
                radio.addEventListener('change', () => {
                    document.querySelectorAll('.shipping-option').forEach(option => {
                        option.classList.remove('active');
                    });
                    radio.closest('.shipping-option').classList.add('active');
                    this.calculateTotals();
                });
                
                const shippingOption = radio.closest('.shipping-option');
                shippingOption.addEventListener('click', () => {
                    if (!radio.checked) {
                        radio.checked = true;
                        radio.dispatchEvent(new Event('change'));
                    }
                });
            });
        }

        initFormValidation() {
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

            const phoneField = document.querySelector('input[name="CustomerPhone"]');
            if (phoneField) {
                // Auto format phone number as user types
                phoneField.addEventListener('input', (e) => {
                    const formatted = this.formatPhoneNumber(e.target.value);
                    if (formatted !== e.target.value) {
                        e.target.value = formatted;
                    }
                    // Validate
                    if (phoneField.value.trim() && !this.isValidPhone(phoneField.value.trim())) {
                        this.showFieldError(phoneField, 'Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại Việt Nam (10-11 chữ số)');
                    } else {
                        this.hideFieldError(phoneField);
                    }
                });

                // On blur: keep formatted display (backend will normalize on submit)
                phoneField.addEventListener('blur', () => {
                    const normalized = this.normalizePhoneNumber(phoneField.value);
                    if (normalized) {
                        phoneField.value = this.formatPhoneNumber(normalized);
                    }
                });
            }

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

        bindDiscountEvents() {
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

            const applyDiscountBtn = document.getElementById('apply-discount');
            if (applyDiscountBtn) {
                applyDiscountBtn.addEventListener('click', () => {
                    this.applyDiscountCode();
                });
            }

            const discountInput = document.getElementById('discount-code');
            if (discountInput) {
                discountInput.addEventListener('keypress', (e) => {
                    if (e.key === 'Enter') {
                        this.applyDiscountCode();
                    }
                });
            }
        }

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

        showDiscountMessage(message, type) {
            const messageElement = document.getElementById('discount-message');
            if (messageElement) {
                messageElement.textContent = message;
                messageElement.className = `discount-message ${type}`;
                messageElement.style.display = 'block';
            }
        }

        calculateTotals() {
            const subtotalElement = document.getElementById('subtotal');
            const shippingElement = document.getElementById('shipping-fee');
            const totalElement = document.getElementById('total-amount');
            
            if (!subtotalElement || !shippingElement || !totalElement) {
                console.warn('Missing pricing elements in DOM');
                return;
            }

            const subtotalInput = document.querySelector('input[name="SubTotal"]');
            const shippingInput = document.querySelector('input[name="ShippingFee"]');
            const totalInput = document.querySelector('input[name="TotalAmount"]');
            
            if (subtotalInput && shippingInput && totalInput) {
                const subtotal = parseFloat(subtotalInput.value) || 0;
                let shippingFee = parseFloat(shippingInput.value) || 0;
                let discountAmount = 0;

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

                if (subtotal >= this.shippingConfig.freeShippingThreshold) {
                    shippingFee = 0;
                }

                const total = subtotal + shippingFee - discountAmount;

                subtotalElement.textContent = this.formatPrice(subtotal);
                shippingElement.innerHTML = shippingFee === 0 ? '<span class="free-text">Miễn phí</span>' : this.formatPrice(shippingFee);
                totalElement.textContent = this.formatPrice(total);

                shippingInput.value = shippingFee;
                totalInput.value = total;

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

        formatPrice(price) {
            return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
        }

        isValidEmail(email) {
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return emailRegex.test(email);
        }

        isValidPhone(phone) {
            if (!phone || !phone.trim()) return false;
            
            // Remove all non-digit characters except +
            let normalized = phone.replace(/[\s\-\(\)]/g, '');
            
            // Convert +84 to 0
            if (normalized.startsWith('+84')) {
                normalized = '0' + normalized.substring(3);
            }
            
            // Vietnamese phone number: 10 digits (mobile) or 10-11 digits (landline)
            const phoneRegex = /^(0[0-9]{9,10})$/;
            if (!phoneRegex.test(normalized)) {
                return false;
            }
            
            // Check valid Vietnamese prefix
            return this.isValidVietnamesePhonePrefix(normalized);
        }

        isValidVietnamesePhonePrefix(phone) {
            if (phone.length < 10 || phone.length > 11) {
                return false;
            }

            const prefix3 = phone.substring(0, 3);
            const prefix2 = phone.substring(0, 2);

            // Valid mobile prefixes (10 digits)
            const validMobilePrefixes = [
                // Viettel
                '032', '033', '034', '035', '036', '037', '038', '039', '086', '096', '097', '098',
                // VinaPhone
                '081', '082', '083', '084', '085', '088', '091', '094',
                // MobiFone
                '070', '076', '077', '078', '079',
                // Vietnamobile
                '056', '058',
                // Gmobile
                '059'
            ];

            // Valid landline prefixes
            const validLandlinePrefixes = [
                '024', // Hà Nội
                '028', // TP.HCM
                '020', '021', '022', '023', '025', '026', '027', '029', // Các mã vùng miền Bắc
                '236', // Đà Nẵng
                '296'  // An Giang (ví dụ)
            ];

            // Check mobile (10 digits)
            if (phone.length === 10) {
                if (validMobilePrefixes.includes(prefix3)) {
                    return true;
                }
                // Old landline format (02x)
                if (prefix2 === '02') {
                    return true;
                }
                return false;
            }

            // Check landline (11 digits)
            if (phone.length === 11) {
                return validLandlinePrefixes.includes(prefix3);
            }

            return false;
        }

        formatPhoneNumber(phone) {
            if (!phone) return '';
            
            // Remove all non-digit characters except +
            let digits = phone.replace(/[^\d+]/g, '');
            
            // Convert +84 to 0
            if (digits.startsWith('+84')) {
                digits = '0' + digits.substring(3);
            }
            
            // Limit to 11 digits (max Vietnamese phone length)
            digits = digits.substring(0, 11);
            
            // Format: 0123 456 789
            if (digits.length <= 4) {
                return digits;
            } else if (digits.length <= 7) {
                return digits.substring(0, 4) + ' ' + digits.substring(4);
            } else {
                return digits.substring(0, 4) + ' ' + digits.substring(4, 7) + ' ' + digits.substring(7);
            }
        }

        normalizePhoneNumber(phone) {
            if (!phone) return '';
            
            // Remove all non-digit characters except +
            let normalized = phone.replace(/[\s\-\(\)]/g, '');
            
            // Convert +84 to 0
            if (normalized.startsWith('+84')) {
                normalized = '0' + normalized.substring(3);
            }
            
            return normalized;
        }

        showFieldError(field, message) {
            this.hideFieldError(field);
            field.classList.add('error');
            const errorElement = document.createElement('span');
            errorElement.className = 'field-validation-error';
            errorElement.textContent = message;
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
            const notification = document.createElement('div');
            notification.className = `notification notification-${type}`;
            notification.innerHTML = `
                <div class="notification-content">
                    <span class="notification-message">${message}</span>
                    <button class="notification-close" onclick="this.parentElement.parentElement.remove()">×</button>
                </div>
            `;
            document.body.appendChild(notification);
            setTimeout(() => {
                if (notification.parentNode) {
                    notification.remove();
                }
            }, 5000);
        }
    }

    // ============================================
    // 3. ORDER SUCCESS PAGE (from order-success.js)
    // ============================================
    function initializeOrderSuccessPage() {
        // Only initialize if we're on order success page
        if (!document.querySelector('.order-success-container, .order-success')) return;

        new OrderSuccessManager();
    }

    class OrderSuccessManager {
        constructor() {
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

        displayOrderInformation() {
            this.updateOrderDetails();
            this.updateCustomerInfo();
            this.displayOrderItems();
        }

        updateOrderDetails() {
            const orderNumberElements = document.querySelectorAll('.order-number .value, .order-number');
            orderNumberElements.forEach(element => {
                if (element.classList.contains('value') || !element.querySelector('.value')) {
                    element.textContent = this.orderData.orderNumber;
                }
            });

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

            const paymentMethodElements = document.querySelectorAll('.payment-method, [data-payment-method]');
            paymentMethodElements.forEach(element => {
                const methodText = this.getPaymentMethodText(this.orderData.paymentMethod);
                element.textContent = methodText;
            });

            const totalAmountElements = document.querySelectorAll('.total-amount, [data-total-amount]');
            totalAmountElements.forEach(element => {
                element.textContent = this.formatPrice(this.orderData.totalAmount);
            });

            const statusElements = document.querySelectorAll('.status, [data-status]');
            statusElements.forEach(element => {
                element.textContent = this.getStatusText(this.orderData.status);
            });
        }

        updateCustomerInfo() {
            const customerNameElements = document.querySelectorAll('.customer-name, [data-customer-name]');
            customerNameElements.forEach(element => {
                element.textContent = this.orderData.customerName;
            });

            const customerPhoneElements = document.querySelectorAll('.customer-phone, [data-customer-phone]');
            customerPhoneElements.forEach(element => {
                element.textContent = this.orderData.customerPhone;
            });

            if (this.orderData.customerEmail) {
                const customerEmailElements = document.querySelectorAll('.customer-email, [data-customer-email]');
                customerEmailElements.forEach(element => {
                    element.textContent = this.orderData.customerEmail;
                });
            }

            const customerAddressElements = document.querySelectorAll('.customer-address, [data-customer-address]');
            customerAddressElements.forEach(element => {
                element.textContent = this.orderData.shippingAddress;
            });

            if (this.orderData.notes && this.orderData.notes.trim()) {
                const customerNoteElements = document.querySelectorAll('.customer-note, [data-customer-note]');
                customerNoteElements.forEach(element => {
                    element.textContent = this.orderData.notes;
                    const container = element.closest('.info-row, .customer-note-container');
                    if (container) {
                        container.style.display = 'flex';
                    }
                });
            }
        }

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

        formatPrice(price) {
            return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
        }

        bindEvents() {
            const trackOrderBtn = document.querySelector('.btn-track-order');
            if (trackOrderBtn) {
                trackOrderBtn.addEventListener('click', (e) => {
                    e.preventDefault();
                    this.trackOrder();
                });
            }
        }

        trackOrder() {
            if (!this.orderData || !this.orderData.orderNumber) {
                this.showNotification('Không tìm thấy thông tin đơn hàng', 'error');
                return;
            }

            const statusText = this.getStatusText(this.orderData.status);
            const message = `Mã đơn hàng: ${this.orderData.orderNumber}\n\nTrạng thái hiện tại: ${statusText}\n\nChúng tôi sẽ liên hệ với bạn khi có cập nhật về đơn hàng.`;
            
            this.showNotification(message, 'info');
        }

        showNotification(message, type = 'info') {
            const notification = document.createElement('div');
            notification.className = `notification notification-${type}`;
            notification.innerHTML = `
                <div class="notification-content">
                    <span class="notification-message">${message}</span>
                    <button class="notification-close" onclick="this.parentElement.parentElement.remove()">×</button>
                </div>
            `;
            document.body.appendChild(notification);
            setTimeout(() => {
                if (notification.parentNode) {
                    notification.remove();
                }
            }, 8000);
        }
    }

    // ============================================
    // 4. PAYMENT PAGE (from payment.js)
    // ============================================
    function initializePaymentPage() {
        // Only initialize if we're on payment page
        if (!document.querySelector('.payment-container, .payment-page')) return;

        new PaymentManager();
    }

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

        getOrderDataFromStorage() {
            const orderData = localStorage.getItem('orderData');
            return orderData ? JSON.parse(orderData) : null;
        }

        loadOrderSummary() {
            if (!this.orderData) {
                alert('Không có thông tin đơn hàng. Vui lòng thực hiện lại từ đầu.');
                window.location.href = '/Cart/Index';
                return;
            }

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

            document.getElementById('subtotal').textContent = this.formatPrice(this.orderData.subtotal);
            document.getElementById('shipping-fee').textContent = this.orderData.shippingFee === 0 ? 'Miễn phí' : this.formatPrice(this.orderData.shippingFee);
            document.getElementById('total-amount').textContent = this.formatPrice(this.orderData.total);

            if (this.orderData.discount > 0) {
                const discountRow = document.getElementById('discount-row');
                document.getElementById('discount-amount').textContent = `-${this.formatPrice(this.orderData.discount)}`;
                discountRow.style.display = 'flex';
            }
        }

        formatPrice(price) {
            return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
        }

        bindEvents() {
            const paymentMethods = document.querySelectorAll('input[name="payment"]');
            paymentMethods.forEach(method => {
                method.addEventListener('change', (e) => {
                    this.updatePaymentMethod(e.target);
                });
            });

            const completeBtn = document.querySelector('.btn-complete-order');
            if (completeBtn) {
                completeBtn.addEventListener('click', () => {
                    this.completeOrder();
                });
            }
        }

        updatePaymentMethod(selectedMethod) {
            document.querySelectorAll('.payment-method').forEach(method => {
                method.classList.remove('active');
            });

            const methodContainer = selectedMethod.closest('.payment-method');
            methodContainer.classList.add('active');
            this.selectedPaymentMethod = selectedMethod.value;

            const bankInfo = document.getElementById('bank-info');
            const transferContent = document.getElementById('transfer-content');
            
            if (selectedMethod.value === 'bank') {
                if (bankInfo) bankInfo.style.display = 'block';
                if (transferContent) transferContent.textContent = `GauMeo ${this.orderNumber || '[Mã đơn hàng]'}`;
            } else {
                if (bankInfo) bankInfo.style.display = 'none';
            }
        }

        generateOrderNumber() {
            const timestamp = Date.now();
            const random = Math.floor(Math.random() * 1000).toString().padStart(3, '0');
            this.orderNumber = `GM${timestamp}${random}`;
            
            const transferContent = document.getElementById('transfer-content');
            if (transferContent) {
                transferContent.textContent = `GauMeo ${this.orderNumber}`;
            }
        }

        completeOrder() {
            if (!this.orderData) {
                alert('Không có thông tin đơn hàng');
                return;
            }

            const completeBtn = document.querySelector('.btn-complete-order');
            const originalText = completeBtn.textContent;
            completeBtn.textContent = 'Đang xử lý...';
            completeBtn.disabled = true;

            const finalOrderData = {
                orderNumber: this.orderNumber,
                ...this.orderData,
                paymentMethod: this.selectedPaymentMethod,
                status: 'pending',
                paymentStatus: this.selectedPaymentMethod === 'cod' ? 'pending' : 'waiting_payment',
                createdAt: new Date().toISOString()
            };

            setTimeout(() => {
                const orders = JSON.parse(localStorage.getItem('orders') || '[]');
                orders.push(finalOrderData);
                localStorage.setItem('orders', JSON.stringify(orders));
                localStorage.removeItem('orderData');
                localStorage.removeItem('shoppingCart');
                localStorage.setItem('lastOrder', JSON.stringify(finalOrderData));
                window.location.href = '/Cart/OrderSuccess';
            }, 2000);
        }
    }

    // ============================================
    // SHARED UTILITY FUNCTIONS
    // ============================================
    function formatCurrency(amount) {
        return Math.round(amount).toLocaleString('vi-VN') + ' đ';
    }

    function getAntiForgeryToken() {
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        return token ? token.value : '';
    }

    function showLoading(show) {
        if (show) {
            document.body.style.cursor = 'wait';
        } else {
            document.body.style.cursor = 'default';
        }
    }

    function showConfirmModal(title, message, onConfirm) {
        // Remove existing modal if any
        const existingModal = document.getElementById('cart-confirm-modal');
        if (existingModal) {
            existingModal.remove();
        }

        // Create modal overlay
        const modalOverlay = document.createElement('div');
        modalOverlay.id = 'cart-confirm-modal';
        modalOverlay.className = 'cart-confirm-modal-overlay';
        modalOverlay.innerHTML = `
            <div class="cart-confirm-modal">
                <div class="cart-confirm-modal-header">
                    <h3 class="cart-confirm-modal-title">${title}</h3>
                    <button class="cart-confirm-modal-close" aria-label="Đóng">&times;</button>
                </div>
                <div class="cart-confirm-modal-body">
                    <p class="cart-confirm-modal-message">${message}</p>
                </div>
                <div class="cart-confirm-modal-footer">
                    <button class="cart-confirm-btn cart-confirm-btn-cancel">Hủy</button>
                    <button class="cart-confirm-btn cart-confirm-btn-confirm">Xác nhận</button>
                </div>
            </div>
        `;

        document.body.appendChild(modalOverlay);
        document.body.style.overflow = 'hidden';

        // Close handlers
        const closeModal = () => {
            modalOverlay.classList.remove('show');
            setTimeout(() => {
                modalOverlay.remove();
                document.body.style.overflow = '';
            }, 200);
        };

        const closeBtn = modalOverlay.querySelector('.cart-confirm-modal-close');
        const cancelBtn = modalOverlay.querySelector('.cart-confirm-btn-cancel');
        const confirmBtn = modalOverlay.querySelector('.cart-confirm-btn-confirm');

        closeBtn.addEventListener('click', closeModal);
        cancelBtn.addEventListener('click', closeModal);
        confirmBtn.addEventListener('click', () => {
            closeModal();
            if (onConfirm) {
                onConfirm();
            }
        });

        // Close on overlay click
        modalOverlay.addEventListener('click', (e) => {
            if (e.target === modalOverlay) {
                closeModal();
            }
        });

        // Show modal with animation
        setTimeout(() => {
            modalOverlay.classList.add('show');
        }, 10);
    }

    function showNotification(message, type = 'success') {
        const existingNotifications = document.querySelectorAll('.notification');
        existingNotifications.forEach(notification => notification.remove());
        
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
        
        document.body.appendChild(notification);
        
        setTimeout(() => {
            notification.classList.add('show');
        }, 100);
        
        setTimeout(() => {
            notification.classList.remove('show');
            setTimeout(() => {
                notification.remove();
            }, 300);
        }, 3000);
    }

    // ============================================
    // INJECT STYLES
    // ============================================
    (function injectStyles() {
        if (document.getElementById('cart-checkout-styles')) return;
        
        const style = document.createElement('style');
        style.id = 'cart-checkout-styles';
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
                max-width: 320px;
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
                padding: 8px 12px;
                display: flex;
                align-items: center;
                gap: 8px;
            }
            
            .notification-icon {
                flex-shrink: 0;
                width: 20px;
                height: 20px;
                display: flex;
                align-items: center;
                justify-content: center;
            }
            
            .notification-message {
                font-size: 0.9375rem;
                color: #333;
                line-height: 1.4;
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
            
            .cart-confirm-modal-overlay {
                position: fixed;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: rgba(0, 0, 0, 0.5);
                display: flex;
                align-items: center;
                justify-content: center;
                z-index: 10000;
                opacity: 0;
                transition: opacity 0.2s ease;
            }
            
            .cart-confirm-modal-overlay.show {
                opacity: 1;
            }
            
            .cart-confirm-modal {
                background: white;
                border-radius: 12px;
                box-shadow: 0 8px 32px rgba(0, 0, 0, 0.2);
                max-width: 450px;
                width: 90%;
                max-height: 90vh;
                overflow: hidden;
                transform: scale(0.9);
                transition: transform 0.2s ease;
            }
            
            .cart-confirm-modal-overlay.show .cart-confirm-modal {
                transform: scale(1);
            }
            
            .cart-confirm-modal-header {
                padding: 20px 24px;
                border-bottom: 1px solid #e5e5e5;
                display: flex;
                justify-content: space-between;
                align-items: center;
            }
            
            .cart-confirm-modal-title {
                margin: 0;
                font-size: 1.25rem;
                font-weight: 600;
                color: #333;
            }
            
            .cart-confirm-modal-close {
                background: none;
                border: none;
                font-size: 28px;
                color: #999;
                cursor: pointer;
                padding: 0;
                width: 32px;
                height: 32px;
                display: flex;
                align-items: center;
                justify-content: center;
                border-radius: 4px;
                transition: all 0.2s;
            }
            
            .cart-confirm-modal-close:hover {
                background-color: #f5f5f5;
                color: #333;
            }
            
            .cart-confirm-modal-body {
                padding: 24px;
            }
            
            .cart-confirm-modal-message {
                margin: 0;
                font-size: 1rem;
                color: #666;
                line-height: 1.6;
                word-break: break-word;
                white-space: normal;
            }
            
            .cart-confirm-modal-footer {
                padding: 16px 24px;
                border-top: 1px solid #e5e5e5;
                display: flex;
                justify-content: flex-end;
                gap: 12px;
            }
            
            .cart-confirm-btn {
                padding: 10px 24px;
                border: none;
                border-radius: 6px;
                font-size: 0.9375rem;
                font-weight: 500;
                cursor: pointer;
                transition: all 0.2s;
            }
            
            .cart-confirm-btn-cancel {
                background-color: #f5f5f5;
                color: #333;
            }
            
            .cart-confirm-btn-cancel:hover {
                background-color: #e5e5e5;
            }
            
            .cart-confirm-btn-confirm {
                background-color: #dc3545;
                color: white;
            }
            
            .cart-confirm-btn-confirm:hover {
                background-color: #c82333;
            }
        `;
        document.head.appendChild(style);
    })();

    // Export classes for use in other scripts if needed
    window.CheckoutManager = CheckoutManager;
    window.OrderSuccessManager = OrderSuccessManager;
    window.PaymentManager = PaymentManager;
})();
