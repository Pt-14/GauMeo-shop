// Service page functionality
document.addEventListener('DOMContentLoaded', function() {
    // Service switching functionality
    function showService(serviceId) {
        // Hide all service details
        document.querySelectorAll('.service-detail').forEach(detail => {
            detail.classList.remove('active');
        });
        
        // Show selected service
        const selectedService = document.getElementById('service-' + serviceId);
        if (selectedService) {
            selectedService.classList.add('active');
        }
        
        // Update active tab
        document.querySelectorAll('.service-tab').forEach(tab => {
            tab.classList.remove('active');
        });
        
        // Find and activate corresponding tab
        const activeTab = document.querySelector(`.service-tab[data-service-id="${serviceId}"]`);
        if (activeTab) {
            activeTab.classList.add('active');
        }
        
        // Update URL without page reload
        const url = new URL(window.location);
        url.searchParams.set('service', serviceId);
        window.history.pushState({}, '', url);
    }
    
    // Initialize service based on URL parameter or default to first service
    function initializeService() {
        const urlParams = new URLSearchParams(window.location.search);
        const serviceId = urlParams.get('service');
        
        if (serviceId) {
            showService(parseInt(serviceId));
        } else {
            // Default to first service (Pet's Spa)
            showService(1);
        }
    }
    
    // Add click handlers to service tabs
    document.querySelectorAll('.service-tab').forEach(tab => {
        tab.addEventListener('click', function() {
            const serviceId = this.getAttribute('data-service-id');
            showService(parseInt(serviceId));
        });
    });
    
    // Handle browser back/forward buttons
    window.addEventListener('popstate', function() {
        initializeService();
    });
    
    // Initialize on page load
    initializeService();
    
    // Smooth scrolling for better UX
    function smoothScrollTo(element) {
        element.scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        });
    }
    
    // Add smooth scroll when switching services
    const originalShowService = showService;
    showService = function(serviceId) {
        originalShowService(serviceId);
        
        // Smooth scroll to top of service content
        const serviceContent = document.querySelector('.service-content');
        if (serviceContent) {
            smoothScrollTo(serviceContent);
        }
    };
    
    // Add loading animation
    function addLoadingAnimation() {
        const serviceDetails = document.querySelectorAll('.service-detail');
        serviceDetails.forEach(detail => {
            detail.style.opacity = '0';
            detail.style.transform = 'translateY(20px)';
            detail.style.transition = 'opacity 0.3s ease, transform 0.3s ease';
        });
    }
    
    function showServiceWithAnimation(serviceId) {
        const selectedService = document.getElementById('service-' + serviceId);
        if (selectedService) {
            selectedService.style.opacity = '1';
            selectedService.style.transform = 'translateY(0)';
        }
    }
    
    // Enhanced service switching with animation
    const enhancedShowService = function(serviceId) {
        // Hide all services with animation
        document.querySelectorAll('.service-detail').forEach(detail => {
            detail.style.opacity = '0';
            detail.style.transform = 'translateY(20px)';
        });
        
        // Show selected service with animation
        setTimeout(() => {
            showService(serviceId);
            showServiceWithAnimation(serviceId);
        }, 150);
    };
    
    // Replace the original showService with enhanced version
    window.showService = enhancedShowService;
    
    // Add keyboard navigation
    document.addEventListener('keydown', function(e) {
        const activeTab = document.querySelector('.service-tab.active');
        if (!activeTab) return;
        
        const currentServiceId = parseInt(activeTab.getAttribute('data-service-id'));
        let nextServiceId = currentServiceId;
        
        switch(e.key) {
            case 'ArrowLeft':
                e.preventDefault();
                nextServiceId = currentServiceId > 1 ? currentServiceId - 1 : 4;
                break;
            case 'ArrowRight':
                e.preventDefault();
                nextServiceId = currentServiceId < 4 ? currentServiceId + 1 : 1;
                break;
            case 'Home':
                e.preventDefault();
                nextServiceId = 1;
                break;
            case 'End':
                e.preventDefault();
                nextServiceId = 4;
                break;
        }
        
        if (nextServiceId !== currentServiceId) {
            showService(nextServiceId);
        }
    });
    
    // Add focus management for accessibility
    document.querySelectorAll('.service-tab').forEach(tab => {
        tab.addEventListener('focus', function() {
            this.setAttribute('aria-selected', 'true');
        });
        
        tab.addEventListener('blur', function() {
            this.setAttribute('aria-selected', 'false');
        });
    });
    
    // Add service content lazy loading (for performance)
    function lazyLoadServiceContent() {
        const serviceDetails = document.querySelectorAll('.service-detail');
        serviceDetails.forEach(detail => {
            if (!detail.classList.contains('active')) {
                detail.style.display = 'none';
            }
        });
    }
    
    // Initialize lazy loading
    setTimeout(lazyLoadServiceContent, 1000);
});
