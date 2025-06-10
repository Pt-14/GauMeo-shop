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
            localStorage.removeItem('theme');
        }
    }
    
    // Initialize theme based on localStorage or system preference
    if(localStorage.getItem('theme') === 'dark' || 
       (!localStorage.getItem('theme') && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        setTheme(true);
    } else {
        setTheme(false);
    }
    
    // Theme toggle button click handler
    themeToggleBtn.addEventListener('click', () => {
        setTheme(!document.body.classList.contains('dark'));
        themeToggleBtn.focus();
    });
    
    // Listen for system theme changes
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', e => {
        if(!localStorage.getItem('theme')) {
            setTheme(e.matches);
        }
    });
}); 