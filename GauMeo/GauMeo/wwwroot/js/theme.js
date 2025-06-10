// Theme toggle functionality
document.addEventListener('DOMContentLoaded', function() {
    const themeToggleBtn = document.querySelector('.btn-theme-toggle');
    
    function setTheme(dark) {
        if(dark) {
            document.body.classList.add('dark');
            themeToggleBtn.textContent = '☀️';
            themeToggleBtn.setAttribute('aria-pressed', 'true');
            localStorage.setItem('theme', 'dark');
        } else {
            document.body.classList.remove('dark');
            themeToggleBtn.textContent = '🌙';
            themeToggleBtn.setAttribute('aria-pressed', 'false');
            localStorage.setItem('theme', 'light');
        }
    }
    
    // Initialize theme based on localStorage, default to light mode
    const savedTheme = localStorage.getItem('theme');
    if(savedTheme === 'dark') {
        setTheme(true);
    } else {
        // Default to light mode if no theme is saved or if it's 'light'
        setTheme(false);
    }
    
    // Theme toggle button click handler
    themeToggleBtn.addEventListener('click', () => {
        setTheme(!document.body.classList.contains('dark'));
        themeToggleBtn.focus();
    });
    
    // Listen for system theme changes (optional - can be removed if you don't want system preference)
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', e => {
        // Only change if user hasn't explicitly set a theme
        if(!localStorage.getItem('theme')) {
            setTheme(e.matches);
        }
    });
}); 