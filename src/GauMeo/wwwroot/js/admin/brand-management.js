// Brand Management JavaScript
document.addEventListener('DOMContentLoaded', function() {
    initializeBrandManagement();
});

let currentSort = {
    column: null,
    direction: 'asc'
};

function initializeBrandManagement() {
    // Search functionality
    const searchInput = document.getElementById('brandSearch');
    const statusFilter = document.getElementById('statusFilter');

    // View toggle
    const viewBtns = document.querySelectorAll('.view-btn');
    const tableView = document.getElementById('tableView');
    const gridView = document.getElementById('gridView');

    // Table sorting
    const sortableHeaders = document.querySelectorAll('.sortable');

    // Event listeners for search and filters
    if (searchInput) searchInput.addEventListener('input', filterBrands);
    if (statusFilter) statusFilter.addEventListener('change', filterBrands);

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
    const brandCheckboxes = document.querySelectorAll('.brand-checkbox');

    if (selectAll) {
        selectAll.addEventListener('change', function() {
            brandCheckboxes.forEach(checkbox => {
                checkbox.checked = this.checked;
            });
        });
    }
}

// Filter brands based on search and filter criteria
function filterBrands() {
    const searchInput = document.getElementById('brandSearch');
    const statusFilter = document.getElementById('statusFilter');

    const searchTerm = searchInput ? searchInput.value.toLowerCase() : '';
    const statusValue = statusFilter ? statusFilter.value : '';

    const rows = document.querySelectorAll('.brand-row, .brand-card');
    
    rows.forEach(row => {
        const name = row.dataset.name || '';
        const status = row.dataset.status || '';

        const matchesSearch = name.includes(searchTerm);
        const matchesStatus = !statusValue || status === statusValue;

        if (matchesSearch && matchesStatus) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });

    updateResultCount();
}

// Sort table by column
function sortTable(column) {
    const tableBody = document.querySelector('#brandsTable tbody');
    const rows = Array.from(tableBody.querySelectorAll('.brand-row'));

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
    const visibleRows = document.querySelectorAll('.brand-row:not([style*="display: none"])');
    const totalRows = document.querySelectorAll('.brand-row');
    
    // You can add a result count display here if needed
    console.log(`Showing ${visibleRows.length} of ${totalRows.length} brands`);
}

// Delete brand function
function deleteBrand(id) {
    if (confirm('Bạn có chắc chắn muốn xóa thương hiệu này?')) {
        // Show loading
        AdminUtils.showLoading();
        
        // Here you would make an AJAX call to delete the brand
        // For now, redirect to delete action
        window.location.href = `/Admin/Brand/Delete?id=${id}`;
    }
}

// Bulk actions
function performBulkAction(action) {
    const selectedBrands = document.querySelectorAll('.brand-checkbox:checked');
    const selectedIds = Array.from(selectedBrands).map(checkbox => checkbox.value);

    if (selectedIds.length === 0) {
        alert('Vui lòng chọn ít nhất một thương hiệu');
        return;
    }

    switch (action) {
        case 'delete':
            if (confirm(`Bạn có chắc chắn muốn xóa ${selectedIds.length} thương hiệu đã chọn?`)) {
                // Perform bulk delete
                console.log('Bulk delete:', selectedIds);
            }
            break;
        case 'activate':
            // Perform bulk activate
            console.log('Bulk activate:', selectedIds);
            break;
        case 'deactivate':
            // Perform bulk deactivate
            console.log('Bulk deactivate:', selectedIds);
            break;
    }
} 