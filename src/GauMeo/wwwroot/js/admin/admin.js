// Admin Dashboard JavaScript
document.addEventListener('DOMContentLoaded', function() {
    initializeSidebar();
    initializeDropdowns();
    setActiveMenu();
});

// Sidebar functionality
function initializeSidebar() {
    let sidebar = document.querySelector(".sidebar");
    let closeBtn = document.querySelector("#btn");

    if (closeBtn) {
        closeBtn.addEventListener("click", () => {
            sidebar.classList.toggle("open");
            menuBtnChange(); // Calling the function
            
            // Save sidebar state to localStorage
            const isOpen = sidebar.classList.contains("open");
            localStorage.setItem('sidebarOpen', isOpen);
        });
    }

    // Restore sidebar state from localStorage
    const sidebarOpen = localStorage.getItem('sidebarOpen');
    if (sidebarOpen === 'true') {
        sidebar.classList.add("open");
    }

    // Auto open sidebar on hover
    sidebar.addEventListener('mouseenter', () => {
        if (!sidebar.classList.contains('open')) {
            sidebar.classList.add('hover-open');
        }
    });

    sidebar.addEventListener('mouseleave', () => {
        sidebar.classList.remove('hover-open');
    });
}

// Function to change sidebar button icon
function menuBtnChange() {
    let sidebar = document.querySelector(".sidebar");
    let closeBtn = document.querySelector("#btn");
    
    if (sidebar.classList.contains("open")) {
        closeBtn.classList.replace("bx-menu", "bx-menu-alt-right");
    } else {
        closeBtn.classList.replace("bx-menu-alt-right", "bx-menu");
    }
}

// Dropdown functionality
function initializeDropdowns() {
    const dropdownItems = document.querySelectorAll('.dropdown-menu-item');
    
    dropdownItems.forEach(item => {
        const dropdownToggle = item.querySelector('.dropdown-toggle');
        
        if (dropdownToggle) {
            dropdownToggle.addEventListener('click', function(e) {
                // Allow navigation to the main category
                const href = this.getAttribute('href');
                if (href && href !== '#') {
                    // If sidebar is closed and it's a real link, navigate
                    const sidebar = document.querySelector('.sidebar');
                    if (!sidebar.classList.contains('open')) {
                        window.location.href = href;
                        return;
                    }
                }
                
                e.preventDefault();
                
                // Close all other dropdowns
                dropdownItems.forEach(otherItem => {
                    if (otherItem !== item) {
                        otherItem.classList.remove('active');
                    }
                });
                
                // Toggle current dropdown
                item.classList.toggle('active');
            });
        }
    });

    // Close dropdowns when clicking outside
    document.addEventListener('click', function(e) {
        if (!e.target.closest('.dropdown-menu-item')) {
            dropdownItems.forEach(item => {
                item.classList.remove('active');
            });
        }
    });

    // Close dropdowns when sidebar closes
    const sidebar = document.querySelector('.sidebar');
    const closeBtn = document.querySelector('#btn');
    
    if (closeBtn) {
        closeBtn.addEventListener('click', () => {
            // Close all dropdowns when sidebar is toggled
            dropdownItems.forEach(item => {
                item.classList.remove('active');
            });
        });
    }
}



// Set active menu based on current URL
function setActiveMenu() {
    const currentPath = window.location.pathname;
    const menuLinks = document.querySelectorAll('.nav-list a');
    
    menuLinks.forEach(link => {
        const href = link.getAttribute('href');
        
        if (href && currentPath.includes(href) && href !== '#') {
            link.classList.add('active');
            
            // If it's in a dropdown, expand the dropdown
            const dropdownParent = link.closest('.dropdown-menu-item');
            if (dropdownParent) {
                dropdownParent.classList.add('active');
            }
            
            // If it's a submenu item, also mark parent as active
            const submenuParent = link.closest('.dropdown-submenu');
            if (submenuParent) {
                const parentDropdown = submenuParent.closest('.dropdown-menu-item');
                if (parentDropdown) {
                    parentDropdown.classList.add('active');
                    const parentLink = parentDropdown.querySelector('.dropdown-toggle');
                    if (parentLink) {
                        parentLink.classList.add('active');
                    }
                }
            }
        }
    });
}

// Smooth scrolling for anchor links
function smoothScrollToSection(targetId) {
    const target = document.getElementById(targetId);
    if (target) {
        target.scrollIntoView({
            behavior: 'smooth',
            block: 'start'
        });
    }
}

// Notification system
function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <i class='bx bx-${type === 'success' ? 'check-circle' : type === 'error' ? 'x-circle' : 'info-circle'}'></i>
        <span>${message}</span>
        <button class="notification-close" onclick="this.parentElement.remove()">
            <i class='bx bx-x'></i>
        </button>
    `;
    
    // Add to top-right corner
    notification.style.position = 'fixed';
    notification.style.top = '20px';
    notification.style.right = '20px';
    notification.style.zIndex = '10000';
    notification.style.background = 'white';
    notification.style.padding = '15px 20px';
    notification.style.borderRadius = '8px';
    notification.style.boxShadow = '0 4px 12px rgba(0,0,0,0.15)';
    notification.style.display = 'flex';
    notification.style.alignItems = 'center';
    notification.style.gap = '12px';
    notification.style.maxWidth = '400px';
    notification.style.transform = 'translateX(450px)';
    notification.style.transition = 'transform 0.3s ease';
    
    document.body.appendChild(notification);
    
    // Animate in
    setTimeout(() => {
        notification.style.transform = 'translateX(0)';
    }, 100);
    
    // Auto remove after 5 seconds
    setTimeout(() => {
        if (notification.parentElement) {
            notification.style.transform = 'translateX(450px)';
            setTimeout(() => {
                notification.remove();
            }, 300);
        }
    }, 5000);
}

// Responsive handling
function handleResponsive() {
    const sidebar = document.querySelector('.sidebar');
    const homeSection = document.querySelector('.home-section');
    
    function checkScreenSize() {
        if (window.innerWidth <= 768) {
            sidebar.classList.remove('open');
            menuBtnChange();
        }
    }
    
    window.addEventListener('resize', checkScreenSize);
    checkScreenSize(); // Check on initial load
}

// Initialize responsive handling
handleResponsive();

// Utility functions
const AdminUtils = {
    // Format currency for Vietnamese
    formatCurrency: function(amount) {
        return new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND'
        }).format(amount);
    },
    
    // Format date for Vietnamese
    formatDate: function(date) {
        return new Intl.DateTimeFormat('vi-VN').format(new Date(date));
    },
    
    // Confirm dialog
    confirm: function(message, callback) {
        if (confirm(message)) {
            callback();
        }
    },
    
    // Show loading overlay
    showLoading: function() {
        const loading = document.createElement('div');
        loading.className = 'loading-overlay';
        loading.innerHTML = `
            <div class="loading-spinner"></div>
        `;
        loading.style.position = 'fixed';
        loading.style.top = '0';
        loading.style.left = '0';
        loading.style.width = '100%';
        loading.style.height = '100%';
        loading.style.background = 'rgba(0,0,0,0.5)';
        loading.style.display = 'flex';
        loading.style.alignItems = 'center';
        loading.style.justifyContent = 'center';
        loading.style.zIndex = '10001';
        
        const spinner = loading.querySelector('.loading-spinner');
        spinner.style.width = '50px';
        spinner.style.height = '50px';
        spinner.style.border = '5px solid rgba(255,255,255,0.3)';
        spinner.style.borderTop = '5px solid white';
        spinner.style.borderRadius = '50%';
        spinner.style.animation = 'spin 1s linear infinite';
        
        document.body.appendChild(loading);
    },
    
    // Hide loading overlay
    hideLoading: function() {
        const loading = document.querySelector('.loading-overlay');
        if (loading) {
            loading.remove();
        }
    },
    
    // Show notification
    showNotification: showNotification
};

// Add CSS for loading spinner animation
const style = document.createElement('style');
style.textContent = `
    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }
`;
document.head.appendChild(style);

// Export for use in other scripts
window.AdminUtils = AdminUtils;

// Handle logout confirmation
document.addEventListener('DOMContentLoaded', function() {
    const logoutBtn = document.querySelector('#log_out');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', function(e) {
            if (!confirm('Bạn có chắc chắn muốn đăng xuất không?')) {
                e.preventDefault();
            }
        });
    }
});

// Handle form submissions with loading
document.addEventListener('submit', function(e) {
    const form = e.target;
    if (form.classList.contains('admin-form')) {
        AdminUtils.showLoading();
        
        // Hide loading after 10 seconds as fallback
        setTimeout(() => {
            AdminUtils.hideLoading();
        }, 10000);
    }
});

// Auto-hide alerts
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => {
        setTimeout(() => {
            if (alert.parentElement) {
                alert.style.opacity = '0';
                alert.style.transform = 'translateY(-20px)';
                setTimeout(() => {
                    alert.remove();
                }, 300);
            }
        }, 5000);
    });
}); 