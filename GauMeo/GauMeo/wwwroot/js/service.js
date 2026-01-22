/**
 * Modern Service Page JavaScript
 * Handles service tabs, pricing tabs, FAQ accordion, and animations
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
    // Initialize tab functionality
    initializeServiceTabs();
    
    // Initialize pricing tabs
    initializePricingTabs();
    
    // Initialize FAQ accordion
    initializeFAQAccordion();
    
    // Initialize image slider
    initializeImageSlider();
    
    // Check URL hash on page load
    initializeFromURLHash();
    
    // Add scroll animations
    initializeScrollAnimations();
}

/**
 * Initialize service tabs functionality
 */
function initializeServiceTabs() {
    const tabButtons = document.querySelectorAll('.service-tab-btn');
    
    tabButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Remove focus to prevent border
            this.blur();
            
            const serviceId = this.getAttribute('data-service-id');
            if (serviceId) {
                showService(parseInt(serviceId));
            }
        });
    });
}

/**
 * Initialize pricing tabs functionality
 */
function initializePricingTabs() {
    const pricingTabButtons = document.querySelectorAll('.pricing-tab-btn');
    
    pricingTabButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Remove focus to prevent border
            this.blur();
            
            const petType = this.getAttribute('data-pet');
            const serviceId = this.getAttribute('data-service');
            
            if (petType && serviceId) {
                showPricingContent(petType, serviceId);
                updateActivePricingTab(this);
            }
        });
    });
}

/**
 * Show pricing content for pet type with smooth transition
 */
function showPricingContent(petType, serviceId) {
    // Hide all pricing content for this service with fade out
    const allContent = document.querySelectorAll(`[id*="pricing-${serviceId}"]`);
    allContent.forEach(content => {
        if (content.classList.contains('active')) {
            content.style.opacity = '0';
            content.style.transform = 'translateY(10px)';
            
            setTimeout(() => {
                content.classList.remove('active');
            }, 200);
        }
    });
    
    // Show the selected pricing content with fade in
    const targetContent = document.getElementById(`${petType}-pricing-${serviceId}`);
    if (targetContent) {
        setTimeout(() => {
            targetContent.classList.add('active');
            targetContent.style.opacity = '0';
            targetContent.style.transform = 'translateY(10px)';
            
            // Trigger reflow
            targetContent.offsetHeight;
            
            setTimeout(() => {
                targetContent.style.opacity = '1';
                targetContent.style.transform = 'translateY(0)';
            }, 50);
        }, 200);
    }
}

/**
 * Update active pricing tab
 */
function updateActivePricingTab(clickedButton) {
    // Remove active class from sibling tabs
    const parentTabs = clickedButton.parentElement;
    const siblingTabs = parentTabs.querySelectorAll('.pricing-tab-btn');
    
    siblingTabs.forEach(tab => {
        tab.classList.remove('active');
    });
    
    // Add active class to clicked tab
    clickedButton.classList.add('active');
}

/**
 * Show service by ID with smooth transition - No scrolling
 */
function showService(serviceId) {
    // Update active tab with smooth transition
    updateActiveTab(serviceId);
    
    // Hide all service details with smooth fade out
    const serviceDetails = document.querySelectorAll('.service-detail');
    serviceDetails.forEach(detail => {
        if (detail.classList.contains('active')) {
            detail.style.transition = 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)';
            detail.style.opacity = '0';
            detail.style.transform = 'translateY(15px)';
            
            setTimeout(() => {
                detail.classList.remove('active');
                detail.style.display = 'none';
            }, 300);
        }
    });
    
    // Show the selected service with smooth fade in
    setTimeout(() => {
        const selectedService = document.getElementById('service-' + serviceId);
        if (selectedService) {
            selectedService.style.display = 'block';
            selectedService.classList.add('active');
            selectedService.style.opacity = '0';
            selectedService.style.transform = 'translateY(15px)';
            selectedService.style.transition = 'all 0.4s cubic-bezier(0.4, 0, 0.2, 1)';
            
            // Trigger reflow
            selectedService.offsetHeight;
            
            setTimeout(() => {
                selectedService.style.opacity = '1';
                selectedService.style.transform = 'translateY(0)';
            }, 50);
        }
    }, 300);
    
    // Update URL hash
    updateURLHash(serviceId);
    
    // Re-initialize FAQ and slider for the new service
    setTimeout(() => {
        initializeFAQAccordion();
        initializeImageSlider();
    }, 400);
}

/**
 * Update active tab styling
 */
function updateActiveTab(serviceId) {
    // Remove active class from all tabs
    const allTabs = document.querySelectorAll('.service-tab-btn');
    allTabs.forEach(tab => {
        tab.classList.remove('active');
    });
    
    // Add active class to the selected tab
    const activeTab = document.querySelector(`[data-service-id="${serviceId}"]`);
    if (activeTab) {
        activeTab.classList.add('active');
    }
}



/**
 * Update URL hash for bookmarking - Updated for 5 services
 */
function updateURLHash(serviceId) {
    const serviceNames = {
        1: 'spa-grooming',
        2: 'hotel',
        3: 'swimming', 
        4: 'daycare',
        5: 'training'
    };
    
    const serviceName = serviceNames[serviceId] || 'spa-grooming';
    history.replaceState(null, null, `#${serviceName}`);
}

/**
 * Initialize from URL hash - Updated for 5 services
 */
function initializeFromURLHash() {
    const hash = window.location.hash.substring(1);
    const serviceMap = {
        'spa-grooming': 1,
        'spa': 1, // backward compatibility
        'grooming': 1, // backward compatibility
        'hotel': 2,
        'swimming': 3,
        'daycare': 4,
        'training': 5
    };
    
    const serviceId = serviceMap[hash];
    if (serviceId) {
        setTimeout(() => {
            showService(serviceId);
        }, 100);
    }
}

/**
 * Initialize image slider functionality
 */
function initializeImageSlider() {
    const nextButtons = document.querySelectorAll('.slider-next');
    const prevButtons = document.querySelectorAll('.slider-prev');
    
    nextButtons.forEach(button => {
        button.addEventListener('click', function() {
            this.blur(); // Remove focus
            const sliderContainer = this.closest('.image-slider-container');
            const slideContainer = sliderContainer.querySelector('.image-slide');
            const items = slideContainer.querySelectorAll('.slide-item');
            
            if (items.length > 0) {
                slideContainer.appendChild(items[0]);
            }
        });
    });
    
    prevButtons.forEach(button => {
        button.addEventListener('click', function() {
            this.blur(); // Remove focus
            const sliderContainer = this.closest('.image-slider-container');
            const slideContainer = sliderContainer.querySelector('.image-slide');
            const items = slideContainer.querySelectorAll('.slide-item');
            
            if (items.length > 0) {
                slideContainer.prepend(items[items.length - 1]);
            }
        });
    });
    
    // Initialize slide buttons
    const slideButtons = document.querySelectorAll('.slide-btn');
    slideButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            this.blur(); // Remove focus
            // You can add custom functionality here for each slide button
            console.log('Slide button clicked:', this.parentElement.querySelector('.slide-name').textContent);
        });
    });
}

/**
 * Initialize FAQ accordion functionality - Fixed and smooth
 */
function initializeFAQAccordion() {
    const faqQuestions = document.querySelectorAll('.faq-question');
    
    faqQuestions.forEach(question => {
        question.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            
            // Remove focus to prevent border
            this.blur();
            
            const answer = this.nextElementSibling;
            const isCurrentlyActive = this.classList.contains('active');
            
            // Close all FAQ items first
            faqQuestions.forEach(q => {
                if (q !== this) {
                    q.classList.remove('active');
                    const otherAnswer = q.nextElementSibling;
                    if (otherAnswer && otherAnswer.classList.contains('faq-answer')) {
                        otherAnswer.classList.remove('active');
                    }
                }
            });
            
            // Toggle current item
            if (!isCurrentlyActive) {
                this.classList.add('active');
                if (answer && answer.classList.contains('faq-answer')) {
                    answer.classList.add('active');
                }
            } else {
                this.classList.remove('active');
                if (answer && answer.classList.contains('faq-answer')) {
                    answer.classList.remove('active');
                }
            }
        });
        
        // Add keyboard support for accessibility
        question.addEventListener('keydown', function(e) {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                this.click();
            }
        });
    });
}



/**
 * Initialize scroll animations - Optimized
 */
function initializeScrollAnimations() {
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-in');
                
                // Stagger animations for child elements
                const children = entry.target.querySelectorAll('.feature-card, .additional-item, .booking-step');
                children.forEach((child, index) => {
                    setTimeout(() => {
                        child.classList.add('animate-in');
                    }, index * 80);
                });
            }
        });
    }, observerOptions);
    
    // Observe sections - removed non-existent classes
    const sections = document.querySelectorAll('.service-features, .service-additional, .service-booking-steps, .service-faq');
    sections.forEach(section => observer.observe(section));
}

/**
 * Observe theme changes
 */
function observeThemeChanges() {
    const body = document.body;
    
    // Create a MutationObserver to watch for class changes
    const observer = new MutationObserver((mutations) => {
        mutations.forEach((mutation) => {
            if (mutation.type === 'attributes' && mutation.attributeName === 'class') {
                const isDarkMode = body.classList.contains('dark');
                
                if (isDarkMode) {
                    handleDarkModeActivation();
                } else {
                    handleLightModeActivation();
                }
                
                // Update gradient colors
                updateGradientColors(isDarkMode);
            }
        });
    });
    
    // Start observing
    observer.observe(body, {
        attributes: true,
        attributeFilter: ['class']
    });
}

/**
 * Handle dark mode activation
 */
function handleDarkModeActivation() {
    // Add dark mode specific effects
    addDarkModeEffects();
    
    console.log('Dark mode activated for service page');
}

/**
 * Handle light mode activation
 */
function handleLightModeActivation() {
    // Remove dark mode effects
    removeDarkModeEffects();
    
    console.log('Light mode activated for service page');
}

/**
 * Update gradient colors based on theme
 */
function updateGradientColors(isDarkMode) {
    const serviceHero = document.querySelectorAll('.service-hero');
    const ctaSections = document.querySelectorAll('.service-cta');
    
    if (isDarkMode) {
        serviceHero.forEach(hero => {
            hero.style.background = 'linear-gradient(135deg, var(--service-primary) 0%, var(--service-accent) 100%)';
        });
        ctaSections.forEach(cta => {
            cta.style.background = 'linear-gradient(135deg, var(--service-primary) 0%, var(--service-accent) 100%)';
        });
    } else {
        serviceHero.forEach(hero => {
            hero.style.background = 'var(--service-primary)';
        });
        ctaSections.forEach(cta => {
            cta.style.background = 'var(--service-primary)';
        });
    }
}

/**
 * Add dark mode effects - Cleaned up
 */
function addDarkModeEffects() {
    const cards = document.querySelectorAll('.feature-card, .additional-item, .booking-step');
    
    cards.forEach(card => {
        card.style.boxShadow = '0 4px 15px rgba(0, 170, 40, 0.15)';
        card.style.borderColor = 'var(--service-border)';
    });
}

/**
 * Remove dark mode effects - Cleaned up
 */
function removeDarkModeEffects() {
    const cards = document.querySelectorAll('.feature-card, .additional-item, .booking-step');
    
    cards.forEach(card => {
        card.style.boxShadow = '';
        card.style.borderColor = '';
    });
}

/**
 * Utility function for debouncing
 */
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Simplified CSS injection for essential animations only
const essentialCSS = `
.animate-in {
    animation: slideInUp 0.6s ease forwards;
}

@keyframes slideInUp {
    from {
        opacity: 0;
        transform: translateY(30px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}
`;

// Inject essential CSS
const style = document.createElement('style');
style.textContent = essentialCSS;
document.head.appendChild(style);
