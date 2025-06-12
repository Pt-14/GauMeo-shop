// Xử lý giỏ hàng
class CartManager {
  constructor() {
    this.cart = JSON.parse(localStorage.getItem('cart')) || [];
    this.badge = document.querySelector('.header-cart-badge');
    this.init();
  }

  init() {
    // Xử lý nút thêm vào giỏ hàng ở trang chủ
    document.querySelectorAll('.add-to-cart-btn').forEach(button => {
      button.addEventListener('click', (e) => this.handleAddToCart(e));
    });

    // Xử lý nút thêm vào giỏ hàng ở trang chi tiết
    const detailAddBtn = document.querySelector('.product-actions .add-to-cart');
    if (detailAddBtn) {
      detailAddBtn.addEventListener('click', (e) => this.handleDetailAddToCart(e));
    }

    // Kiểm tra và hiển thị badge khi trang được tải
    this.updateBadge();
    
    // Cập nhật hiển thị giỏ hàng nếu đang ở trang giỏ hàng
    if (window.location.pathname.includes('shopping cart.html')) {
      this.updateCartDisplay();
    }
  }

  handleAddToCart(e) {
    e.preventDefault();
    e.stopPropagation();
    
    const productCard = e.target.closest('.product-card');
    const productName = productCard.querySelector('h3').textContent;
    const productPrice = productCard.querySelector('.product-price').textContent;
    
    this.addToCart({
      name: productName,
      price: productPrice,
      quantity: 1,
      weight: null
    });
  }

  handleDetailAddToCart(e) {
    e.preventDefault();
    
    const productName = document.querySelector('.product-info h1').textContent;
    const productPrice = document.querySelector('.product-price').textContent;
    
    // Lấy khối lượng đã chọn (nếu có)
    const selectedWeight = document.querySelector('.product-variants button.selected');
    const weight = selectedWeight ? selectedWeight.textContent : null;
    
    // Lấy số lượng
    const quantityInput = document.querySelector('.product-quantity input');
    const quantity = quantityInput ? parseInt(quantityInput.value) : 1;
    
    this.addToCart({
      name: productName,
      price: productPrice,
      quantity: quantity,
      weight: weight
    });
  }

  addToCart(item) {
    this.cart.push(item);
    localStorage.setItem('cart', JSON.stringify(this.cart));
    this.updateBadge();
    alert('Đã thêm sản phẩm vào giỏ hàng!');
  }

  updateBadge() {
    if (this.cart.length > 0) {
      this.badge.classList.add('show');
      this.badge.textContent = this.cart.length;
    } else {
      this.badge.classList.remove('show');
    }
  }

  updateCartDisplay() {
    const cartList = document.querySelector('.cart-list');
    const cartEmpty = document.querySelector('.cart-empty');
    const cartItems = document.querySelectorAll('.cart-item');
    
    if (this.cart.length === 0) {
      cartEmpty.style.display = 'block';
      cartItems.forEach(item => item.style.display = 'none');
    } else {
      cartEmpty.style.display = 'none';
      // Cập nhật hiển thị từng sản phẩm trong giỏ hàng
      this.cart.forEach((item, index) => {
        if (cartItems[index]) {
          cartItems[index].style.display = 'flex';
          const nameEl = cartItems[index].querySelector('.cart-product-name');
          const priceEl = cartItems[index].querySelector('.cart-product-price');
          const qtyInput = cartItems[index].querySelector('.cart-product-qty input');
          
          nameEl.textContent = item.name;
          priceEl.textContent = item.price;
          qtyInput.value = item.quantity;
        }
      });
    }
  }
}

// Khởi tạo CartManager khi trang được tải
document.addEventListener('DOMContentLoaded', () => {
  new CartManager();
}); 