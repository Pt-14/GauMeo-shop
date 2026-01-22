document.addEventListener('DOMContentLoaded', function() {
    const sliderItems = document.querySelectorAll('.slider-item');
    let currentIndex = 0;
    let autoSlideInterval;
    
    if (sliderItems.length === 0) return;
    
    function showSlide(index) {
        // Ẩn slide hiện tại
        sliderItems[currentIndex].classList.remove('active');
        
        // Cập nhật index
        currentIndex = index;
        if (currentIndex >= sliderItems.length) currentIndex = 0;
        if (currentIndex < 0) currentIndex = sliderItems.length - 1;
        
        // Hiển thị slide mới
        sliderItems[currentIndex].classList.add('active');
    }
    
    function nextSlide() {
        showSlide(currentIndex + 1);
    }
    
    function startAutoSlide() {
        autoSlideInterval = setInterval(nextSlide, 7000); // 7 giây
    }
    
    function stopAutoSlide() {
        clearInterval(autoSlideInterval);
    }
    
    // Global function cho navigation buttons
    window.changeSlide = function(direction) {
        stopAutoSlide();
        showSlide(currentIndex + direction);
        startAutoSlide(); // Restart auto slide sau khi click
    };
    
    // Bắt đầu auto slide
    startAutoSlide();
    
    // Pause khi hover vào slider
    const slider = document.querySelector('.top-bar-slider');
    if (slider) {
        slider.addEventListener('mouseenter', stopAutoSlide);
        slider.addEventListener('mouseleave', startAutoSlide);
    }
}); 