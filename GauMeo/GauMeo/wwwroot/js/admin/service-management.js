// Service Management JavaScript

document.addEventListener('DOMContentLoaded', function() {
    initializeServiceManagement();
});

function initializeServiceManagement() {
    setupTabNavigation();
    setupSearch();
    setupFilters();
    setupSorting();
    setupCheckboxes();
    setupModalHandlers();
}

// Tab Navigation
function setupTabNavigation() {
    const tabButtons = document.querySelectorAll('.tab-btn');
    const tabPanes = document.querySelectorAll('.tab-pane');

    tabButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            const tabId = this.dataset.tab;
            
            // Remove active class from all tabs and panes
            tabButtons.forEach(b => b.classList.remove('active'));
            tabPanes.forEach(p => p.classList.remove('active'));
            
            // Add active class to clicked tab and corresponding pane
            this.classList.add('active');
            const targetPane = document.getElementById(tabId);
            if (targetPane) {
                targetPane.classList.add('active');
            }
        });
    });
}

// View Toggle Functionality
function setupViewToggle() {
    const viewButtons = document.querySelectorAll('.view-btn');
    const tableView = document.getElementById('tableView');
    const gridView = document.getElementById('gridView');

    viewButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            const view = this.getAttribute('data-view');
            
            // Update active button
            viewButtons.forEach(b => b.classList.remove('active'));
            this.classList.add('active');
            
            // Toggle views
            if (view === 'table') {
                tableView.style.display = 'block';
                gridView.style.display = 'none';
            } else {
                tableView.style.display = 'none';
                gridView.style.display = 'block';
            }
        });
    });
}

// Search Functionality
function setupSearch() {
    const searchInput = document.getElementById('serviceSearch');
    if (!searchInput) return;

    searchInput.addEventListener('input', function() {
        const searchTerm = this.value.toLowerCase();
        filterServices();
    });
}

// Filter Functionality
function setupFilters() {
    const statusFilter = document.getElementById('statusFilter');
    const featuredFilter = document.getElementById('featuredFilter');

    if (statusFilter) {
        statusFilter.addEventListener('change', filterServices);
    }
    
    if (featuredFilter) {
        featuredFilter.addEventListener('change', filterServices);
    }
}

function filterServices() {
    const searchTerm = document.getElementById('serviceSearch')?.value.toLowerCase() || '';
    const statusFilter = document.getElementById('statusFilter')?.value || '';
    const featuredFilter = document.getElementById('featuredFilter')?.value || '';

    // Filter table rows
    const tableRows = document.querySelectorAll('#servicesTable tbody tr');
    tableRows.forEach(row => {
        const name = row.getAttribute('data-name') || '';
        const status = row.getAttribute('data-status') || '';
        const featured = row.getAttribute('data-featured') || '';

        const matchesSearch = name.includes(searchTerm);
        const matchesStatus = !statusFilter || status === statusFilter;
        const matchesFeatured = !featuredFilter || featured === featuredFilter;

        if (matchesSearch && matchesStatus && matchesFeatured) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

// Sorting Functionality
function setupSorting() {
    const sortableHeaders = document.querySelectorAll('.sortable');
    
    sortableHeaders.forEach(header => {
        header.addEventListener('click', function() {
            const sortBy = this.getAttribute('data-sort');
            sortTable(sortBy, this);
        });
    });
}

function sortTable(sortBy, headerElement) {
    const table = document.getElementById('servicesTable');
    if (!table) return;

    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));
    
    // Determine sort direction
    const currentDirection = headerElement.getAttribute('data-direction') || 'asc';
    const newDirection = currentDirection === 'asc' ? 'desc' : 'asc';
    
    // Clear all sort indicators
    document.querySelectorAll('.sortable').forEach(th => {
        th.removeAttribute('data-direction');
        const icon = th.querySelector('.sort-icon');
        if (icon) {
            icon.className = 'bx bx-sort sort-icon';
        }
    });
    
    // Set new sort direction
    headerElement.setAttribute('data-direction', newDirection);
    const icon = headerElement.querySelector('.sort-icon');
    if (icon) {
        icon.className = newDirection === 'asc' ? 'bx bx-sort-up sort-icon' : 'bx bx-sort-down sort-icon';
    }
    
    // Sort rows
    rows.sort((a, b) => {
        let aValue = a.getAttribute(`data-sort-${sortBy}`) || '';
        let bValue = b.getAttribute(`data-sort-${sortBy}`) || '';
        
        // Handle numeric sorting
        if (sortBy === 'price' || sortBy === 'date') {
            aValue = parseFloat(aValue) || 0;
            bValue = parseFloat(bValue) || 0;
        } else {
            aValue = aValue.toLowerCase();
            bValue = bValue.toLowerCase();
        }
        
        if (newDirection === 'asc') {
            return aValue > bValue ? 1 : -1;
        } else {
            return aValue < bValue ? 1 : -1;
        }
    });
    
    // Reorder rows in table
    rows.forEach(row => tbody.appendChild(row));
}

// Checkbox Functionality
function setupCheckboxes() {
    const selectAllCheckbox = document.getElementById('selectAll');
    const rowCheckboxes = document.querySelectorAll('.service-checkbox');

    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function() {
            rowCheckboxes.forEach(checkbox => {
                checkbox.checked = this.checked;
            });
        });
    }

    rowCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const checkedCount = document.querySelectorAll('.service-checkbox:checked').length;
            const totalCount = rowCheckboxes.length;
            
            if (selectAllCheckbox) {
                selectAllCheckbox.checked = checkedCount === totalCount;
                selectAllCheckbox.indeterminate = checkedCount > 0 && checkedCount < totalCount;
            }
        });
    });
}

// Modal Handlers
function setupModalHandlers() {
    // Close modal when clicking outside
    document.addEventListener('click', function(e) {
        if (e.target.classList.contains('modal')) {
            closeModal(e.target);
        }
    });
    
    // Close modal with Escape key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape') {
            const openModal = document.querySelector('.modal.show');
            if (openModal) {
                closeModal(openModal);
            }
        }
    });
}

// Service Management Functions
function toggleServiceStatus(serviceId) {
    fetch(`/Admin/Service/ToggleStatus/${serviceId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': getAntiForgeryToken()
        }
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showNotification(data.message, 'success');
            setTimeout(() => location.reload(), 1000);
        } else {
            showNotification(data.message, 'error');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showNotification('Có lỗi xảy ra khi cập nhật trạng thái', 'error');
    });
}

function deleteService(serviceId) {
    if (confirm('Bạn có chắc chắn muốn xóa dịch vụ này?')) {
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = `/Admin/Service/Delete/${serviceId}`;
        
        const tokenInput = document.createElement('input');
        tokenInput.type = 'hidden';
        tokenInput.name = '__RequestVerificationToken';
        tokenInput.value = getAntiForgeryToken();
        form.appendChild(tokenInput);
        
        document.body.appendChild(form);
        form.submit();
    }
}

// Image Management Functions
function showAddImageModal() {
    showNotification('Chức năng thêm hình ảnh sẽ sớm được triển khai', 'info');
}

function editImage(imageId) {
    showNotification('Chức năng chỉnh sửa hình ảnh sẽ sớm được triển khai', 'info');
}

function deleteImage(imageId) {
    if (confirm('Bạn có chắc chắn muốn xóa hình ảnh này?')) {
        fetch(`/Admin/Service/DeleteImage/${imageId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getAntiForgeryToken()
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                showNotification(data.message, 'success');
                setTimeout(() => location.reload(), 1000);
            } else {
                showNotification(data.message, 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showNotification('Có lỗi xảy ra khi xóa hình ảnh', 'error');
        });
    }
}

// Variant Management Functions
function showAddVariantModal() {
    showNotification('Chức năng thêm biến thể sẽ sớm được triển khai', 'info');
}

function editVariant(variantId) {
    showNotification('Chức năng chỉnh sửa biến thể sẽ sớm được triển khai', 'info');
}

function deleteVariant(variantId) {
    if (confirm('Bạn có chắc chắn muốn xóa biến thể này?')) {
        fetch(`/Admin/Service/DeleteVariant/${variantId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getAntiForgeryToken()
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                showNotification(data.message, 'success');
                setTimeout(() => location.reload(), 1000);
            } else {
                showNotification(data.message, 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showNotification('Có lỗi xảy ra khi xóa biến thể', 'error');
        });
    }
}

// Addon Management Functions
function showAddAddonModal() {
    showNotification('Chức năng thêm addon sẽ sớm được triển khai', 'info');
}

function editAddon(addonId) {
    showNotification('Chức năng chỉnh sửa addon sẽ sớm được triển khai', 'info');
}

function deleteAddon(addonId) {
    if (confirm('Bạn có chắc chắn muốn xóa addon này?')) {
        fetch(`/Admin/Service/DeleteAddon/${addonId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getAntiForgeryToken()
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                showNotification(data.message, 'success');
                setTimeout(() => location.reload(), 1000);
            } else {
                showNotification(data.message, 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showNotification('Có lỗi xảy ra khi xóa addon', 'error');
        });
    }
}

// FAQ Management Functions
function showAddFAQModal() {
    showNotification('Chức năng thêm FAQ sẽ sớm được triển khai', 'info');
}

function editFAQ(faqId) {
    showNotification('Chức năng chỉnh sửa FAQ sẽ sớm được triển khai', 'info');
}

function deleteFAQ(faqId) {
    if (confirm('Bạn có chắc chắn muốn xóa FAQ này?')) {
        fetch(`/Admin/Service/DeleteFAQ/${faqId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getAntiForgeryToken()
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                showNotification(data.message, 'success');
                setTimeout(() => location.reload(), 1000);
            } else {
                showNotification(data.message, 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showNotification('Có lỗi xảy ra khi xóa FAQ', 'error');
        });
    }
}

// Note Management Functions
function showAddNoteModal() {
    showNotification('Chức năng thêm ghi chú sẽ sớm được triển khai', 'info');
}

function editNote(noteId) {
    showNotification('Chức năng chỉnh sửa ghi chú sẽ sớm được triển khai', 'info');
}

function deleteNote(noteId) {
    if (confirm('Bạn có chắc chắn muốn xóa ghi chú này?')) {
        fetch(`/Admin/Service/DeleteNote/${noteId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getAntiForgeryToken()
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                showNotification(data.message, 'success');
                setTimeout(() => location.reload(), 1000);
            } else {
                showNotification(data.message, 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showNotification('Có lỗi xảy ra khi xóa ghi chú', 'error');
        });
    }
}

// Utility Functions
function getAntiForgeryToken() {
    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
    return tokenInput ? tokenInput.value : '';
}

function showModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
        modal.classList.add('show');
        document.body.style.overflow = 'hidden';
    }
}

function closeModal(modal) {
    if (typeof modal === 'string') {
        modal = document.getElementById(modal);
    }
    if (modal) {
        modal.classList.remove('show');
        document.body.style.overflow = '';
    }
}

function showNotification(message, type = 'info') {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <div class="notification-content">
            <i class="bx ${getNotificationIcon(type)}"></i>
            <span>${message}</span>
        </div>
        <button class="notification-close" onclick="closeNotification(this)">
            <i class="bx bx-x"></i>
        </button>
    `;

    // Add to page
    let container = document.querySelector('.notification-container');
    if (!container) {
        container = document.createElement('div');
        container.className = 'notification-container';
        document.body.appendChild(container);
    }
    container.appendChild(notification);

    // Auto remove after 5 seconds
    setTimeout(() => {
        closeNotification(notification.querySelector('.notification-close'));
    }, 5000);
}

function getNotificationIcon(type) {
    switch (type) {
        case 'success': return 'bx-check-circle';
        case 'error': return 'bx-error-circle';
        case 'warning': return 'bx-error';
        default: return 'bx-info-circle';
    }
}

function closeNotification(button) {
    const notification = button.closest('.notification');
    if (notification) {
        notification.style.animation = 'slideOut 0.3s ease-in-out';
        setTimeout(() => {
            notification.remove();
        }, 300);
    }
}

 