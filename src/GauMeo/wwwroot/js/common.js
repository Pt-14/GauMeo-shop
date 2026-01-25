/**
 * Common JavaScript - Loaded on all pages
 * Contains: Dropdown navigation, Top bar slider, Product tracking, Newsletter
 */

(function() {
    'use strict';

    // Wait for DOM to be ready
    document.addEventListener('DOMContentLoaded', function() {
        initializeDropdownNavigation();
        initializeTopBarSlider();
        initializeProductTracking();
        initializeNewsletter();
        initializeFAQ();
    });

    // ============================================
    // 1. DROPDOWN NAVIGATION (from main.js)
    // ============================================
    function initializeDropdownNavigation() {
        const dropdowns = document.querySelectorAll('.nav-dropdown');
        
        dropdowns.forEach(dropdown => {
            const link = dropdown.querySelector('a');
            const menu = dropdown.querySelector('.dropdown-menu');
            
            if (link && menu) {
                // Toggle dropdown on click (only if href is '#')
                link.addEventListener('click', (e) => {
                    if (link.getAttribute('href') === '#') {
                        e.preventDefault();
                        menu.classList.toggle('show');
                    }
                    // If it's a real link (e.g., /Brand/Index), allow navigation
                });
                
                // Close menu when clicking outside
                document.addEventListener('click', (e) => {
                    if (!dropdown.contains(e.target)) {
                        menu.classList.remove('show');
                    }
                });
            }
        });
    }

    // ============================================
    // 2. TOP BAR SLIDER (from slider.js)
    // ============================================
    function initializeTopBarSlider() {
        const sliderItems = document.querySelectorAll('.slider-item');
        if (sliderItems.length === 0) return;
        
        let currentIndex = 0;
        let autoSlideInterval;
        
        function showSlide(index) {
            // Hide current slide
            sliderItems[currentIndex].classList.remove('active');
            
            // Update index with wrap-around
            currentIndex = index;
            if (currentIndex >= sliderItems.length) currentIndex = 0;
            if (currentIndex < 0) currentIndex = sliderItems.length - 1;
            
            // Show new slide
            sliderItems[currentIndex].classList.add('active');
        }
        
        function nextSlide() {
            showSlide(currentIndex + 1);
        }
        
        function startAutoSlide() {
            autoSlideInterval = setInterval(nextSlide, 7000); // 7 seconds
        }
        
        function stopAutoSlide() {
            clearInterval(autoSlideInterval);
        }
        
        // Global function for navigation buttons (used in HTML onclick)
        window.changeSlide = function(direction) {
            stopAutoSlide();
            showSlide(currentIndex + direction);
            startAutoSlide(); // Restart auto slide after click
        };
        
        // Start auto slide
        startAutoSlide();
        
        // Pause on hover
        const slider = document.querySelector('.top-bar-slider');
        if (slider) {
            slider.addEventListener('mouseenter', stopAutoSlide);
            slider.addEventListener('mouseleave', startAutoSlide);
        }
    }

    // ============================================
    // 3. PRODUCT TRACKING (from product-tracking.js)
    // ============================================
    function initializeProductTracking() {
        setupProductTrackingClicks();
    }

    function setupProductTrackingClicks() {
        // Track clicks on product links
        const productLinks = document.querySelectorAll('a[href*="/Product/Detail/"]');
        
        productLinks.forEach(link => {
            link.addEventListener('click', function(e) {
                const href = this.getAttribute('href');
                const productIdMatch = href.match(/\/Product\/Detail\/(\d+)/);
                
                if (productIdMatch) {
                    trackProductView(productIdMatch[1]);
                }
            });
        });
        
        // Track clicks on product images and titles within product cards
        const productCards = document.querySelectorAll('.product-card');
        
        productCards.forEach(card => {
            const links = card.querySelectorAll('a[href*="/Product/Detail/"]');
            const images = card.querySelectorAll('img');
            const titles = card.querySelectorAll('h3');
            
            // Track clicks on any clickable element within product card
            [...links, ...images, ...titles].forEach(element => {
                element.addEventListener('click', function(e) {
                    const productLink = card.querySelector('a[href*="/Product/Detail/"]');
                    if (productLink) {
                        const href = productLink.getAttribute('href');
                        const productIdMatch = href.match(/\/Product\/Detail\/(\d+)/);
                        
                        if (productIdMatch) {
                            trackProductView(productIdMatch[1]);
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

    // ============================================
    // 4. NEWSLETTER SUBSCRIPTION (from newsletter.js)
    // ============================================
    function initializeNewsletter() {
        const form = document.getElementById('newsletterForm');
        const submitButton = document.getElementById('newsletterSubmit');
        const messageDiv = document.getElementById('newsletterMessage');

        if (!form || !submitButton || !messageDiv) return;

        form.addEventListener('submit', async function(e) {
            e.preventDefault();
            
            // Disable submit button
            submitButton.disabled = true;
            submitButton.textContent = 'Đang xử lý...';
            
            const email = document.getElementById('newsletterEmail').value;
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

            try {
                const response = await fetch('/Newsletter/Subscribe', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                        'RequestVerificationToken': token
                    },
                    body: `email=${encodeURIComponent(email)}`
                });

                const result = await response.json();
                
                messageDiv.textContent = result.message;
                messageDiv.style.display = 'block';
                messageDiv.className = 'newsletter-message ' + (result.success ? 'success' : 'error');

                if (result.success) {
                    form.reset();
                }

                // Hide message after 5 seconds
                setTimeout(() => {
                    messageDiv.style.display = 'none';
                }, 5000);
            } catch (error) {
                messageDiv.textContent = 'Đã có lỗi xảy ra. Vui lòng thử lại sau.';
                messageDiv.style.display = 'block';
                messageDiv.className = 'newsletter-message error';
            } finally {
                // Re-enable submit button
                submitButton.disabled = false;
                submitButton.textContent = 'Gửi';
            }
        });
    }

    // ============================================
    // 5. FAQ FUNCTIONALITY (from faq.js)
    // Only initialize if we're on FAQ page
    // ============================================
    function initializeFAQ() {
        // Only initialize if we're on FAQ page
        if (!document.querySelector('.faq-item, .faq-question, #faqSearch')) return;

        // Note: FAQ toggle buttons use onclick="toggleFAQ(this)" in HTML, no need for event listeners here

        // Search functionality
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

        // Note: Category filter buttons use onclick handlers in HTML, no need for event listeners here
        // Chat button (if exists)
        const chatButton = document.querySelector('.chat-btn, [onclick*="openChat"]');
        if (chatButton) {
            chatButton.addEventListener('click', function(e) {
                e.preventDefault();
                openChat();
            });
        }
    }

    function toggleFAQ(button) {
        const answer = button.nextElementSibling;
        const isActive = button.classList.contains('active');
        
        // Close all other FAQs
        document.querySelectorAll('.faq-question.active').forEach(q => {
            if (q !== button) {
                q.classList.remove('active');
                const otherAnswer = q.nextElementSibling;
                if (otherAnswer && otherAnswer.classList.contains('faq-answer')) {
                    otherAnswer.classList.remove('active');
                }
            }
        });
        
        // Toggle current FAQ
        if (!isActive) {
            button.classList.add('active');
            if (answer && answer.classList.contains('faq-answer')) {
                answer.classList.add('active');
            }
        } else {
            button.classList.remove('active');
            if (answer && answer.classList.contains('faq-answer')) {
                answer.classList.remove('active');
            }
        }
    }

    function filterFAQ(category) {
        // Update active button based on category
        document.querySelectorAll('.category-btn').forEach(btn => {
            btn.classList.remove('active');
            // Check if this button matches the category
            const btnCategory = btn.getAttribute('data-category') || btn.textContent.trim().toLowerCase();
            if (btnCategory === category || (category === 'all' && btn.textContent.trim().toLowerCase() === 'tất cả')) {
                btn.classList.add('active');
            }
        });
        
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
            const answer = q.nextElementSibling;
            if (answer && answer.classList.contains('faq-answer')) {
                answer.classList.remove('active');
            }
        });
    }

    function searchFAQ() {
        const searchInput = document.getElementById('faqSearch');
        if (!searchInput) return;

        const searchTerm = searchInput.value.toLowerCase();
        const faqItems = document.querySelectorAll('.faq-item');
        
        faqItems.forEach(item => {
            const questionEl = item.querySelector('.faq-question span, .faq-question');
            const answerEl = item.querySelector('.faq-answer');
            
            const question = questionEl ? questionEl.textContent.toLowerCase() : '';
            const answer = answerEl ? answerEl.textContent.toLowerCase() : '';
            
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

    // Export FAQ functions for use in HTML onclick handlers if needed
    window.toggleFAQ = toggleFAQ;
    window.filterFAQ = filterFAQ;
    window.searchFAQ = searchFAQ;
    window.openChat = openChat;
})();
