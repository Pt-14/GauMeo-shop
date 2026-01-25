// Promotion Management JavaScript

document.addEventListener('DOMContentLoaded', function() {
    initializePromotionManagement();
});

function initializePromotionManagement() {
    setupViewToggle();
    setupSearch();
    setupFilters();
    setupSorting();
    setupCheckboxes();
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
    const searchInput = document.getElementById('promotionSearch');
    if (!searchInput) return;

    searchInput.addEventListener('input', function() {
        const searchTerm = this.value.toLowerCase();
        filterPromotions();
    });
}

// Filter Functionality
function setupFilters() {
    const statusFilter = document.getElementById('statusFilter');
    const discountFilter = document.getElementById('discountFilter');

    if (statusFilter) {
        statusFilter.addEventListener('change', filterPromotions);
    }
    
    if (discountFilter) {
        discountFilter.addEventListener('change', filterPromotions);
    }
}

function filterPromotions() {
    const searchTerm = document.getElementById('promotionSearch')?.value.toLowerCase() || '';
    const statusFilter = document.getElementById('statusFilter')?.value || '';
    const discountFilter = document.getElementById('discountFilter')?.value || '';

    // Filter table rows
    const tableRows = document.querySelectorAll('#promotionsTable tbody tr');
    tableRows.forEach(row => {
        const name = row.getAttribute('data-name') || '';
        const status = row.getAttribute('data-status') || '';
        const discountRange = row.getAttribute('data-discount-range') || '';

        const matchesSearch = name.includes(searchTerm);
        const matchesStatus = !statusFilter || status === statusFilter;
        const matchesDiscount = !discountFilter || discountRange === discountFilter;

        if (matchesSearch && matchesStatus && matchesDiscount) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });

    // Filter grid cards
    const gridCards = document.querySelectorAll('.promotion-card');
    gridCards.forEach(card => {
        const name = card.getAttribute('data-name') || '';
        const status = card.getAttribute('data-status') || '';
        const discountRange = card.getAttribute('data-discount-range') || '';

        const matchesSearch = name.includes(searchTerm);
        const matchesStatus = !statusFilter || status === statusFilter;
        const matchesDiscount = !discountFilter || discountRange === discountFilter;

        if (matchesSearch && matchesStatus && matchesDiscount) {
            card.style.display = '';
        } else {
            card.style.display = 'none';
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
    const table = document.getElementById('promotionsTable');
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
        if (sortBy === 'discount' || sortBy.includes('date')) {
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
    const rowCheckboxes = document.querySelectorAll('.promotion-checkbox');

    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function() {
            rowCheckboxes.forEach(checkbox => {
                checkbox.checked = this.checked;
            });
        });
    }

    rowCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const checkedCount = document.querySelectorAll('.promotion-checkbox:checked').length;
            const totalCount = rowCheckboxes.length;
            
            if (selectAllCheckbox) {
                selectAllCheckbox.checked = checkedCount === totalCount;
                selectAllCheckbox.indeterminate = checkedCount > 0 && checkedCount < totalCount;
            }
        });
    });
}

// Delete Promotion Function
function deletePromotion(promotionId) {
    if (confirm('Bạn có chắc chắn muốn xóa khuyến mãi này?')) {
        // TODO: Implement server-side deletion via AJAX call
        console.log('Deleting promotion:', promotionId);
        
        // Remove the row from the table
        const row = document.querySelector(`tr input[value="${promotionId}"]`)?.closest('tr');
        const card = document.querySelector(`[data-promotion-id="${promotionId}"]`);
        
        if (row) row.remove();
        if (card) card.remove();
        
        // Show success message
        showNotification('Khuyến mãi đã được xóa thành công!', 'success');
    }
}

// Utility Functions
function showNotification(message, type = 'info') {
    // Create notification element
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <i class="bx bx-${type === 'success' ? 'check' : type === 'error' ? 'x' : 'info'}-circle"></i>
        <span>${message}</span>
    `;
    
    // Add styles
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        background: ${type === 'success' ? '#d4edda' : type === 'error' ? '#f8d7da' : '#d1ecf1'};
        color: ${type === 'success' ? '#155724' : type === 'error' ? '#721c24' : '#0c5460'};
        border: 1px solid ${type === 'success' ? '#c3e6cb' : type === 'error' ? '#f5c6cb' : '#bee5eb'};
        border-radius: 8px;
        padding: 12px 20px;
        display: flex;
        align-items: center;
        gap: 8px;
        font-weight: 500;
        z-index: 9999;
        animation: slideIn 0.3s ease-out;
    `;
    
    // Add to page
    document.body.appendChild(notification);
    
    // Remove after 3 seconds
    setTimeout(() => {
        notification.style.animation = 'slideOut 0.3s ease-in';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

// Export functions for global access
window.deletePromotion = deletePromotion; 