/**
 * Service Page JavaScript
 * Handles tab switching and dark/light mode integration
 */

document.addEventListener('DOMContentLoaded', function() {
    // Initialize service page functionality
    initializeServicePage();
    
    // Listen for theme changes
    observeThemeChanges();
});

/**
 * Initialize service page functionality
 */
function initializeServicePage() {
    // Set default active service (Pet's Spa)
    const defaultServiceId = 1;
    showService(defaultServiceId);
    
    // Add click event listeners to service tabs
    addServiceTabListeners();
    
    // Add smooth transitions
    addSmoothTransitions();
}

/**
 * Add click event listeners to service tabs
 */
function addServiceTabListeners() {
    const serviceTabs = document.querySelectorAll('.service-tab');
    
    serviceTabs.forEach(tab => {
        tab.addEventListener('click', function() {
            // Extract service ID from the tab's onclick attribute or data attribute
            const serviceId = this.getAttribute('data-service-id') || 
                             extractServiceIdFromOnclick(this.getAttribute('onclick'));
            
            if (serviceId) {
                showService(parseInt(serviceId));
            }
        });
    });
}

/**
 * Extract service ID from onclick attribute
 */
function extractServiceIdFromOnclick(onclickString) {
    if (!onclickString) return null;
    
    const match = onclickString.match(/showService\((\d+)\)/);
    return match ? match[1] : null;
}

/**
 * Show service by ID
 */
function showService(serviceId) {
    // Hide all service details
    const serviceDetails = document.querySelectorAll('.service-detail');
    serviceDetails.forEach(detail => {
        detail.classList.remove('active');
        // Add fade out effect
        detail.style.opacity = '0';
        detail.style.transform = 'translateY(10px)';
    });
    
    // Show the selected service
    const selectedService = document.getElementById('service-' + serviceId);
    if (selectedService) {
        selectedService.classList.add('active');
        // Add fade in effect
        setTimeout(() => {
            selectedService.style.opacity = '1';
            selectedService.style.transform = 'translateY(0)';
        }, 50);
    }
    
    // Update active tab
    updateActiveTab(serviceId);
    
    // Update URL hash for bookmarking
    updateURLHash(serviceId);
    
    // Trigger theme-aware animations
    triggerThemeAwareAnimations();
}

/**
 * Update active tab styling
 */
function updateActiveTab(serviceId) {
    // Remove active class from all tabs
    const allTabs = document.querySelectorAll('.service-tab');
    allTabs.forEach(tab => {
        tab.classList.remove('active');
    });
    
    // Add active class to the selected tab
    const activeTab = document.querySelector(`[data-service-id="${serviceId}"]`) ||
                     document.querySelector(`[onclick*="showService(${serviceId})"]`);
    
    if (activeTab) {
        activeTab.classList.add('active');
        
        // Add smooth transition effect
        activeTab.style.transform = 'scale(1.02)';
        setTimeout(() => {
            activeTab.style.transform = 'scale(1)';
        }, 200);
    }
}

/**
 * Update URL hash for bookmarking
 */
function updateURLHash(serviceId) {
    const serviceNames = {
        1: 'spa',
        2: 'grooming', 
        3: 'hotel',
        4: 'swimming'
    };
    
    const serviceName = serviceNames[serviceId] || 'spa';
    window.location.hash = `#${serviceName}`;
}

/**
 * Add smooth transitions to elements
 */
function addSmoothTransitions() {
    const serviceDetails = document.querySelectorAll('.service-detail');
    serviceDetails.forEach(detail => {
        detail.style.transition = 'opacity 0.3s ease, transform 0.3s ease';
        detail.style.opacity = '0';
        detail.style.transform = 'translateY(10px)';
    });
    
    const serviceTabs = document.querySelectorAll('.service-tab');
    serviceTabs.forEach(tab => {
        tab.style.transition = 'all 0.3s ease, transform 0.2s ease';
    });
}

/**
 * Observe theme changes and update service page accordingly
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
    const serviceContainer = document.querySelector('.service-container');
    if (serviceContainer) {
        serviceContainer.style.transition = 'background-color 0.3s ease';
    }
    
    // Update service image placeholders for dark mode
    updateServiceImagePlaceholders(true);
    
    // Add dark mode specific effects
    addDarkModeEffects();
}

/**
 * Handle light mode activation
 */
function handleLightModeActivation() {
    // Remove dark mode specific animations
    const serviceContainer = document.querySelector('.service-container');
    if (serviceContainer) {
        serviceContainer.style.transition = 'background-color 0.3s ease';
    }
    
    // Update service image placeholders for light mode
    updateServiceImagePlaceholders(false);
    
    // Remove dark mode specific effects
    removeDarkModeEffects();
}

/**
 * Update service image placeholders based on theme
 */
function updateServiceImagePlaceholders(isDarkMode) {
    const placeholders = document.querySelectorAll('.service-image-placeholder');
    
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
    const activeTab = document.querySelector('.service-tab.active');
    if (activeTab) {
        activeTab.style.boxShadow = '0 0 10px rgba(125, 220, 255, 0.3)';
    }
    
    // Add hover effects for dark mode
    const serviceTabs = document.querySelectorAll('.service-tab');
    serviceTabs.forEach(tab => {
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
    const serviceTabs = document.querySelectorAll('.service-tab');
    serviceTabs.forEach(tab => {
        tab.style.boxShadow = 'none';
    });
}

/**
 * Trigger theme-aware animations
 */
function triggerThemeAwareAnimations() {
    const isDarkMode = document.body.classList.contains('dark');
    
    // Add theme-specific animation to the active service
    const activeService = document.querySelector('.service-detail.active');
    if (activeService) {
        if (isDarkMode) {
            activeService.style.animation = 'serviceSlideInDark 0.5s ease';
        } else {
            activeService.style.animation = 'serviceSlideInLight 0.5s ease';
        }
        
        // Remove animation after it completes
        setTimeout(() => {
            activeService.style.animation = '';
        }, 500);
    }
}

/**
 * Handle URL hash changes for direct navigation
 */
window.addEventListener('hashchange', function() {
    const hash = window.location.hash.substring(1);
    const serviceMap = {
        'spa': 1,
        'grooming': 2,
        'hotel': 3,
        'swimming': 4
    };
    
    const serviceId = serviceMap[hash];
    if (serviceId) {
        showService(serviceId);
    }
});

/**
 * Initialize from URL hash if present
 */
function initializeFromURLHash() {
    const hash = window.location.hash.substring(1);
    const serviceMap = {
        'spa': 1,
        'grooming': 2,
        'hotel': 3,
        'swimming': 4
    };
    
    const serviceId = serviceMap[hash];
    if (serviceId) {
        showService(serviceId);
    }
}

// Initialize from URL hash on page load
document.addEventListener('DOMContentLoaded', function() {
    initializeFromURLHash();
});

// Add CSS animations for theme-aware transitions
const style = document.createElement('style');
style.textContent = `
    @keyframes serviceSlideInLight {
        from {
            opacity: 0;
            transform: translateY(20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    @keyframes serviceSlideInDark {
        from {
            opacity: 0;
            transform: translateY(20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    .service-detail {
        transition: opacity 0.3s ease, transform 0.3s ease;
    }
    
    .service-tab {
        transition: all 0.3s ease, transform 0.2s ease;
    }
    
    .service-tab:hover {
        transform: translateY(-1px);
    }
    
    .service-tab.active {
        transform: translateY(-2px);
    }
`;
document.head.appendChild(style);
