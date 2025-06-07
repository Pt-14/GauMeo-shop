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
