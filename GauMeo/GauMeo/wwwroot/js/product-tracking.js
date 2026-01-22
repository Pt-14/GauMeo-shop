// Product Popularity Tracking JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Track clicks on product links
    setupProductTrackingClicks();
});

function setupProductTrackingClicks() {
    // Track clicks on product cards and links
    const productLinks = document.querySelectorAll('a[href*="/Product/Detail/"]');
    
    productLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            // Extract product ID from URL
            const href = this.getAttribute('href');
            const productIdMatch = href.match(/\/Product\/Detail\/(\d+)/);
            
            if (productIdMatch) {
                const productId = productIdMatch[1];
                trackProductView(productId);
            }
        });
    });
    
    // Also track clicks on product images and titles within product cards
    const productCards = document.querySelectorAll('.product-card');
    
    productCards.forEach(card => {
        const links = card.querySelectorAll('a[href*="/Product/Detail/"]');
        const images = card.querySelectorAll('img');
        const titles = card.querySelectorAll('h3');
        
        // Track clicks on any clickable element within product card
        [...links, ...images, ...titles].forEach(element => {
            element.addEventListener('click', function(e) {
                // Find the product link in this card
                const productLink = card.querySelector('a[href*="/Product/Detail/"]');
                if (productLink) {
                    const href = productLink.getAttribute('href');
                    const productIdMatch = href.match(/\/Product\/Detail\/(\d+)/);
                    
                    if (productIdMatch) {
                        const productId = productIdMatch[1];
                        trackProductView(productId);
                    }
                }
            });
        });
    });
}

function trackProductView(productId) {
    // Send async request to track the view
    fetch(`/api/product/track-view/${productId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        }
    })
    .catch(error => {
        // Silently fail - don't interrupt user experience
        console.debug('Product tracking failed:', error);
    });
}

// Export for use in other scripts if needed
window.trackProductView = trackProductView;
