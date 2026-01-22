// Order Management JavaScript for Admin

let currentOrderIdToCancel = null;

// Initialize when document is ready
document.addEventListener('DOMContentLoaded', function() {
    initializeOrderManagement();
});

function initializeOrderManagement() {
    // Search functionality
    const searchInput = document.getElementById('orderSearch');
    if (searchInput) {
        searchInput.addEventListener('input', handleSearch);
    }

    // Modal close events
    window.onclick = function(event) {
        const modal = document.getElementById('cancelOrderModal');
        if (event.target == modal) {
            closeCancelModal();
        }
    }
}

function filterByStatus() {
    const status = document.getElementById('statusFilter').value;
    const baseUrl = window.location.pathname;
    window.location.href = baseUrl + '?status=' + encodeURIComponent(status);
}

function updateOrderStatus(orderId, status) {
    if (confirm(`Bạn có chắc chắn muốn cập nhật trạng thái đơn hàng này không?`)) {
        submitFormWithData('/Admin/Order/UpdateStatus', {
            id: orderId,
            status: status
        });
    }
}

function updatePaymentStatus(orderId, paymentStatus) {
    if (confirm(`Bạn có chắc chắn muốn cập nhật trạng thái thanh toán không?`)) {
        submitFormWithData('/Admin/Order/UpdatePaymentStatus', {
            id: orderId,
            paymentStatus: paymentStatus
        });
    }
}

function showCancelModal(orderId, orderNumber) {
    currentOrderIdToCancel = orderId;
    const cancelOrderNumber = document.getElementById('cancelOrderNumber');
    if (cancelOrderNumber) {
        cancelOrderNumber.textContent = '#' + orderNumber;
    }
    
    const modal = document.getElementById('cancelOrderModal');
    if (modal) {
        modal.style.display = 'block';
    }
}

function closeCancelModal() {
    const modal = document.getElementById('cancelOrderModal');
    if (modal) {
        modal.style.display = 'none';
    }
    
    const cancelReason = document.getElementById('cancelReason');
    if (cancelReason) {
        cancelReason.value = '';
    }
    
    currentOrderIdToCancel = null;
}

function confirmCancelOrder() {
    const reasonElement = document.getElementById('cancelReason');
    if (!reasonElement) {
        alert('Không tìm thấy trường lý do hủy đơn');
        return;
    }
    
    const reason = reasonElement.value.trim();
    if (!reason) {
        alert('Vui lòng nhập lý do hủy đơn');
        return;
    }
    
    submitFormWithData('/Admin/Order/Cancel', {
        id: currentOrderIdToCancel,
        cancelReason: reason
    });
    
    closeCancelModal();
}

function handleSearch() {
    const searchTerm = this.value.toLowerCase();
    const rows = document.querySelectorAll('.order-row');
    
    rows.forEach(row => {
        const orderNumber = row.getAttribute('data-order');
        const customerName = row.getAttribute('data-customer');
        
        if (orderNumber && customerName) {
            if (orderNumber.includes(searchTerm) || customerName.includes(searchTerm)) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        }
    });
}

function submitFormWithData(actionUrl, data) {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = actionUrl;
    
    // Add anti-forgery token
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    if (tokenElement) {
        const tokenInput = document.createElement('input');
        tokenInput.type = 'hidden';
        tokenInput.name = '__RequestVerificationToken';
        tokenInput.value = tokenElement.value;
        form.appendChild(tokenInput);
    }
    
    // Add data fields
    for (const [key, value] of Object.entries(data)) {
        const input = document.createElement('input');
        input.type = 'hidden';
        input.name = key;
        input.value = value;
        form.appendChild(input);
    }
    
    document.body.appendChild(form);
    form.submit();
}

// View toggle functionality
function toggleView(viewType) {
    const tableView = document.getElementById('tableView');
    const gridView = document.getElementById('gridView');
    const viewButtons = document.querySelectorAll('.view-btn');
    
    viewButtons.forEach(btn => btn.classList.remove('active'));
    
    if (viewType === 'table') {
        if (tableView) tableView.style.display = 'block';
        if (gridView) gridView.style.display = 'none';
        document.querySelector('[data-view="table"]')?.classList.add('active');
    } else if (viewType === 'grid') {
        if (tableView) tableView.style.display = 'none';
        if (gridView) gridView.style.display = 'block';
        document.querySelector('[data-view="grid"]')?.classList.add('active');
    }
}

// Sort functionality
function sortTable(column) {
    const table = document.getElementById('ordersTable');
    if (!table) return;
    
    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));
    
    // Determine sort direction
    const currentIcon = document.querySelector(`[data-sort="${column}"] .sort-icon`);
    const isAscending = !currentIcon?.classList.contains('sorted-asc');
    
    // Reset all sort icons
    document.querySelectorAll('.sort-icon').forEach(icon => {
        icon.classList.remove('sorted-asc', 'sorted-desc');
    });
    
    // Set current sort icon
    if (currentIcon) {
        currentIcon.classList.add(isAscending ? 'sorted-asc' : 'sorted-desc');
    }
    
    // Sort rows
    rows.sort((a, b) => {
        const aVal = a.getAttribute(`data-sort-${column}`) || '';
        const bVal = b.getAttribute(`data-sort-${column}`) || '';
        
        if (column === 'amount' || column === 'date') {
            const aNum = parseFloat(aVal) || 0;
            const bNum = parseFloat(bVal) || 0;
            return isAscending ? aNum - bNum : bNum - aNum;
        } else {
            return isAscending ? aVal.localeCompare(bVal) : bVal.localeCompare(aVal);
        }
    });
    
    // Reappend sorted rows
    rows.forEach(row => tbody.appendChild(row));
}

// Add event listeners for sortable headers
document.addEventListener('DOMContentLoaded', function() {
    document.querySelectorAll('.sortable').forEach(header => {
        header.addEventListener('click', function() {
            const sortColumn = this.getAttribute('data-sort');
            if (sortColumn) {
                sortTable(sortColumn);
            }
        });
    });
    
    // Add event listeners for view toggle buttons
    document.querySelectorAll('.view-btn').forEach(btn => {
        btn.addEventListener('click', function() {
            const viewType = this.getAttribute('data-view');
            if (viewType) {
                toggleView(viewType);
            }
        });
    });
}); 