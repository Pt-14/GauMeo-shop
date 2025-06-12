/**
 * Brand Page JavaScript
 * Handles tab switching and dark/light mode integration
 */

document.addEventListener('DOMContentLoaded', function() {
    // Initialize brand page functionality
    initializeBrandPage();
    
    // Listen for theme changes
    observeThemeChanges();
});

/**
 * Initialize brand page functionality
 */
function initializeBrandPage() {
    // Set default active brand (Royal Canin)
    const defaultBrandId = 1;
    showBrand(defaultBrandId);
    
    // Add click event listeners to brand tabs
    addBrandTabListeners();
    
    // Add smooth transitions
    addSmoothTransitions();
}

/**
 * Add click event listeners to brand tabs
 */
function addBrandTabListeners() {
    const brandTabs = document.querySelectorAll('.brand-tab');
    
    brandTabs.forEach(tab => {
        tab.addEventListener('click', function() {
            // Extract brand ID from the tab's data attribute
            const brandId = this.getAttribute('data-brand-id');
            
            if (brandId) {
                showBrand(parseInt(brandId));
            }
        });
    });
}

/**
 * Show brand by ID
 */
function showBrand(brandId) {
    // Hide all brand details
    const brandDetails = document.querySelectorAll('.brand-detail');
    brandDetails.forEach(detail => {
        detail.classList.remove('active');
        // Add fade out effect
        detail.style.opacity = '0';
        detail.style.transform = 'translateY(10px)';
    });
    
    // Show the selected brand
    const selectedBrand = document.getElementById('brand-' + brandId);
    if (selectedBrand) {
        selectedBrand.classList.add('active');
        // Add fade in effect
        setTimeout(() => {
            selectedBrand.style.opacity = '1';
            selectedBrand.style.transform = 'translateY(0)';
        }, 50);
    }
    
    // Update active tab
    updateActiveTab(brandId);
    
    // Update URL hash for bookmarking
    updateURLHash(brandId);
    
    // Trigger theme-aware animations
    triggerThemeAwareAnimations();
}

/**
 * Update active tab styling
 */
function updateActiveTab(brandId) {
    // Remove active class from all tabs
    const allTabs = document.querySelectorAll('.brand-tab');
    allTabs.forEach(tab => {
        tab.classList.remove('active');
    });
    
    // Add active class to the selected tab
    const activeTab = document.querySelector(`[data-brand-id="${brandId}"]`);
    
    if (activeTab) {
        activeTab.classList.add('active');
        
        // Add smooth transition effect
        activeTab.style.transform = 'scale(1.02)';
        setTimeout(() => {
            activeTab.style.transform = 'scale(1)';
        }, 200);
        
        // Scroll to active tab if needed
        scrollToActiveTab(activeTab);
    }
}

/**
 * Scroll to active tab in sidebar
 */
function scrollToActiveTab(activeTab) {
    const sidebar = document.querySelector('.brand-sidebar');
    if (sidebar) {
        const tabTop = activeTab.offsetTop;
        const sidebarTop = sidebar.scrollTop;
        const sidebarHeight = sidebar.clientHeight;
        
        if (tabTop < sidebarTop || tabTop > sidebarTop + sidebarHeight) {
            sidebar.scrollTo({
                top: tabTop - sidebarHeight / 2,
                behavior: 'smooth'
            });
        }
    }
}

/**
 * Update URL hash for bookmarking
 */
function updateURLHash(brandId) {
    const brandNames = {
        1: 'royal-canin',
        2: 'pedigree',
        3: 'purina',
        4: 'whiskas',
        5: 'hills',
        6: 'nutri-source',
        7: 'acana',
        8: 'orijen',
        9: 'natural-core',
        10: 'monge',
        11: 'zenith',
        12: 'k9-natural'
    };
    
    const brandName = brandNames[brandId] || 'royal-canin';
    window.location.hash = `#${brandName}`;
}

/**
 * Add smooth transitions to elements
 */
function addSmoothTransitions() {
    const brandDetails = document.querySelectorAll('.brand-detail');
    brandDetails.forEach(detail => {
        detail.style.transition = 'opacity 0.3s ease, transform 0.3s ease';
        detail.style.opacity = '0';
        detail.style.transform = 'translateY(10px)';
    });
    
    const brandTabs = document.querySelectorAll('.brand-tab');
    brandTabs.forEach(tab => {
        tab.style.transition = 'all 0.3s ease, transform 0.2s ease';
    });
}

/**
 * Observe theme changes and update brand page accordingly
 */
function observeThemeChanges() {
    // Create a mutation observer to watch for theme changes
    const observer = new MutationObserver(function(mutations) {
        mutations.forEach(function(mutation) {
            if (mutation.type === 'attributes' && mutation.attributeName === 'class') {
                if (mutation.target.classList.contains('dark')) {
                    handleDarkModeActivation();
                } else {
                    handleLightModeActivation();
                }
            }
        });
    });
    
    // Start observing the body element for class changes
    observer.observe(document.body, {
        attributes: true,
        attributeFilter: ['class']
    });
    
    // Initial theme check
    if (document.body.classList.contains('dark')) {
        handleDarkModeActivation();
    } else {
        handleLightModeActivation();
    }
}

/**
 * Handle dark mode activation
 */
function handleDarkModeActivation() {
    // Add dark mode specific animations
    const brandContainer = document.querySelector('.brand-container');
    if (brandContainer) {
        brandContainer.style.transition = 'background-color 0.3s ease';
    }
    
    // Update brand image placeholders for dark mode
    updateBrandImagePlaceholders(true);
    
    // Add dark mode specific effects
    addDarkModeEffects();
}

/**
 * Handle light mode activation
 */
function handleLightModeActivation() {
    // Remove dark mode specific animations
    const brandContainer = document.querySelector('.brand-container');
    if (brandContainer) {
        brandContainer.style.transition = 'background-color 0.3s ease';
    }
    
    // Update brand image placeholders for light mode
    updateBrandImagePlaceholders(false);
    
    // Remove dark mode specific effects
    removeDarkModeEffects();
}

/**
 * Update brand image placeholders based on theme
 */
function updateBrandImagePlaceholders(isDarkMode) {
    const placeholders = document.querySelectorAll('.brand-image-placeholder');
    
    placeholders.forEach(placeholder => {
        if (isDarkMode) {
            placeholder.style.background = 'linear-gradient(135deg, #7ddcff 0%, #5a96ff 100%)';
        } else {
            placeholder.style.background = 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)';
        }
    });
}

/**
 * Add dark mode specific effects
 */
function addDarkModeEffects() {
    // Add subtle glow effect to active tabs in dark mode
    const activeTab = document.querySelector('.brand-tab.active');
    if (activeTab) {
        activeTab.style.boxShadow = '0 0 10px rgba(125, 220, 255, 0.3)';
    }
    
    // Add hover effects for dark mode
    const brandTabs = document.querySelectorAll('.brand-tab');
    brandTabs.forEach(tab => {
        tab.addEventListener('mouseenter', function() {
            if (!this.classList.contains('active')) {
                this.style.boxShadow = '0 0 8px rgba(125, 220, 255, 0.2)';
            }
        });
        
        tab.addEventListener('mouseleave', function() {
            if (!this.classList.contains('active')) {
                this.style.boxShadow = 'none';
            }
        });
    });
}

/**
 * Remove dark mode specific effects
 */
function removeDarkModeEffects() {
    // Remove glow effects
    const brandTabs = document.querySelectorAll('.brand-tab');
    brandTabs.forEach(tab => {
        tab.style.boxShadow = 'none';
    });
}

/**
 * Trigger theme-aware animations
 */
function triggerThemeAwareAnimations() {
    const isDarkMode = document.body.classList.contains('dark');
    
    // Add theme-specific animation to the active brand
    const activeBrand = document.querySelector('.brand-detail.active');
    if (activeBrand) {
        if (isDarkMode) {
            activeBrand.style.animation = 'brandSlideInDark 0.5s ease';
        } else {
            activeBrand.style.animation = 'brandSlideInLight 0.5s ease';
        }
        
        // Remove animation after it completes
        setTimeout(() => {
            activeBrand.style.animation = '';
        }, 500);
    }
}

/**
 * Handle URL hash changes for direct navigation
 */
window.addEventListener('hashchange', function() {
    const hash = window.location.hash.substring(1);
    const brandMap = {
        'royal-canin': 1,
        'pedigree': 2,
        'purina': 3,
        'whiskas': 4,
        'hills': 5,
        'nutri-source': 6,
        'acana': 7,
        'orijen': 8,
        'natural-core': 9,
        'monge': 10,
        'zenith': 11,
        'k9-natural': 12
    };
    
    const brandId = brandMap[hash];
    if (brandId) {
        showBrand(brandId);
    }
});

/**
 * Initialize from URL hash if present
 */
function initializeFromURLHash() {
    const hash = window.location.hash.substring(1);
    const brandMap = {
        'royal-canin': 1,
        'pedigree': 2,
        'purina': 3,
        'whiskas': 4,
        'hills': 5,
        'nutri-source': 6,
        'acana': 7,
        'orijen': 8,
        'natural-core': 9,
        'monge': 10,
        'zenith': 11,
        'k9-natural': 12
    };
    
    const brandId = brandMap[hash];
    if (brandId) {
        showBrand(brandId);
    }
}

// Initialize from URL hash on page load
document.addEventListener('DOMContentLoaded', function() {
    initializeFromURLHash();
});

// Add CSS animations for theme-aware transitions
const style = document.createElement('style');
style.textContent = `
    @keyframes brandSlideInLight {
        from {
            opacity: 0;
            transform: translateY(20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    @keyframes brandSlideInDark {
        from {
            opacity: 0;
            transform: translateY(20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    .brand-detail {
        transition: opacity 0.3s ease, transform 0.3s ease;
    }
    
    .brand-tab {
        transition: all 0.3s ease, transform 0.2s ease;
    }
    
    .brand-tab:hover {
        transform: translateY(-1px);
    }
    
    .brand-tab.active {
        transform: translateY(-2px);
    }
    
    .brand-year, .brand-origin {
        transition: all 0.3s ease;
    }
    
    .feature-item {
        transition: all 0.3s ease;
    }
    
    .feature-item:hover {
        transform: translateY(-1px);
    }
`;
document.head.appendChild(style); 