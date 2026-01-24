// Profile Page JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Get all menu items and content sections
    const menuItems = document.querySelectorAll('.profile-menu-item');
    const contentSections = document.querySelectorAll('.profile-content-section');

    // Section mapping
    const sectionMap = {
        'my-account': 'my-account-section',
        'address': 'address-section',
        'order-history': 'order-history-section',
        'my-wishlist': 'my-wishlist-section',
        'password-setting': 'password-setting-section'
    };

    // Function to switch sections
    function switchSection(sectionKey) {
        // Hide all content sections
        contentSections.forEach(section => {
            section.classList.remove('active');
        });

        // Remove active class from all menu items
        menuItems.forEach(item => {
            if (!item.classList.contains('logout-item')) {
                item.classList.remove('active');
                item.style.backgroundColor = '';
                const p = item.querySelector('p');
                if (p) {
                    p.style.color = '#333';
                    p.style.fontWeight = 'normal';
                }
            }
        });

        // Show selected section
        const targetSectionId = sectionMap[sectionKey];
        const targetSection = document.getElementById(targetSectionId);
        if (targetSection) {
            targetSection.classList.add('active');
            
            // Initialize order history when order-history section is shown
            if (sectionKey === 'order-history') {
                console.log('Order history section shown, initializing...');
                setTimeout(() => {
                    initializeOrderHistory();
                }, 500);
            }
            
            // Update cart badge when switching to wishlist section (from cart-helper.js)
            if (sectionKey === 'my-wishlist') {
                refreshCartBadge();
            }
        }

        // Add active class to clicked menu item
        const activeMenuItem = document.querySelector(`[data-section="${sectionKey}"]`);
        if (activeMenuItem && !activeMenuItem.classList.contains('logout-item')) {
            activeMenuItem.classList.add('active');
            activeMenuItem.style.backgroundColor = '#28a745';
            const p = activeMenuItem.querySelector('p');
            if (p) {
                p.style.color = 'white';
                p.style.fontWeight = '600';
            }
        }
    }

    // Add click handlers to menu items
    menuItems.forEach(item => {
        if (!item.classList.contains('logout-item')) {
            item.addEventListener('click', () => {
                const sectionKey = item.getAttribute('data-section');
                if (sectionKey) {
                    switchSection(sectionKey);
                    window.location.hash = sectionKey;
                }
            });
        }
    });

    // Handle browser back/forward navigation
    window.addEventListener('hashchange', function() {
        const hash = window.location.hash.substring(1);
        if (hash && sectionMap[hash]) {
            switchSection(hash);
        }
    });

    // Initialize based on URL hash or default to my-account
    const initialHash = window.location.hash.substring(1);
    if (initialHash && sectionMap[initialHash]) {
        // Check if password-setting tab is available for external login users
        if (initialHash === 'password-setting') {
            const passwordMenuItem = document.querySelector('[data-section="password-setting"]');
            if (!passwordMenuItem) {
                // If password-setting menu item doesn't exist (external login user), redirect to my-account
                switchSection('my-account');
                console.log('Password setting not available for external login users, redirected to my-account');
            } else {
                switchSection(initialHash);
            }
        } else {
        switchSection(initialHash);
        // If initial section is order-history, initialize it
        if (initialHash === 'order-history') {
            setTimeout(() => {
                initializeOrderHistory();
            }, 500);
            }
        }
    } else {
        switchSection('my-account');
    }

    // Wishlist functionality
    initializeWishlist();
    
    // Initialize cart badge (from cart-helper.js)
    refreshCartBadge();

    // Profile Edit Mode Management
    const editProfileBtn = document.getElementById('edit-profile-btn');
    const cancelEditBtn = document.getElementById('cancel-edit-btn');
    const profileViewMode = document.getElementById('profile-view-mode');
    const profileEditMode = document.getElementById('profile-edit-mode');
    const sectionTitleText = document.getElementById('section-title-text');

    // Switch to edit mode
    if (editProfileBtn) {
        editProfileBtn.addEventListener('click', function() {
            profileViewMode.style.display = 'none';
            profileEditMode.style.display = 'block';
            
            // Change title to "Chỉnh sửa hồ sơ"
            if (sectionTitleText) {
                sectionTitleText.textContent = 'Chỉnh sửa hồ sơ';
            }
        });
    }

    // Switch back to view mode
    if (cancelEditBtn) {
        cancelEditBtn.addEventListener('click', function() {
            profileEditMode.style.display = 'none';
            profileViewMode.style.display = 'block';
            
            // Change title back to "Tài khoản của tôi"
            if (sectionTitleText) {
                sectionTitleText.textContent = 'Tài khoản của tôi';
            }
        });
    }

    // Date Picker Functionality
    const datePickerInput = document.getElementById('textMemberBirthdate');
    const hiddenDateInput = document.getElementById('hiddenDateOfBirth');
    const calendarIcon = document.querySelector('#birthdatePicker .input-group-text');

    // Function to convert dd/MM/yyyy to yyyy-MM-dd
    function convertToISODate(ddmmyyyy) {
        if (!ddmmyyyy) return '';
        const parts = ddmmyyyy.split('/');
        if (parts.length === 3) {
            return `${parts[2]}-${parts[1].padStart(2, '0')}-${parts[0].padStart(2, '0')}`;
        }
        return '';
    }

    // Function to convert yyyy-MM-dd to dd/MM/yyyy
    function convertToDisplayDate(isoDate) {
        if (!isoDate) return '';
        const parts = isoDate.split('-');
        if (parts.length === 3) {
            return `${parts[2]}/${parts[1]}/${parts[0]}`;
        }
        return '';
    }

    // Create a hidden date input for the calendar picker
    let hiddenCalendarInput = document.createElement('input');
    hiddenCalendarInput.type = 'date';
    hiddenCalendarInput.style.position = 'absolute';
    hiddenCalendarInput.style.left = '-9999px';
    hiddenCalendarInput.style.top = '-9999px';
    hiddenCalendarInput.style.opacity = '0';
    hiddenCalendarInput.style.width = '1px';
    hiddenCalendarInput.style.height = '1px';
    hiddenCalendarInput.style.pointerEvents = 'none';
    document.body.appendChild(hiddenCalendarInput);

    // Handle calendar icon click and input click
    function openDatePicker() {
        // Set the hidden date input value
        const currentValue = datePickerInput.value;
        if (currentValue) {
            const isoDate = convertToISODate(currentValue);
            if (isoDate) {
                hiddenCalendarInput.value = isoDate;
            }
        }
        
        // Trigger the date picker - multiple methods for better compatibility
        try {
            hiddenCalendarInput.style.pointerEvents = 'auto';
            hiddenCalendarInput.focus();
            
            // Create and dispatch a click event
            const clickEvent = new MouseEvent('click', {
                view: window,
                bubbles: true,
                cancelable: true
            });
            hiddenCalendarInput.dispatchEvent(clickEvent);
            
            hiddenCalendarInput.style.pointerEvents = 'none';
        } catch (e) {
            console.log('Date picker error:', e);
            // Fallback - just click
            hiddenCalendarInput.click();
        }
    }

    if (calendarIcon && datePickerInput) {
        // Calendar icon click handler
        calendarIcon.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            openDatePicker();
        });
        
        // Input field click handler
        datePickerInput.addEventListener('click', function(e) {
            openDatePicker();
        });

        // Handle date selection
        hiddenCalendarInput.addEventListener('change', function() {
            const selectedDate = this.value;
            if (selectedDate) {
                const displayDate = convertToDisplayDate(selectedDate);
                datePickerInput.value = displayDate;
                
                // Update the hidden input for form submission
                if (hiddenDateInput) {
                    hiddenDateInput.value = selectedDate;
                }
            }
        });

        // Handle manual input in the text field
        datePickerInput.addEventListener('blur', function() {
            const value = this.value.trim();
            if (value) {
                // Validate dd/MM/yyyy format
                const dateRegex = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
                const match = value.match(dateRegex);
                
                if (match) {
                    const day = parseInt(match[1]);
                    const month = parseInt(match[2]);
                    const year = parseInt(match[3]);
                    
                    // Basic validation
                    if (day >= 1 && day <= 31 && month >= 1 && month <= 12 && year >= 1900 && year <= new Date().getFullYear()) {
                        const formattedValue = `${day.toString().padStart(2, '0')}/${month.toString().padStart(2, '0')}/${year}`;
                        this.value = formattedValue;
                        const isoDate = convertToISODate(formattedValue);
                        if (hiddenDateInput) {
                            hiddenDateInput.value = isoDate;
                        }
                    } else {
                        alert('Vui lòng nhập ngày sinh hợp lệ (dd/MM/yyyy)');
                        this.focus();
                    }
                } else if (value !== '') {
                    alert('Vui lòng nhập ngày sinh theo định dạng dd/MM/yyyy');
                    this.focus();
                }
            }
        });
    }

    // File Upload Handling
    const uploadBtn = document.querySelector('.upload-btn');
    const fileInput = document.querySelector('input[type="file"][name="AvatarFile"]');
    const uploadNote = document.querySelector('.upload-note');
    const currentAvatar = document.querySelector('.current-avatar');

    if (uploadBtn && fileInput) {
        uploadBtn.addEventListener('click', function() {
            fileInput.click();
        });

        fileInput.addEventListener('change', function() {
            const file = this.files[0];
            if (file) {
                // Validate file type
                const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png'];
                if (!allowedTypes.includes(file.type)) {
                    alert('Chỉ chấp nhận file ảnh (.jpg, .png, .jpeg)');
                    this.value = '';
                    uploadNote.textContent = 'Không có tệp nào được chọn';
                    return;
                }

                // Check file size (max 5MB)
                if (file.size > 5 * 1024 * 1024) {
                    alert('Kích thước file không được vượt quá 5MB');
                    this.value = '';
                    uploadNote.textContent = 'Không có tệp nào được chọn';
                    return;
                }

                uploadNote.textContent = file.name;
                
                // Preview image
                const reader = new FileReader();
                reader.onload = function(e) {
                    if (currentAvatar) {
                        currentAvatar.style.backgroundImage = `url(${e.target.result})`;
                        currentAvatar.textContent = '';
                    }
                };
                reader.readAsDataURL(file);
            } else {
                uploadNote.textContent = 'Không có tệp nào được chọn';
            }
        });
    }

    // Profile edit form validation
    const editProfileForm = document.querySelector('#profile-edit-mode form');
    if (editProfileForm) {
        editProfileForm.addEventListener('submit', function(e) {
            const fullName = this.querySelector('input[name="FullName"]');
            const phoneNumber = this.querySelector('input[name="PhoneNumber"]');
            
            let isValid = true;
            let errorMessage = '';

            // Validate full name
            if (fullName && fullName.value.trim().length < 2) {
                errorMessage += 'Họ và tên phải có ít nhất 2 ký tự.\n';
                isValid = false;
            }

            // Validate phone number
            if (phoneNumber && phoneNumber.value.trim()) {
                const phoneRegex = /^[0-9]{10,11}$/;
                if (!phoneRegex.test(phoneNumber.value.trim())) {
                    errorMessage += 'Số điện thoại phải có 10-11 chữ số.\n';
                    isValid = false;
                }
            }

            if (!isValid) {
                e.preventDefault();
                alert(errorMessage);
                return false;
            }
        });
    }

    // Password form validation
    const passwordForm = document.querySelector('.password-form');
    if (passwordForm) {
        passwordForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            const currentPassword = document.getElementById('current-password');
            const newPassword = document.getElementById('new-password');
            const confirmPassword = document.getElementById('confirm-password');
            
            if (!currentPassword || !newPassword || !confirmPassword) {
                alert('Không tìm thấy các trường mật khẩu.');
                return false;
            }
            
            let isValid = true;
            let errorMessage = '';

            // Validate current password
            if (!currentPassword.value.trim()) {
                errorMessage += 'Vui lòng nhập mật khẩu hiện tại.\n';
                isValid = false;
            }

            // Validate new password
            if (newPassword.value.length < 8) {
                errorMessage += 'Mật khẩu mới phải có ít nhất 8 ký tự.\n';
                isValid = false;
            }

            // Check password complexity
            const hasLower = /[a-z]/.test(newPassword.value);
            const hasUpper = /[A-Z]/.test(newPassword.value);
            const hasNumber = /\d/.test(newPassword.value);

            if (!hasLower || !hasUpper || !hasNumber) {
                errorMessage += 'Mật khẩu mới phải chứa ít nhất một chữ thường, chữ hoa và số.\n';
                isValid = false;
            }

            // Validate confirm password
            if (newPassword.value !== confirmPassword.value) {
                errorMessage += 'Mật khẩu xác nhận không khớp.\n';
                isValid = false;
            }

            if (!isValid) {
                showNotification(errorMessage.replace(/\n/g, '<br>'), 'error');
                return false;
            }

            // Submit to server
            const formData = new FormData();
            formData.append('currentPassword', currentPassword.value);
            formData.append('newPassword', newPassword.value);
            formData.append('confirmPassword', confirmPassword.value);
            formData.append('__RequestVerificationToken', document.querySelector('input[name="__RequestVerificationToken"]').value);

            const submitBtn = this.querySelector('.profile-update-btn');
            const originalText = submitBtn.innerHTML;
            submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Đang xử lý...';
            submitBtn.disabled = true;

            fetch('/Account/ChangePassword', {
                method: 'POST',
                body: formData
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    showNotification(data.message, 'success');
                    this.reset();
                } else {
                    showNotification(data.message || 'Có lỗi xảy ra khi đổi mật khẩu.', 'error');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                showNotification('Có lỗi xảy ra khi đổi mật khẩu.', 'error');
            })
            .finally(() => {
                submitBtn.innerHTML = originalText;
                submitBtn.disabled = false;
            });
        });
    }

    // Add address button functionality
    const addAddressBtn = document.querySelector('.profile-add-btn');
    if (addAddressBtn) {
        addAddressBtn.addEventListener('click', function() {
            alert('Chức năng thêm địa chỉ đang được phát triển.');
        });
    }

    // Logout confirmation
    const logoutBtn = document.querySelector('.logout-btn');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', function(e) {
            if (!confirm('Bạn có chắc chắn muốn đăng xuất?')) {
                e.preventDefault();
                return false;
            }
        });
    }

    // Add hover effects for menu items
    menuItems.forEach(item => {
        if (!item.classList.contains('logout-item')) {
            item.addEventListener('mouseenter', function() {
                if (!this.classList.contains('active')) {
                    this.style.backgroundColor = '#f8f9fa';
                }
            });

            item.addEventListener('mouseleave', function() {
                if (!this.classList.contains('active')) {
                    this.style.backgroundColor = '';
                } else {
                    this.style.backgroundColor = '#28a745';
                }
            });
        }
    });

    // Order history interactions
    const orderRows = document.querySelectorAll('.order-row');
    orderRows.forEach(row => {
        row.addEventListener('click', function() {
            orderRows.forEach(r => r.classList.remove('selected'));
            this.classList.add('selected');
        });
    });

    // Initialize avatar upload functionality
    initializeAvatarUpload();

    console.log('Profile page JavaScript loaded successfully');
}); 

// Wishlist functionality
function initializeWishlist() {
    loadWishlistItems();
    
    // Add all to cart functionality
    const addAllToCartBtn = document.getElementById('add-all-to-cart-btn');
    if (addAllToCartBtn) {
        addAllToCartBtn.addEventListener('click', async function() {
            const productItems = document.querySelectorAll('.wishlist-product-item');
            if (productItems.length > 0) {
                let addedCount = 0;
                let hasVariantsProducts = [];
                
                // Lọc sản phẩm không có biến thể và thêm vào giỏ hàng
                for (const item of productItems) {
                    const productId = item.dataset.productId;
                    const hasVariants = item.dataset.hasVariants === 'true';
                    const productTitle = item.querySelector('.product-title').textContent;
                    
                    if (hasVariants) {
                        hasVariantsProducts.push(productTitle);
                    } else {
                        const success = await addToCartFromWishlist(productId);
                        if (success) {
                            addedCount++;
                        }
                    }
                }
                
                // Hiển thị thông báo
                if (addedCount > 0) {
                    showNotification(`Đã thêm ${addedCount} sản phẩm vào giỏ hàng!`, 'success');
                    refreshCartBadge(); // from cart-helper.js
                }
                
                if (hasVariantsProducts.length > 0) {
                    showNotification(`${hasVariantsProducts.length} sản phẩm có biến thể cần vào trang chi tiết để chọn`, 'info');
                }
            }
        });
    }
}

// Load wishlist items from API
async function loadWishlistItems() {
    try {
        const response = await fetch('/Wishlist/GetItems');
        const data = await response.json();
        
        if (data.success && data.items.length > 0) {
            displayWishlistItems(data.items);
            showWishlistProducts();
        } else {
            showEmptyWishlist();
        }
    } catch (error) {
        console.error('Error loading wishlist:', error);
        showEmptyWishlist();
    }
}

// Display wishlist items
function displayWishlistItems(items) {
    const container = document.getElementById('wishlist-products-container');
    if (!container) return;
    
    container.innerHTML = `
        <div class="wishlist-header">
            <span id="wishlist-count">${items.length} sản phẩm trong danh sách yêu thích của tôi</span>
        </div>
        <div class="wishlist-products-list">
            ${items.map(item => `
                <div class="wishlist-product-item" data-product-id="${item.productId}" data-has-variants="${item.hasVariants}">
                    <div class="product-image">
                        <img src="${item.productImage || '/images/products/default-product.jpg'}" alt="${item.productName}">
                    </div>
                    <div class="product-info">
                        <h5 class="product-title">${item.productName}</h5>
                        <p class="product-sku">SKU: #${item.productId.toString().padStart(6, '0')}</p>
                        <p class="product-category">${item.categoryName || 'Chưa phân loại'}</p>
                        <p class="product-brand">Thương hiệu: ${item.brandName || 'Chưa có'}</p>
                        <p class="product-date">Đã thêm: ${item.addedAt}</p>
                    </div>
                    <div class="product-price">
                        <span class="price">${formatPrice(item.productPrice)}</span>
                    </div>
                    <div class="product-actions">
                        <button type="button" class="btn btn-success add-to-cart-btn" data-product-id="${item.productId}" data-has-variants="${item.hasVariants}">
                            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M3 3H5L5.4 5M7 13H17L21 5H5.4M7 13L5.4 5M7 13L4.7 15.3C4.3 15.7 4.6 16.5 5.1 16.5H17M17 13V17C17 18.1 16.1 19 15 19H9C7.9 19 7 18.1 7 17V13M9 21C9.6 21 10 20.6 10 20S9.6 19 9 19 8 19.4 8 20 8.4 21 9 21ZM20 21C20.6 21 21 20.6 21 20S20.6 19 20 19 19 19.4 19 20 19.4 21 20 21Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                            </svg>
                            Thêm vào giỏ
                        </button>
                        <button type="button" class="btn btn-outline-secondary remove-from-wishlist-btn" data-product-id="${item.productId}">
                            <svg width="18" height="18" viewBox="0 0 448 512" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path fill="currentColor" d="M192 188v216c0 6.627-5.373 12-12 12h-24c-6.627 0-12-5.373-12-12V188c0-6.627 5.373-12 12-12h24c6.627 0 12 5.373 12 12zm100-12h-24c-6.627 0-12 5.373-12 12v216c0 6.627 5.373 12 12 12h24c6.627 0 12-5.373 12-12V188c0-6.627-5.373-12-12-12zm132-96c13.255 0 24 10.745 24 24v12c0 6.627-5.373 12-12 12h-20v336c0 26.51-21.49 48-48 48H80c-26.51 0-48-21.49-48-48V128H12c-6.627 0-12-5.373-12-12v-12c0-13.255 10.745-24 24-24h74.411l34.018-56.696A48 48 0 0 1 173.589 0h100.823a48 48 0 0 1 41.16 23.304L349.589 80H424zm-269.611 0h139.223L276.16 50.913A6 6 0 0 0 271.015 48h-94.028a6 6 0 0 0-5.145 2.913L154.389 80zM368 128H80v330a6 6 0 0 0 6 6h276a6 6 0 0 0 6-6V128z"/>
                            </svg>
                        </button>
                    </div>
                </div>
            `).join('')}
        </div>
    `;
    
    // Add event listeners
    addWishlistEventListeners();
}

// Add event listeners for wishlist actions
function addWishlistEventListeners() {
    const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
    const removeFromWishlistButtons = document.querySelectorAll('.remove-from-wishlist-btn');
    
    addToCartButtons.forEach(button => {
        button.addEventListener('click', async function() {
            const productId = this.dataset.productId;
            const hasVariants = this.dataset.hasVariants === 'true';
            const productItem = this.closest('.wishlist-product-item');
            const productTitle = productItem.querySelector('.product-title').textContent;
            
            // Nếu sản phẩm có biến thể, chuyển hướng đến trang chi tiết
            if (hasVariants) {
                window.location.href = `/Product/Detail/${productId}`;
                return;
            }
            
            // Nếu không có biến thể, thêm trực tiếp vào giỏ hàng
            const success = await addToCartFromWishlist(productId);
            if (success) {
                showNotification(`Đã thêm "${productTitle}" vào giỏ hàng!`, 'success');
                
                // Cập nhật số lượng giỏ hàng trong header (from cart-helper.js)
                refreshCartBadge();
            } else {
                showNotification('Không thể thêm sản phẩm vào giỏ hàng!', 'error');
            }
        });
    });
    
    removeFromWishlistButtons.forEach(button => {
        button.addEventListener('click', async function() {
            const productId = this.dataset.productId;
            const productItem = this.closest('.wishlist-product-item');
            const productTitle = productItem.querySelector('.product-title').textContent;
            
            const success = await removeFromWishlist(productId);
            if (success) {
                productItem.remove();
                showNotification(`Đã xóa "${productTitle}" khỏi danh sách yêu thích!`, 'info');
                
                // Check if wishlist is empty
                const remainingProducts = document.querySelectorAll('.wishlist-product-item');
                if (remainingProducts.length === 0) {
                    showEmptyWishlist();
                } else {
                    updateWishlistCount();
                }
                
                // Update header wishlist count
                updateHeaderWishlistCount();
            } else {
                showNotification('Không thể xóa sản phẩm!', 'error');
            }
        });
    });
}

function showWishlistProducts() {
    const wishlistEmptyState = document.getElementById('wishlist-empty-state');
    const wishlistProductsContainer = document.getElementById('wishlist-products-container');
    const addAllToCartBtn = document.getElementById('add-all-to-cart-btn');
    
    if (wishlistEmptyState) wishlistEmptyState.style.display = 'none';
    if (wishlistProductsContainer) wishlistProductsContainer.style.display = 'block';
    if (addAllToCartBtn) addAllToCartBtn.style.display = 'block';
    
    updateWishlistCount();
}

function showEmptyWishlist() {
    const wishlistEmptyState = document.getElementById('wishlist-empty-state');
    const wishlistProductsContainer = document.getElementById('wishlist-products-container');
    const addAllToCartBtn = document.getElementById('add-all-to-cart-btn');
    
    if (wishlistEmptyState) wishlistEmptyState.style.display = 'block';
    if (wishlistProductsContainer) wishlistProductsContainer.style.display = 'none';
    if (addAllToCartBtn) addAllToCartBtn.style.display = 'none';
    
    updateWishlistCount();
}

function updateWishlistCount() {
    const wishlistCount = document.getElementById('wishlist-count');
    const productItems = document.querySelectorAll('.wishlist-product-item');
    const count = productItems.length;
    
    if (wishlistCount) {
        wishlistCount.textContent = `${count} sản phẩm trong danh sách yêu thích của tôi`;
    }
}

// Helper functions for API calls
async function removeFromWishlist(productId) {
    try {
        const response = await fetch('/api/wishlist/remove', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value
            },
            body: JSON.stringify({ productId: parseInt(productId) })
        });
        const data = await response.json();
        return data.success;
    } catch (error) {
        console.error('Error removing from wishlist:', error);
        return false;
    }
}

// addToCart function is now provided by cart-helper.js (global)
// Wrapper to maintain backward compatibility with return value
async function addToCartFromWishlist(productId) {
    const result = await addToCart(productId, 1, {}, null);
    return result.success;
}

async function updateHeaderWishlistCount() {
    try {
        const response = await fetch('/Wishlist/GetCount');
        const data = await response.json();
        
        // Update wishlist count in header
        const headerWishlistCount = document.querySelector('.header-wishlist-count');
        if (headerWishlistCount) {
            headerWishlistCount.textContent = data.count;
            headerWishlistCount.style.display = data.count > 0 ? 'inline' : 'none';
        }
    } catch (error) {
        console.error('Error updating header wishlist count:', error);
    }
}

// updateCartBadge and refreshCartBadge functions are now provided by cart-helper.js (global)

function formatPrice(price) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(price);
}



// Order History Functions
function initializeOrderHistory() {
    console.log('Initializing order history...');
    
    const orderSearchInput = document.getElementById('order-search-input');
    const orderStatusFilter = document.getElementById('order-status-filter');
    const orderRows = document.querySelectorAll('.order-row');
    const backToOrdersBtn = document.getElementById('back-to-orders-btn');
    const orderViewMode = document.getElementById('order-view-mode');
    const orderDetailMode = document.getElementById('order-detail-mode');

    console.log('Found elements:', {
        orderSearchInput: !!orderSearchInput,
        orderStatusFilter: !!orderStatusFilter,
        orderRows: orderRows.length,
        backToOrdersBtn: !!backToOrdersBtn,
        orderViewMode: !!orderViewMode,
        orderDetailMode: !!orderDetailMode
    });

    // If elements not found, retry after 100ms
    if (!orderSearchInput || !orderStatusFilter || orderRows.length === 0) {
        console.log('Elements not found, retrying...');
        setTimeout(() => {
            initializeOrderHistory();
        }, 100);
        return;
    }

    // Search and filter functionality
    if (orderSearchInput) {
        orderSearchInput.addEventListener('input', filterOrders);
        console.log('Added search input listener');
    }

    if (orderStatusFilter) {
        orderStatusFilter.addEventListener('change', filterOrders);
        console.log('Added status filter listener');
    }

    // Click handlers for order rows
    orderRows.forEach((row, index) => {
        row.addEventListener('click', function(e) {
            console.log('Order row clicked:', index);
            const orderId = this.getAttribute('data-order-id');
            console.log('Order ID:', orderId);
            if (orderId) {
                showOrderDetail(orderId);
            }
        });
        row.style.cursor = 'pointer';
    });
    console.log('Added click listeners to', orderRows.length, 'order rows');

    // Back button handler
    if (backToOrdersBtn) {
        backToOrdersBtn.addEventListener('click', function() {
            console.log('Back button clicked');
            orderDetailMode.style.display = 'none';
            orderViewMode.style.display = 'block';
        });
        console.log('Added back button listener');
    }

    function filterOrders() {
        console.log('Filtering orders...');
        const searchTerm = orderSearchInput ? orderSearchInput.value.toLowerCase() : '';
        const statusFilter = orderStatusFilter ? orderStatusFilter.value : '';
        
        console.log('Search term:', searchTerm);
        console.log('Status filter:', statusFilter);

        orderRows.forEach((row, index) => {
            const orderNumber = row.getAttribute('data-order-number') || '';
            const priceText = row.querySelector('.order-col-price')?.textContent || '';
            const statusElement = row.querySelector('.order-col-status .badge');
            const status = statusElement ? statusElement.textContent : '';

            const matchesSearch = orderNumber.toLowerCase().includes(searchTerm) || 
                                priceText.toLowerCase().includes(searchTerm);
            const matchesStatus = !statusFilter || status.includes(getStatusText(statusFilter));

            if (matchesSearch && matchesStatus) {
                row.style.display = 'block';
            } else {
                row.style.display = 'none';
            }
        });
    }

    function getStatusText(status) {
        const statusMap = {
            'Pending': 'Chờ xác nhận',
            'Confirmed': 'Đã xác nhận',
            'Processing': 'Đang xử lý',
            'Shipping': 'Đang giao hàng',
            'Delivered': 'Đã giao hàng',
            'Cancelled': 'Đã hủy'
        };
        return statusMap[status] || status;
    }

    async function showOrderDetail(orderId) {
        try {
            const response = await fetch(`/Account/GetOrderDetail/${orderId}`);
            const data = await response.json();

            if (data.success) {
                const order = data.order;
                const detailContent = document.getElementById('order-detail-content');
                const detailTitle = document.getElementById('order-detail-title');

                detailTitle.textContent = `Chi tiết đơn hàng ${order.orderNumber}`;

                const statusText = getStatusText(order.status);
                const statusBadge = getStatusBadge(order.status);

                detailContent.innerHTML = `
                    <div class="order-detail-info">
                        <div class="row">
                            <div class="col-md-6">
                                <h5>Thông tin đơn hàng</h5>
                                <div class="detail-row">
                                    <label>Mã đơn hàng:</label>
                                    <span>${order.orderNumber}</span>
                                </div>
                                <div class="detail-row">
                                    <label>Ngày đặt hàng:</label>
                                    <span>${order.createdAt}</span>
                                </div>
                                <div class="detail-row">
                                    <label>Trạng thái:</label>
                                    <span>${statusBadge}</span>
                                </div>
                                <div class="detail-row">
                                    <label>Phương thức thanh toán:</label>
                                    <span>${getPaymentMethodText(order.paymentMethod)}</span>
                                </div>
                                <div class="detail-row">
                                    <label>Trạng thái thanh toán:</label>
                                    <span>${getPaymentStatusText(order.paymentStatus)}</span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <h5>Thông tin khách hàng</h5>
                                <div class="detail-row">
                                    <label>Họ tên:</label>
                                    <span>${order.customerName}</span>
                                </div>
                                <div class="detail-row">
                                    <label>Số điện thoại:</label>
                                    <span>${order.customerPhone}</span>
                                </div>
                                <div class="detail-row">
                                    <label>Email:</label>
                                    <span>${order.customerEmail}</span>
                                </div>
                                <div class="detail-row">
                                    <label>Địa chỉ giao hàng:</label>
                                    <span>${order.shippingAddress}</span>
                                </div>
                            </div>
                        </div>
                        
                        <div class="order-items-section mt-4">
                            <h5>Chi tiết sản phẩm</h5>
                            <div class="table-responsive">
                                <table class="table table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Sản phẩm</th>
                                            <th>Số lượng</th>
                                            <th>Đơn giá</th>
                                            <th>Thành tiền</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        ${order.orderItems.map(item => `
                                            <tr>
                                                <td>${item.productName}</td>
                                                <td>${item.quantity}</td>
                                                <td>${formatPrice(item.unitPrice)}</td>
                                                <td>${formatPrice(item.totalPrice)}</td>
                                            </tr>
                                        `).join('')}
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="order-summary mt-4">
                            <div class="row">
                                <div class="col-md-6 offset-md-6">
                                    <div class="summary-row">
                                        <label>Tổng tiền hàng:</label>
                                        <span>${formatPrice(order.subTotal)}</span>
                                    </div>
                                    <div class="summary-row">
                                        <label>Phí vận chuyển:</label>
                                        <span>${formatPrice(order.shippingFee)}</span>
                                    </div>
                                    <div class="summary-row">
                                        <label>Giảm giá:</label>
                                        <span>-${formatPrice(order.discountAmount)}</span>
                                    </div>
                                    <div class="summary-row total">
                                        <label>Tổng thanh toán:</label>
                                        <span>${formatPrice(order.totalAmount)}</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        ${order.notes ? `
                            <div class="order-notes mt-4">
                                <h5>Ghi chú</h5>
                                <p>${order.notes}</p>
                            </div>
                        ` : ''}
                    </div>
                `;

                orderViewMode.style.display = 'none';
                orderDetailMode.style.display = 'block';
            } else {
                showNotification(data.message, 'error');
            }
        } catch (error) {
            console.error('Error fetching order detail:', error);
            showNotification('Có lỗi xảy ra khi tải chi tiết đơn hàng', 'error');
        }
    }

    function getStatusBadge(status) {
        const badgeMap = {
            'Pending': '<span class="badge bg-warning">Chờ xác nhận</span>',
            'Confirmed': '<span class="badge bg-info">Đã xác nhận</span>',
            'Processing': '<span class="badge bg-primary">Đang xử lý</span>',
            'Shipping': '<span class="badge bg-secondary">Đang giao hàng</span>',
            'Delivered': '<span class="badge bg-success">Đã giao hàng</span>',
            'Cancelled': '<span class="badge bg-danger">Đã hủy</span>'
        };
        return badgeMap[status] || `<span class="badge bg-secondary">${status}</span>`;
    }

    function getPaymentMethodText(method) {
        const methodMap = {
            'COD': 'Thanh toán khi nhận hàng',
            'Bank Transfer': 'Chuyển khoản ngân hàng',
            'E-Wallet': 'Ví điện tử'
        };
        return methodMap[method] || method;
    }

    function getPaymentStatusText(status) {
        const statusMap = {
            'Pending': 'Chờ thanh toán',
            'Paid': 'Đã thanh toán',
            'Failed': 'Thanh toán thất bại'
        };
        return statusMap[status] || status;
    }
}

// Notification function
function showNotification(message, type = 'success') {
  // Remove existing notifications
  const existingNotifications = document.querySelectorAll('.profile-notification');
  existingNotifications.forEach(n => n.remove());

  const notification = document.createElement('div');
  notification.className = `profile-notification ${type}`;
  notification.innerHTML = `
    <div class="notification-content">
      <div class="notification-icon">
        ${type === 'success' ? 
          '<svg width="20" height="20" fill="currentColor" viewBox="0 0 16 16"><path d="M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425a.247.247 0 0 1 .02-.022Z"/></svg>' : 
          type === 'error' ?
          '<svg width="20" height="20" fill="currentColor" viewBox="0 0 16 16"><path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/><path d="M7.002 11a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 4.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 4.995z"/></svg>' :
          '<svg width="20" height="20" fill="currentColor" viewBox="0 0 16 16"><path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"/></svg>'
        }
      </div>
      <span class="notification-message">${message}</span>
      <button class="notification-close" onclick="this.parentElement.parentElement.remove()">
        <svg width="16" height="16" fill="currentColor" viewBox="0 0 16 16"><path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z"/></svg>
      </button>
    </div>
  `;

  // Add styles
  const bgColor = type === 'success' ? '#d4edda' : type === 'error' ? '#f8d7da' : '#d1ecf1';
  const textColor = type === 'success' ? '#155724' : type === 'error' ? '#721c24' : '#0c5460';
  const borderColor = type === 'success' ? '#c3e6cb' : type === 'error' ? '#f5c6cb' : '#bee5eb';

  notification.style.cssText = `
    position: fixed;
    top: 20px;
    right: 20px;
    z-index: 10000;
    max-width: 400px;
    background: ${bgColor};
    color: ${textColor};
    border: 1px solid ${borderColor};
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