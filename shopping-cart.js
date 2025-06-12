function formatPrice(price) {
  return price.toLocaleString('vi-VN') + ' đ';
}

function renderCart() {
  const cart = JSON.parse(localStorage.getItem('cart') || '[]');
  const cartHeader = document.querySelector('.cart-header');
  const cartItemsContainer = document.querySelector('.cart-items');
  const cartEmpty = document.querySelector('.cart-empty');
  const cartSubtotal = document.querySelector('.cart-subtotal');
  const cartTotal = document.querySelector('.cart-total');

  if (!cart.length) {
    cartItemsContainer.innerHTML = '';
    cartEmpty.style.display = '';
    cartSubtotal.textContent = '0 đ';
    cartTotal.textContent = '0 đ';
    return;
  }
  cartHeader.style.display = '';
  cartHeader.innerHTML = '<span>Thông tin sản phẩm</span><span>Đơn giá</span>';
  cartEmpty.style.display = 'none';

  // Render sản phẩm
  cartItemsContainer.innerHTML = cart.map((item, idx) => `
    <div class="cart-item" data-idx="${idx}">
      <div class="cart-product-info">
        <img src="${item.img}" alt="">
        <div>
          <div class="cart-product-name">${item.name}</div>
          <div class="cart-product-qty">
            <button class="qty-btn minus">-</button>
            <input type="number" value="${item.qty}" min="1" class="qty-input">
            <button class="qty-btn plus">+</button>
          </div>
        </div>
      </div>
      <div class="cart-product-price">${formatPrice(item.price)}</div>
      <button class="cart-remove" title="Xóa">&times;</button>
    </div>
  `).join('');

  // Tính tổng
  let subtotal = cart.reduce((sum, item) => sum + item.price * item.qty, 0);
  cartSubtotal.textContent = formatPrice(subtotal);
  cartTotal.textContent = formatPrice(subtotal);

  // Sự kiện tăng/giảm/xóa
  cartItemsContainer.querySelectorAll('.qty-btn.minus').forEach(btn => {
    btn.onclick = function() {
      const idx = this.closest('.cart-item').dataset.idx;
      if (cart[idx].qty > 1) {
        cart[idx].qty--;
        localStorage.setItem('cart', JSON.stringify(cart));
        renderCart();
      }
    };
  });
  cartItemsContainer.querySelectorAll('.qty-btn.plus').forEach(btn => {
    btn.onclick = function() {
      const idx = this.closest('.cart-item').dataset.idx;
      cart[idx].qty++;
      localStorage.setItem('cart', JSON.stringify(cart));
      renderCart();
    };
  });
  cartItemsContainer.querySelectorAll('.qty-input').forEach(input => {
    input.onchange = function() {
      const idx = this.closest('.cart-item').dataset.idx;
      let val = parseInt(this.value);
      if (isNaN(val) || val < 1) val = 1;
      cart[idx].qty = val;
      localStorage.setItem('cart', JSON.stringify(cart));
      renderCart();
    };
  });
  cartItemsContainer.querySelectorAll('.cart-remove').forEach(btn => {
    btn.onclick = function() {
      const idx = this.closest('.cart-item').dataset.idx;
      cart.splice(idx, 1);
      localStorage.setItem('cart', JSON.stringify(cart));
      renderCart();
    };
  });
}

document.addEventListener('DOMContentLoaded', function() {
  renderCart();
  document.querySelector('.continue-shopping').onclick = function() {
    window.location.href = 'index.html';
  };
  document.querySelector('.buy-now').onclick = function() {
    alert('Chức năng thanh toán sẽ được bổ sung!');
  };
  document.querySelector('.promo-apply').onclick = function() {
    alert('Mã khuyến mãi chỉ là demo!');
  };
}); 