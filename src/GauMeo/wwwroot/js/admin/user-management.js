// User Management JavaScript
document.addEventListener('DOMContentLoaded', function() {
    initializeUserManagement();
});

let currentSort = {
    column: null,
    direction: 'asc'
};

function initializeUserManagement() {
    // Search functionality
    const searchInput = document.getElementById('userSearch');
    const statusFilter = document.getElementById('statusFilter');
    const roleFilter = document.getElementById('roleFilter');

    // View toggle
    const viewBtns = document.querySelectorAll('.view-btn');
    const tableView = document.getElementById('tableView');
    const gridView = document.getElementById('gridView');

    // Table sorting
    const sortableHeaders = document.querySelectorAll('.sortable');

    // Event listeners for search and filters
    if (searchInput) searchInput.addEventListener('input', filterUsers);
    if (statusFilter) statusFilter.addEventListener('change', filterUsers);
    if (roleFilter) roleFilter.addEventListener('change', filterUsers);

    // View toggle functionality
    viewBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            const view = this.dataset.view;
            
            viewBtns.forEach(b => b.classList.remove('active'));
            this.classList.add('active');

            if (view === 'table') {
                tableView.style.display = 'block';
                gridView.style.display = 'none';
            } else {
                tableView.style.display = 'none';
                gridView.style.display = 'grid';
            }
        });
    });

    // Table sorting functionality
    sortableHeaders.forEach(header => {
        header.addEventListener('click', function() {
            const sortColumn = this.dataset.sort;
            sortTable(sortColumn);
            updateSortIcons(this);
        });
    });

    // Select all functionality
    const selectAll = document.getElementById('selectAll');
    const userCheckboxes = document.querySelectorAll('.user-checkbox');

    if (selectAll) {
        selectAll.addEventListener('change', function() {
            userCheckboxes.forEach(checkbox => {
                checkbox.checked = this.checked;
            });
        });
    }
}

// Filter users based on search and filter criteria
function filterUsers() {
    const searchInput = document.getElementById('userSearch');
    const statusFilter = document.getElementById('statusFilter');
    const roleFilter = document.getElementById('roleFilter');

    const searchTerm = searchInput ? searchInput.value.toLowerCase() : '';
    const statusValue = statusFilter ? statusFilter.value : '';
    const roleValue = roleFilter ? roleFilter.value : '';

    const rows = document.querySelectorAll('.user-row, .user-card');
    
    rows.forEach(row => {
        const name = row.dataset.name || '';
        const email = row.dataset.email || '';
        const phone = row.dataset.phone || '';
        const status = row.dataset.status || '';
        const role = row.dataset.role || '';

        const matchesSearch = name.includes(searchTerm) || 
                             email.includes(searchTerm) || 
                             phone.includes(searchTerm);
        const matchesStatus = !statusValue || status === statusValue;
        const matchesRole = !roleValue || role === roleValue;

        if (matchesSearch && matchesStatus && matchesRole) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });

    updateResultCount();
}

// Sort table by column
function sortTable(column) {
    const tableBody = document.querySelector('#usersTable tbody');
    const rows = Array.from(tableBody.querySelectorAll('.user-row'));

    // Determine sort direction
    if (currentSort.column === column) {
        currentSort.direction = currentSort.direction === 'asc' ? 'desc' : 'asc';
    } else {
        currentSort.column = column;
        currentSort.direction = 'asc';
    }

    // Sort rows
    rows.sort((a, b) => {
        let aValue = a.dataset[`sort${column.charAt(0).toUpperCase() + column.slice(1)}`] || a.dataset[`sort-${column}`];
        let bValue = b.dataset[`sort${column.charAt(0).toUpperCase() + column.slice(1)}`] || b.dataset[`sort-${column}`];

        // Handle different data types
        if (column === 'date') {
            aValue = parseFloat(aValue) || 0;
            bValue = parseFloat(bValue) || 0;
        } else {
            aValue = aValue.toLowerCase();
            bValue = bValue.toLowerCase();
        }

        if (currentSort.direction === 'asc') {
            return aValue > bValue ? 1 : -1;
        } else {
            return aValue < bValue ? 1 : -1;
        }
    });

    // Reorder rows in DOM
    rows.forEach(row => tableBody.appendChild(row));
}

// Update sort icons
function updateSortIcons(activeHeader) {
    // Reset all sort icons
    document.querySelectorAll('.sort-icon').forEach(icon => {
        icon.className = 'bx bx-sort sort-icon';
    });

    // Update active header icon
    const icon = activeHeader.querySelector('.sort-icon');
    if (currentSort.direction === 'asc') {
        icon.className = 'bx bx-sort-up sort-icon';
    } else {
        icon.className = 'bx bx-sort-down sort-icon';
    }
}

// Update result count (optional)
function updateResultCount() {
    const visibleRows = document.querySelectorAll('.user-row:not([style*="display: none"])');
    const totalRows = document.querySelectorAll('.user-row');
    
    console.log(`Showing ${visibleRows.length} of ${totalRows.length} users`);
}

// Delete user function with AJAX
function deleteUser(id) {
    if (confirm('Bạn có chắc chắn muốn xóa người dùng này?')) {
        // Get anti-forgery token
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        
        fetch(`/Admin/User/Delete/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'RequestVerificationToken': token
            },
            body: `__RequestVerificationToken=${token}`
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                showNotification(data.message, 'success');
                // Remove the row from table
                const row = document.querySelector(`tr[data-sort-name*="${id}"], .user-card[data-name*="${id}"]`);
                if (row) {
                    row.remove();
                }
                location.reload(); // Refresh to update counts
            } else {
                showNotification(data.message, 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showNotification('Có lỗi xảy ra khi xóa người dùng!', 'error');
        });
    }
}

// Toggle user status (lock/unlock)
function toggleUserStatus(id) {
    fetch(`/Admin/User/ToggleStatus/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        }
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showNotification(data.message, 'success');
            location.reload(); // Refresh to update UI
        } else {
            showNotification(data.message, 'error');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showNotification('Có lỗi xảy ra khi cập nhật trạng thái!', 'error');
    });
}

// Show notification
function showNotification(message, type = 'info') {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <div class="notification-content">
            <i class='bx ${getNotificationIcon(type)}'></i>
            <span>${message}</span>
        </div>
        <button class="notification-close" onclick="this.parentElement.remove()">
            <i class='bx bx-x'></i>
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
        if (notification.parentElement) {
            notification.remove();
        }
    }, 5000);
}

// Get notification icon based on type
function getNotificationIcon(type) {
    switch (type) {
        case 'success': return 'bx-check-circle';
        case 'error': return 'bx-error-circle';
        case 'warning': return 'bx-error';
        default: return 'bx-info-circle';
    }
}

// Add notification styles
const notificationStyles = `
    .notification-container {
        position: fixed;
        top: 20px;
        right: 20px;
        z-index: 10000;
        max-width: 400px;
    }
    
    .notification {
        background: white;
        border-radius: 8px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        margin-bottom: 10px;
        padding: 16px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        border-left: 4px solid;
        animation: slideIn 0.3s ease-out;
    }
    
    .notification-success { border-left-color: #10b981; }
    .notification-error { border-left-color: #ef4444; }
    .notification-warning { border-left-color: #f59e0b; }
    .notification-info { border-left-color: #3b82f6; }
    
    .notification-content {
        display: flex;
        align-items: center;
        gap: 8px;
        flex: 1;
    }
    
    .notification-content i {
        font-size: 18px;
    }
    
    .notification-success .notification-content i { color: #10b981; }
    .notification-error .notification-content i { color: #ef4444; }
    .notification-warning .notification-content i { color: #f59e0b; }
    .notification-info .notification-content i { color: #3b82f6; }
    
    .notification-close {
        background: none;
        border: none;
        color: #6b7280;
        cursor: pointer;
        padding: 4px;
        border-radius: 4px;
        transition: background-color 0.2s;
    }
    
    .notification-close:hover {
        background-color: #f3f4f6;
    }
    
    @keyframes slideIn {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
`;

// Add styles to head
const styleSheet = document.createElement('style');
styleSheet.textContent = notificationStyles;
document.head.appendChild(styleSheet);

// Bulk actions
function performBulkAction(action) {
    const selectedUsers = document.querySelectorAll('.user-checkbox:checked');
    const selectedIds = Array.from(selectedUsers).map(checkbox => checkbox.value);

    if (selectedIds.length === 0) {
        alert('Vui lòng chọn ít nhất một người dùng');
        return;
    }

    switch (action) {
        case 'delete':
            if (confirm(`Bạn có chắc chắn muốn xóa ${selectedIds.length} người dùng đã chọn?`)) {
                // Perform bulk delete
                console.log('Bulk delete:', selectedIds);
            }
            break;
        case 'verify':
            // Perform bulk verify
            console.log('Bulk verify:', selectedIds);
            break;
        case 'unverify':
            // Perform bulk unverify
            console.log('Bulk unverify:', selectedIds);
            break;
    }
} 