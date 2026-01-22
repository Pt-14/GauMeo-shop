// Brand Detail Page JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Initialize all functionality
    initializeFilters();
    initializeSorting();
    initializeViewControls();
    initializePagination();
    initializeProductCards();
    initializeLazyLoading();
    initializeAnalytics();
    
    // Add smooth scrolling
    addSmoothScrolling();
    
    // Add loading animations
    addLoadingAnimations();
});

// Filter functionality
function initializeFilters() {
    const filterCheckboxes = document.querySelectorAll('.filter-checkbox');
    const filterRadios = document.querySelectorAll('.filter-radio');
    const applyFiltersBtn = document.querySelector('.apply-filters-btn');
    const clearFiltersBtn = document.querySelector('.clear-filters-btn');
    
    let activeFilters = {
        categories: [],
        priceRange: null,
        rating: null,
        status: []
    };
    
    // Handle checkbox filters
    filterCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const filterType = this.name;
            const filterValue = this.value;
            
            if (filterType === 'category') {
                if (this.checked) {
                    activeFilters.categories.push(filterValue);
                } else {
                    activeFilters.categories = activeFilters.categories.filter(cat => cat !== filterValue);
                }
            } else if (filterType === 'status') {
                if (this.checked) {
                    activeFilters.status.push(filterValue);
                } else {
                    activeFilters.status = activeFilters.status.filter(status => status !== filterValue);
                }
            }
            
            updateFilterCounts();
        });
    });
    
    // Handle radio filters
    filterRadios.forEach(radio => {
        radio.addEventListener('change', function() {
            const filterType = this.name;
            const filterValue = this.value;
            
            if (filterType === 'priceRange') {
                activeFilters.priceRange = filterValue;
            } else if (filterType === 'rating') {
                activeFilters.rating = filterValue;
            }
            
            updateFilterCounts();
        });
    });
    
    // Apply filters button
    if (applyFiltersBtn) {
        applyFiltersBtn.addEventListener('click', function() {
            applyFilters(activeFilters);
        });
    }
    
    // Clear filters button
    if (clearFiltersBtn) {
        clearFiltersBtn.addEventListener('click', function() {
            clearAllFilters();
        });
    }
}

function applyFilters(filters) {
    const productCards = document.querySelectorAll('.product-card');
    let visibleCount = 0;
    
    productCards.forEach(card => {
        let shouldShow = true;
        
        // Check category filter
        if (filters.categories.length > 0) {
            const productCategory = card.getAttribute('data-category');
            if (!filters.categories.includes(productCategory)) {
                shouldShow = false;
            }
        }
        
        // Check price range filter
        if (filters.priceRange && shouldShow) {
            const productPrice = parseFloat(card.getAttribute('data-price'));
            const [minPrice, maxPrice] = filters.priceRange.split('-').map(p => 
                p === '+' ? Infinity : parseFloat(p)
            );
            
            if (productPrice < minPrice || (maxPrice !== Infinity && productPrice > maxPrice)) {
                shouldShow = false;
            }
        }
        
        // Check rating filter
        if (filters.rating && shouldShow) {
            const productRating = parseFloat(card.getAttribute('data-rating'));
            if (productRating < parseFloat(filters.rating)) {
                shouldShow = false;
            }
        }
        
        // Check status filter
        if (filters.status.length > 0 && shouldShow) {
            const productStatus = card.getAttribute('data-status').split(',');
            const hasMatchingStatus = filters.status.some(status => productStatus.includes(status));
            if (!hasMatchingStatus) {
                shouldShow = false;
            }
        }
        
        // Show/hide card with animation
        if (shouldShow) {
            card.style.display = 'block';
            card.style.animation = 'fadeIn 0.3s ease';
            visibleCount++;
        } else {
            card.style.display = 'none';
        }
    });
    
    // Update results count
    updateResultsCount(visibleCount);
    
    // Show no results message if needed
    if (visibleCount === 0) {
        showNoResultsMessage();
    } else {
        hideNoResultsMessage();
    }
}

function clearAllFilters() {
    // Uncheck all checkboxes
    document.querySelectorAll('.filter-checkbox').forEach(checkbox => {
        checkbox.checked = false;
    });
    
    // Uncheck all radio buttons
    document.querySelectorAll('.filter-radio').forEach(radio => {
        radio.checked = false;
    });
    
    // Reset active filters
    const activeFilters = {
        categories: [],
        priceRange: null,
        rating: null,
        status: []
    };
    
    // Show all products
    applyFilters(activeFilters);
}

function updateFilterCounts() {
    // Update the count display for each filter option
    const filterOptions = document.querySelectorAll('.filter-option');
    
    filterOptions.forEach(option => {
        const countElement = option.querySelector('.filter-count');
        if (countElement) {
            // This would typically be calculated based on current filter state
            // For now, we'll just show the original count
            const originalCount = countElement.getAttribute('data-original-count') || 
                                countElement.textContent.match(/\((\d+)\)/)?.[1] || '0';
            countElement.textContent = `(${originalCount})`;
        }
    });
}

function updateResultsCount(count) {
    const resultsCountElement = document.querySelector('.results-count');
    if (resultsCountElement) {
        resultsCountElement.textContent = `${count} sản phẩm`;
    }
}

// Sorting functionality
function initializeSorting() {
    const sortSelect = document.querySelector('.sort-select');
    
    if (sortSelect) {
        sortSelect.addEventListener('change', function() {
            const sortBy = this.value;
            sortProducts(sortBy);
        });
    }
}

function sortProducts(sortBy) {
    const productsGrid = document.querySelector('.products-grid');
    const productCards = Array.from(document.querySelectorAll('.product-card'));
    
    productCards.sort((a, b) => {
        switch (sortBy) {
            case 'price-asc':
                return parseFloat(a.getAttribute('data-price')) - parseFloat(b.getAttribute('data-price'));
            case 'price-desc':
                return parseFloat(b.getAttribute('data-price')) - parseFloat(a.getAttribute('data-price'));
            case 'name-asc':
                return a.querySelector('.product-name').textContent.localeCompare(
                    b.querySelector('.product-name').textContent
                );
            case 'name-desc':
                return b.querySelector('.product-name').textContent.localeCompare(
                    a.querySelector('.product-name').textContent
                );
            case 'rating':
                return parseFloat(b.getAttribute('data-rating')) - parseFloat(a.getAttribute('data-rating'));
            case 'newest':
                return new Date(b.getAttribute('data-date')) - new Date(a.getAttribute('data-date'));
            default:
                return 0;
        }
    });
    
    // Reorder products in the grid
    productCards.forEach(card => {
        productsGrid.appendChild(card);
    });
    
    // Add animation
    productCards.forEach((card, index) => {
        card.style.animation = `slideIn 0.3s ease ${index * 0.05}s`;
    });
}

// View controls (grid/list)
function initializeViewControls() {
    const viewButtons = document.querySelectorAll('.view-btn');
    const productsGrid = document.querySelector('.products-grid');
    
    viewButtons.forEach(button => {
        button.addEventListener('click', function() {
            const viewType = this.getAttribute('data-view');
            
            // Update active button
            viewButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');
            
            // Update grid layout
            if (viewType === 'list') {
                productsGrid.classList.add('list-view');
            } else {
                productsGrid.classList.remove('list-view');
            }
        });
    });
}

// Pagination functionality
function initializePagination() {
    const paginationNumbers = document.querySelectorAll('.pagination-number');
    const prevBtn = document.querySelector('.pagination-btn.prev');
    const nextBtn = document.querySelector('.pagination-btn.next');
    
    let currentPage = 1;
    const itemsPerPage = 12;
    
    paginationNumbers.forEach(number => {
        number.addEventListener('click', function() {
            const page = parseInt(this.textContent);
            if (page !== currentPage) {
                goToPage(page);
            }
        });
    });
    
    if (prevBtn) {
        prevBtn.addEventListener('click', function() {
            if (currentPage > 1) {
                goToPage(currentPage - 1);
            }
        });
    }
    
    if (nextBtn) {
        nextBtn.addEventListener('click', function() {
            const totalPages = Math.ceil(document.querySelectorAll('.product-card').length / itemsPerPage);
            if (currentPage < totalPages) {
                goToPage(currentPage + 1);
            }
        });
    }
}

function goToPage(page) {
    const productCards = document.querySelectorAll('.product-card');
    const itemsPerPage = 12;
    const startIndex = (page - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;
    
    // Hide all products
    productCards.forEach(card => {
        card.style.display = 'none';
    });
    
    // Show products for current page
    for (let i = startIndex; i < endIndex && i < productCards.length; i++) {
        productCards[i].style.display = 'block';
        productCards[i].style.animation = 'fadeIn 0.3s ease';
    }
    
    // Update pagination
    updatePagination(page);
    
    // Scroll to top of products section
    const productsSection = document.querySelector('.products-section');
    if (productsSection) {
        productsSection.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
}

function updatePagination(currentPage) {
    const paginationNumbers = document.querySelectorAll('.pagination-number');
    const prevBtn = document.querySelector('.pagination-btn.prev');
    const nextBtn = document.querySelector('.pagination-btn.next');
    
    // Update active page
    paginationNumbers.forEach(number => {
        number.classList.remove('active');
        if (parseInt(number.textContent) === currentPage) {
            number.classList.add('active');
        }
    });
    
    // Update prev/next buttons
    if (prevBtn) {
        prevBtn.disabled = currentPage === 1;
    }
    
    if (nextBtn) {
        const totalPages = Math.ceil(document.querySelectorAll('.product-card').length / 12);
        nextBtn.disabled = currentPage === totalPages;
    }
}

// Product card interactions
function initializeProductCards() {
    const productCards = document.querySelectorAll('.product-card');
    
    productCards.forEach(card => {
        // Add to cart button
        const addToCartBtn = card.querySelector('.add-to-cart-btn');
        if (addToCartBtn) {
            addToCartBtn.addEventListener('click', function(e) {
                e.stopPropagation();
                addToCart(card);
            });
        }
        
        // Quick view button
        const quickViewBtn = card.querySelector('.quick-view-btn');
        if (quickViewBtn) {
            quickViewBtn.addEventListener('click', function(e) {
                e.stopPropagation();
                showQuickView(card);
            });
        }
        
        // Product card click for detail view
        card.addEventListener('click', function() {
            const productId = this.getAttribute('data-product-id');
            if (productId) {
                window.location.href = `/Product/Detail/${productId}`;
            }
        });
    });
}

function addToCart(productCard) {
    const productId = productCard.getAttribute('data-product-id');
    const productName = productCard.querySelector('.product-name').textContent;
    
    // Add loading state
    const addToCartBtn = productCard.querySelector('.add-to-cart-btn');
    const originalText = addToCartBtn.textContent;
    addToCartBtn.textContent = 'Đang thêm...';
    addToCartBtn.disabled = true;
    
    // Simulate API call
    setTimeout(() => {
        // Reset button
        addToCartBtn.textContent = originalText;
        addToCartBtn.disabled = false;
        
        // Show success message
        showNotification('Đã thêm sản phẩm vào giỏ hàng!', 'success');
        
        // Update cart count
        updateCartCount();
        
        // Track event
        trackEvent('add_to_cart', {
            product_id: productId,
            product_name: productName
        });
    }, 1000);
}

function showQuickView(productCard) {
    const productId = productCard.getAttribute('data-product-id');
    const productName = productCard.querySelector('.product-name').textContent;
    const productImage = productCard.querySelector('.product-image img').src;
    const productPrice = productCard.querySelector('.current-price').textContent;
    
    // Create modal
    const modal = document.createElement('div');
    modal.className = 'quick-view-modal';
    modal.innerHTML = `
        <div class="modal-overlay">
            <div class="modal-content">
                <button class="modal-close">&times;</button>
                <div class="quick-view-content">
                    <div class="quick-view-image">
                        <img src="${productImage}" alt="${productName}">
                    </div>
                    <div class="quick-view-info">
                        <h3>${productName}</h3>
                        <div class="price">${productPrice}</div>
                        <p>Thông tin chi tiết sản phẩm sẽ được hiển thị ở đây...</p>
                        <button class="view-detail-btn">Xem chi tiết</button>
                    </div>
                </div>
            </div>
        </div>
    `;
    
    document.body.appendChild(modal);
    
    // Close modal
    modal.querySelector('.modal-close').addEventListener('click', () => {
        modal.remove();
    });
    
    modal.querySelector('.modal-overlay').addEventListener('click', (e) => {
        if (e.target === e.currentTarget) {
            modal.remove();
        }
    });
    
    // View detail button
    modal.querySelector('.view-detail-btn').addEventListener('click', () => {
        window.location.href = `/Product/Detail/${productId}`;
    });
}

// Utility functions
function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.textContent = message;
    
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

function updateCartCount() {
    // This would typically update the cart count in the header
    const cartBadge = document.querySelector('.header-cart-badge');
    if (cartBadge) {
        const currentCount = parseInt(cartBadge.textContent) || 0;
        cartBadge.textContent = currentCount + 1;
    }
}

function trackEvent(eventName, parameters = {}) {
    // Analytics tracking
    console.log(`Event: ${eventName}`, parameters);
    
    if (typeof gtag !== 'undefined') {
        gtag('event', eventName, parameters);
    }
}

function showNoResultsMessage() {
    const productsGrid = document.querySelector('.products-grid');
    const existingMessage = productsGrid.querySelector('.no-results');
    
    if (!existingMessage) {
        const noResults = document.createElement('div');
        noResults.className = 'no-results';
        noResults.innerHTML = `
            <div style="text-align: center; padding: 60px 20px; color: #666;">
                <i class="fas fa-search" style="font-size: 4rem; color: #ddd; margin-bottom: 20px;"></i>
                <h3>Không tìm thấy sản phẩm</h3>
                <p>Không có sản phẩm nào phù hợp với bộ lọc của bạn.</p>
                <button class="clear-filters-btn" style="margin-top: 20px;">Xóa bộ lọc</button>
            </div>
        `;
        
        productsGrid.appendChild(noResults);
        
        // Add event listener to clear filters button
        noResults.querySelector('.clear-filters-btn').addEventListener('click', clearAllFilters);
    }
}

function hideNoResultsMessage() {
    const noResults = document.querySelector('.no-results');
    if (noResults) {
        noResults.remove();
    }
}

// Lazy loading for images
function initializeLazyLoading() {
    const productImages = document.querySelectorAll('.product-image img');
    
    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.classList.remove('lazy');
                observer.unobserve(img);
            }
        });
    });
    
    productImages.forEach(img => {
        if (img.dataset.src) {
            img.classList.add('lazy');
            imageObserver.observe(img);
        }
    });
}

// Analytics
function initializeAnalytics() {
    // Track page view
    trackEvent('page_view', {
        page_title: document.title,
        page_location: window.location.href
    });
    
    // Track filter usage
    document.addEventListener('change', function(e) {
        if (e.target.matches('.filter-checkbox, .filter-radio')) {
            trackEvent('filter_applied', {
                filter_type: e.target.name,
                filter_value: e.target.value
            });
        }
    });
}

// Smooth scrolling
function addSmoothScrolling() {
    const links = document.querySelectorAll('a[href^="#"]');
    
    links.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            
            const targetId = this.getAttribute('href');
            const targetElement = document.querySelector(targetId);
            
            if (targetElement) {
                targetElement.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
}

// Loading animations
function addLoadingAnimations() {
    const productCards = document.querySelectorAll('.product-card');
    
    productCards.forEach((card, index) => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(30px)';
        
        setTimeout(() => {
            card.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, index * 50);
    });
}

// Add CSS for animations and modals
function addCSSAnimations() {
    const style = document.createElement('style');
    style.textContent = `
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        @keyframes slideIn {
            from { transform: translateX(-100%); }
            to { transform: translateX(0); }
        }
        
        .quick-view-modal {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 1000;
        }
        
        .modal-overlay {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: rgba(0, 0, 0, 0.5);
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }
        
        .modal-content {
            background: white;
            border-radius: 15px;
            max-width: 600px;
            width: 100%;
            max-height: 80vh;
            overflow-y: auto;
            position: relative;
        }
        
        .modal-close {
            position: absolute;
            top: 15px;
            right: 15px;
            background: none;
            border: none;
            font-size: 24px;
            cursor: pointer;
            color: #666;
        }
        
        .quick-view-content {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 30px;
            padding: 30px;
        }
        
        .quick-view-image img {
            width: 100%;
            height: 300px;
            object-fit: cover;
            border-radius: 10px;
        }
        
        .notification {
            position: fixed;
            top: 20px;
            right: 20px;
            background: #667eea;
            color: white;
            padding: 15px 20px;
            border-radius: 8px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
            transform: translateX(100%);
            transition: transform 0.3s ease;
            z-index: 1001;
        }
        
        .notification.show {
            transform: translateX(0);
        }
        
        .notification-success {
            background: #27ae60;
        }
        
        .notification-error {
            background: #e74c3c;
        }
        
        .products-grid.list-view {
            grid-template-columns: 1fr;
        }
        
        .products-grid.list-view .product-card {
            display: grid;
            grid-template-columns: 200px 1fr auto;
            gap: 20px;
            align-items: center;
        }
        
        .products-grid.list-view .product-image {
            height: 150px;
        }
        
        .products-grid.list-view .product-actions {
            flex-direction: column;
            gap: 10px;
        }
    `;
    document.head.appendChild(style);
} 