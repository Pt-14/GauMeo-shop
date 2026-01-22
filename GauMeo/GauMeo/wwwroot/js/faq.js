// FAQ Page JavaScript Functions
// Handles FAQ toggle, search, and filtering functionality

function toggleFAQ(button) {
    const answer = button.nextElementSibling;
    const isActive = button.classList.contains('active');
    
    // Close all other FAQs
    document.querySelectorAll('.faq-question.active').forEach(q => {
        q.classList.remove('active');
        q.nextElementSibling.classList.remove('active');
    });
    
    // Toggle current FAQ
    if (!isActive) {
        button.classList.add('active');
        answer.classList.add('active');
    }
}

function filterFAQ(category) {
    // Update active button
    document.querySelectorAll('.category-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    event.target.classList.add('active');
    
    // Show/hide sections
    document.querySelectorAll('.faq-section').forEach(section => {
        if (category === 'all' || section.dataset.category === category) {
            section.classList.remove('hidden');
        } else {
            section.classList.add('hidden');
        }
    });
    
    // Close all open FAQs when switching categories
    document.querySelectorAll('.faq-question.active').forEach(q => {
        q.classList.remove('active');
        q.nextElementSibling.classList.remove('active');
    });
}

function searchFAQ() {
    const searchTerm = document.getElementById('faqSearch').value.toLowerCase();
    const faqItems = document.querySelectorAll('.faq-item');
    
    faqItems.forEach(item => {
        const question = item.querySelector('.faq-question span').textContent.toLowerCase();
        const answer = item.querySelector('.faq-answer').textContent.toLowerCase();
        
        if (question.includes(searchTerm) || answer.includes(searchTerm)) {
            item.style.display = 'block';
        } else {
            item.style.display = searchTerm === '' ? 'block' : 'none';
        }
    });
}

function openChat() {
    alert('Tính năng chat trực tuyến sẽ sớm được ra mắt!');
}

// Initialize FAQ functionality when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    // Search on Enter key
    const faqSearch = document.getElementById('faqSearch');
    if (faqSearch) {
        faqSearch.addEventListener('keypress', function(e) {
            if (e.key === 'Enter') {
                searchFAQ();
            }
        });

        // Real-time search
        faqSearch.addEventListener('input', searchFAQ);
    }
}); 