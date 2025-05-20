// Dropdown 2 cấp
function setupDropdown2Col(selector) {
  document.querySelectorAll(selector).forEach(function(dropdown) {
    var leftItems = dropdown.querySelectorAll('.dropdown-left ul li');
    var panels = dropdown.querySelectorAll('.dropdown-panel');
    function showPanel(key) {
      panels.forEach(p => p.classList.remove('active'));
      var panel = dropdown.querySelector('.dropdown-panel[data-panel="' + key + '"]');
      if(panel) panel.classList.add('active');
    }
    leftItems.forEach(function(item, idx) {
      item.addEventListener('mouseenter', function() {
        leftItems.forEach(i => i.classList.remove('active'));
        item.classList.add('active');
        showPanel(item.getAttribute('data-menu'));
      });
      // Mặc định chọn mục đầu tiên
      if(idx === 0) {
        item.classList.add('active');
        showPanel(item.getAttribute('data-menu'));
      }
    });
    // Khi rời khỏi dropdown, bỏ active
    dropdown.addEventListener('mouseleave', function() {
      leftItems.forEach(i => i.classList.remove('active'));
      panels.forEach(p => p.classList.remove('active'));
    });
  });
}
setupDropdown2Col('.dropdown-menu.dropdown-2col');

// Slider banner
(function() {
  var slides = document.querySelectorAll('.slider .slide');
  var current = 0;
  var total = slides.length;
  var interval = null;
  function show(idx) {
    slides.forEach((s, i) => s.classList.toggle('active', i === idx));
    current = idx;
  }
  function next() {
    show((current + 1) % total);
  }
  function prev() {
    show((current - 1 + total) % total);
  }
  document.querySelector('.slider-btn.next').onclick = next;
  document.querySelector('.slider-btn.prev').onclick = prev;
  interval = setInterval(next, 3000);
  // Dừng khi hover slider
  document.querySelector('.slider').addEventListener('mouseenter', function() {
    clearInterval(interval);
  });
  document.querySelector('.slider').addEventListener('mouseleave', function() {
    interval = setInterval(next, 3000);
  });
  show(0);
})();

// Slider cho sản phẩm bán chạy & khuyến mãi
function setupProductSlider(sliderSelector) {
  document.querySelectorAll(sliderSelector).forEach(function(slider) {
    const list = slider.querySelector('.product-list');
    const cards = Array.from(list.children);
    const prevBtn = slider.querySelector('.product-slider-btn.prev');
    const nextBtn = slider.querySelector('.product-slider-btn.next');
    const visible = 4;
    let start = 0;
    function update() {
      cards.forEach((card, i) => {
        card.style.display = (i >= start && i < start + visible) ? 'flex' : 'none';
      });
      prevBtn.disabled = start === 0;
      nextBtn.disabled = start + visible >= cards.length;
    }
    prevBtn.onclick = function() {
      if (start > 0) {
        start--;
        update();
      }
    };
    nextBtn.onclick = function() {
      if (start + visible < cards.length) {
        start++;
        update();
      }
    };
    update();
  });
}
setupProductSlider('.product-slider');

// DEMO ALERT FOR NON-WORKING BUTTONS
document.querySelectorAll('a[href="#"], .footer-newsletter button, .footer-col a, .service-card, .category-card').forEach(function(el) {
  el.addEventListener('click', function(e) {
    e.preventDefault();
    alert('Chức năng này chỉ là demo!');
  });
}); 