// Accordion
window.addEventListener('DOMContentLoaded', function() {
  document.querySelectorAll('.accordion-title').forEach(title => {
    title.addEventListener('click', function() {
      const item = this.parentElement;
      const iconOpen = this.querySelector('.icon-open');
      const iconClose = this.querySelector('.icon-close');
      item.classList.toggle('open');
      if(item.classList.contains('open')) {
        iconOpen.style.display = 'none';
        iconClose.style.display = '';
      } else {
        iconOpen.style.display = '';
        iconClose.style.display = 'none';
      }
    });
  });
  document.querySelectorAll('.accordion-item').forEach(item => {
    const iconOpen = item.querySelector('.icon-open');
    const iconClose = item.querySelector('.icon-close');
    if(item.classList.contains('open')) {
      iconOpen.style.display = 'none';
      iconClose.style.display = '';
    } else {
      iconOpen.style.display = '';
      iconClose.style.display = 'none';
    }
  });
});

// Tabs
window.addEventListener('DOMContentLoaded', function() {
  const tabItems = document.querySelectorAll('.tab-item[role="tab"]');
  const tabContents = document.querySelectorAll('.item.content');

  function updateTabStyles(activeTab) {
    tabItems.forEach(tab => {
      const checkboxFilled = tab.querySelector('.checkbox-filled');
      const checkboxOutline = tab.querySelector('.checkbox-outline');
      if (tab === activeTab) {
        checkboxFilled.style.display = 'block';
        checkboxOutline.style.display = 'none';
      } else {
        checkboxFilled.style.display = 'none';
        checkboxOutline.style.display = 'block';
      }
    });
  }

  tabItems.forEach(tab => {
    tab.addEventListener('click', function() {
      tabItems.forEach(t => {
        t.classList.remove('active');
        t.setAttribute('aria-selected', 'false');
        t.setAttribute('aria-expanded', 'false');
      });
      this.classList.add('active');
      this.setAttribute('aria-selected', 'true');
      this.setAttribute('aria-expanded', 'true');
      updateTabStyles(this);
      const targetId = this.getAttribute('aria-controls');
      tabContents.forEach(content => {
        if (content.id === targetId) {
          content.style.display = '';
        } else {
          content.style.display = 'none';
        }
      });
    });
  });

  const activeTab = document.querySelector('.tab-item.active');
  if (activeTab) {
    updateTabStyles(activeTab);
    const targetId = activeTab.getAttribute('aria-controls');
    tabContents.forEach(content => {
      if (content.id === targetId) {
        content.style.display = '';
      } else {
        content.style.display = 'none';
      }
    });
  }
});

// Quantity selector, variant, thumbnail
window.addEventListener('DOMContentLoaded', function() {
  // Quantity selector
  const minusBtn = document.querySelector('.qty-btn.minus');
  const plusBtn = document.querySelector('.qty-btn.plus');
  const qtyInput = document.querySelector('.qty-input');
  if (minusBtn && plusBtn && qtyInput) {
    minusBtn.addEventListener('click', () => {
      let value = parseInt(qtyInput.value);
      if (value > 1) {
        qtyInput.value = value - 1;
      }
    });
    plusBtn.addEventListener('click', () => {
      let value = parseInt(qtyInput.value);
      qtyInput.value = value + 1;
    });
    qtyInput.addEventListener('change', () => {
      let value = parseInt(qtyInput.value);
      if (value < 1) {
        qtyInput.value = 1;
      }
    });
  }

  // Variant selector
  const variantBtns = document.querySelectorAll('.product-variants button');
  variantBtns.forEach(btn => {
    btn.addEventListener('click', function() {
      variantBtns.forEach(b => b.classList.remove('selected'));
      this.classList.add('selected');
    });
  });

  // Thumbnail images
  const mainImg = document.querySelector('.main-img');
  const thumbnails = document.querySelectorAll('.thumb-list img');
  const thumbScroll = document.querySelector('.thumb-scroll');
  if (mainImg && thumbnails.length) {
    thumbnails.forEach(thumb => {
      thumb.addEventListener('click', () => {
        thumbnails.forEach(t => t.classList.remove('active'));
        thumb.classList.add('active');
        mainImg.src = thumb.src;
      });
    });
  }
  // Thumbnail scroll functionality
  if (thumbScroll) {
    let scrollPosition = 0;
    const scrollStep = 50; // Height of one thumbnail
    thumbScroll.addEventListener('click', () => {
      const thumbList = document.querySelector('.thumb-list');
      scrollPosition += scrollStep;
      thumbList.scrollTop = scrollPosition;
    });
  }
}); 