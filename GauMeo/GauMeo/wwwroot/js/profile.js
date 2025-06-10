// Dark Mode Toggle Functionality
document.addEventListener('DOMContentLoaded', function() {
    // Check if dark mode is saved in localStorage
    const isDarkMode = localStorage.getItem('darkMode') === 'true';
    
    // Apply dark mode if it was previously enabled
    if (isDarkMode) {
        document.body.classList.add('dark-mode');
    }
    
    // Create dark mode toggle button
    function createDarkModeToggle() {
        const toggleBtn = document.createElement('button');
        toggleBtn.innerHTML = '<i class="fas fa-moon"></i>';
        toggleBtn.className = 'dark-mode-toggle';
        toggleBtn.title = 'Toggle Dark Mode';
        toggleBtn.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            width: 50px;
            height: 50px;
            border-radius: 50%;
            border: none;
            background: #00dfc4;
            color: #223243;
            font-size: 18px;
            cursor: pointer;
            z-index: 1000;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
            transition: all 0.3s ease;
        `;
        
        // Update icon based on current mode
        function updateIcon() {
            const isDark = document.body.classList.contains('dark-mode');
            toggleBtn.innerHTML = isDark ? '<i class="fas fa-sun"></i>' : '<i class="fas fa-moon"></i>';
        }
        
        updateIcon();
        
        // Toggle dark mode
        toggleBtn.addEventListener('click', function() {
            document.body.classList.toggle('dark-mode');
            const isDark = document.body.classList.contains('dark-mode');
            localStorage.setItem('darkMode', isDark);
            updateIcon();
        });
        
        return toggleBtn;
    }
    
    // Add toggle button to page
    const toggleButton = createDarkModeToggle();
    document.body.appendChild(toggleButton);
    
    // Add hover effects for toggle button
    toggleButton.addEventListener('mouseenter', function() {
        this.style.transform = 'scale(1.1)';
        this.style.boxShadow = '0 6px 16px rgba(0, 0, 0, 0.4)';
    });
    
    toggleButton.addEventListener('mouseleave', function() {
        this.style.transform = 'scale(1)';
        this.style.boxShadow = '0 4px 12px rgba(0, 0, 0, 0.3)';
    });
});

// Dark mode styles for toggle button
const style = document.createElement('style');
style.textContent = `
    .dark-mode .dark-mode-toggle {
        background: #3a4a5a !important;
        color: #00dfc4 !important;
        box-shadow: 0 4px 12px rgba(0, 223, 196, 0.3) !important;
    }
    
    .dark-mode .dark-mode-toggle:hover {
        background: #4a5a6a !important;
        box-shadow: 0 6px 16px rgba(0, 223, 196, 0.4) !important;
    }
`;
document.head.appendChild(style); 



document.addEventListener('DOMContentLoaded', function() {
    // Get all section titles and sections
    const sectionTitles = document.querySelectorAll('.profile-section-title');
    const sections = {
        profileInfoSection: document.getElementById('profileInfoSection'),
        favoritesSection: document.getElementById('favoritesSection'),
        ordersSection: document.getElementById('ordersSection')
    };

    // Function to switch sections
    function switchSection(sectionId) {
        // Hide all sections
        Object.values(sections).forEach(section => {
            if (section) section.style.display = 'none';
        });

        // Show selected section
        if (sections[sectionId]) {
            sections[sectionId].style.display = 'block';
        }

        // Update active state
        sectionTitles.forEach(title => {
            title.classList.remove('active');
            if (title.dataset.section === sectionId) {
                title.classList.add('active');
            }
        });
    }

    // Add click handlers to section titles
    sectionTitles.forEach(title => {
        title.addEventListener('click', () => {
            switchSection(title.dataset.section);
        });
    });

    // Show profile info section by default
    switchSection('profileInfoSection');

    // Avatar modal handling
    const editAvatarBtn = document.getElementById('editAvatarBtn');
    const avatarModalEl = document.getElementById('avatarModal');
    let avatarModal = null;
    if (editAvatarBtn && avatarModalEl && window.bootstrap) {
        avatarModal = new bootstrap.Modal(avatarModalEl);
        editAvatarBtn.addEventListener('click', function() {
            avatarModal.show();
        });
    }

    // Handle avatar upload
    const avatarForm = document.getElementById('avatarForm');
    if (avatarForm) {
        avatarForm.addEventListener('submit', function(e) {
            e.preventDefault();
            if (!avatarModal) return;
            const formData = new FormData(this);
            fetch('/Account/UploadAvatar', {
                method: 'POST',
                body: formData
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    // Update avatar in profile
                    const currentAvatar = document.getElementById('currentAvatar');
                    if (currentAvatar) {
                        currentAvatar.src = data.avatarUrl;
                    }
                    // Update avatar in header
                    const headerAvatar = document.querySelector('.user-avatar');
                    if (headerAvatar) {
                        headerAvatar.src = data.avatarUrl;
                    }
                    // Close modal
                    avatarModal.hide();
                } else {
                    alert('Có lỗi xảy ra khi cập nhật ảnh đại diện');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Có lỗi xảy ra khi cập nhật ảnh đại diện');
            });
        });
    }
});
