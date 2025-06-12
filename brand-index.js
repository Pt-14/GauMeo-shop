// Brand Index Page JavaScript
document.addEventListener('DOMContentLoaded', function() {
    initializeBrandCards();
});

function initializeBrandCards() {
    const brandCards = document.querySelectorAll('.brand-card');
    
    brandCards.forEach(card => {
        // Add click event for navigation
        card.addEventListener('click', function(e) {
            const brandId = this.getAttribute('data-brand-id');
            
            if (brandId) {
                window.location.href = `/Brand/Detail/${brandId}`;
            }
        });
        
        // Add keyboard navigation
        card.addEventListener('keydown', function(e) {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                this.click();
            }
        });
        
        // Make cards focusable
        card.setAttribute('tabindex', '0');
    });
}

// Search functionality (if needed)
function filterBrands(searchTerm) {
    const brandCards = document.querySelectorAll('.brand-card');
    const searchLower = searchTerm.toLowerCase();
    
    brandCards.forEach(card => {
        const brandName = card.querySelector('.brand-name').textContent.toLowerCase();
        const brandDescription = card.querySelector('.brand-description').textContent.toLowerCase();
        
        if (brandName.includes(searchLower) || brandDescription.includes(searchLower)) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });
} 