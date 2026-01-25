/**
 * Account JavaScript - Loaded only when user is authenticated
 * Contains: Logout button animation, Forgot password flow
 */

(function() {
    'use strict';

    // Wait for DOM to be ready
    document.addEventListener('DOMContentLoaded', function() {
        initializeLogoutButton();
        initializeForgotPassword();
    });

    // ============================================
    // 1. LOGOUT BUTTON ANIMATION (from logout-button.js)
    // ============================================
    function initializeLogoutButton() {
        const logoutButtons = document.querySelectorAll('.logoutButton');
        if (logoutButtons.length === 0) return;

        logoutButtons.forEach(button => {
            button.state = 'default';
            
            // Function to transition button from one state to the next
            const updateButtonState = (btn, state) => {
                if (logoutButtonStates[state]) {
                    btn.state = state;
                    for (let key in logoutButtonStates[state]) {
                        btn.style.setProperty(key, logoutButtonStates[state][key]);
                    }
                }
            };
            
            // Mouse hover listeners
            button.addEventListener('mouseenter', () => {
                if (button.state === 'default') {
                    updateButtonState(button, 'hover');
                }
            });
            
            button.addEventListener('mouseleave', () => {
                if (button.state === 'hover') {
                    updateButtonState(button, 'default');
                }
            });
            
            // Click listener
            button.addEventListener('click', (e) => {
                if (button.state === 'default' || button.state === 'hover') {
                    e.preventDefault(); // Prevent default form submission
                    button.classList.add('clicked');
                    updateButtonState(button, 'walking1');
                    
                    setTimeout(() => {
                        button.classList.add('door-slammed');
                        updateButtonState(button, 'walking2');
                        
                        setTimeout(() => {
                            button.classList.add('falling');
                            updateButtonState(button, 'falling1');
                            
                            setTimeout(() => {
                                updateButtonState(button, 'falling2');
                                
                                setTimeout(() => {
                                    updateButtonState(button, 'falling3');
                                    
                                    setTimeout(() => {
                                        button.classList.remove('clicked');
                                        button.classList.remove('door-slammed');
                                        button.classList.remove('falling');
                                        updateButtonState(button, 'default');
                                        
                                        // Submit the form after animation completes
                                        const form = button.closest('form');
                                        if (form) {
                                            form.submit();
                                        }
                                    }, 1000);
                                }, logoutButtonStates['falling2']['--walking-duration']);
                            }, logoutButtonStates['falling1']['--walking-duration']);
                        }, logoutButtonStates['walking2']['--figure-duration']);
                    }, logoutButtonStates['walking1']['--figure-duration']);
                }
            });
        });
    }

    // Logout button animation states
    const logoutButtonStates = {
        'default': {
            '--figure-duration': '100',
            '--transform-figure': 'none',
            '--walking-duration': '100',
            '--transform-arm1': 'none',
            '--transform-wrist1': 'none',
            '--transform-arm2': 'none',
            '--transform-wrist2': 'none',
            '--transform-leg1': 'none',
            '--transform-calf1': 'none',
            '--transform-leg2': 'none',
            '--transform-calf2': 'none'
        },
        'hover': {
            '--figure-duration': '100',
            '--transform-figure': 'translateX(1.5px)',
            '--walking-duration': '100',
            '--transform-arm1': 'rotate(-5deg)',
            '--transform-wrist1': 'rotate(-15deg)',
            '--transform-arm2': 'rotate(5deg)',
            '--transform-wrist2': 'rotate(6deg)',
            '--transform-leg1': 'rotate(-10deg)',
            '--transform-calf1': 'rotate(5deg)',
            '--transform-leg2': 'rotate(20deg)',
            '--transform-calf2': 'rotate(-20deg)'
        },
        'walking1': {
            '--figure-duration': '300',
            '--transform-figure': 'translateX(11px)',
            '--walking-duration': '300',
            '--transform-arm1': 'translateX(-4px) translateY(-2px) rotate(120deg)',
            '--transform-wrist1': 'rotate(-5deg)',
            '--transform-arm2': 'translateX(4px) rotate(-110deg)',
            '--transform-wrist2': 'rotate(-5deg)',
            '--transform-leg1': 'translateX(-3px) rotate(80deg)',
            '--transform-calf1': 'rotate(-30deg)',
            '--transform-leg2': 'translateX(4px) rotate(-60deg)',
            '--transform-calf2': 'rotate(20deg)'
        },
        'walking2': {
            '--figure-duration': '400',
            '--transform-figure': 'translateX(17px)',
            '--walking-duration': '300',
            '--transform-arm1': 'rotate(60deg)',
            '--transform-wrist1': 'rotate(-15deg)',
            '--transform-arm2': 'rotate(-45deg)',
            '--transform-wrist2': 'rotate(6deg)',
            '--transform-leg1': 'rotate(-5deg)',
            '--transform-calf1': 'rotate(10deg)',
            '--transform-leg2': 'rotate(10deg)',
            '--transform-calf2': 'rotate(-20deg)'
        },
        'falling1': {
            '--figure-duration': '1600',
            '--walking-duration': '400',
            '--transform-arm1': 'rotate(-60deg)',
            '--transform-wrist1': 'none',
            '--transform-arm2': 'rotate(30deg)',
            '--transform-wrist2': 'rotate(120deg)',
            '--transform-leg1': 'rotate(-30deg)',
            '--transform-calf1': 'rotate(-20deg)',
            '--transform-leg2': 'rotate(20deg)'
        },
        'falling2': {
            '--walking-duration': '300',
            '--transform-arm1': 'rotate(-100deg)',
            '--transform-arm2': 'rotate(-60deg)',
            '--transform-wrist2': 'rotate(60deg)',
            '--transform-leg1': 'rotate(80deg)',
            '--transform-calf1': 'rotate(20deg)',
            '--transform-leg2': 'rotate(-60deg)'
        },
        'falling3': {
            '--walking-duration': '500',
            '--transform-arm1': 'rotate(-30deg)',
            '--transform-wrist1': 'rotate(40deg)',
            '--transform-arm2': 'rotate(50deg)',
            '--transform-wrist2': 'none',
            '--transform-leg1': 'rotate(-30deg)',
            '--transform-leg2': 'rotate(20deg)',
            '--transform-calf2': 'none'
        }
    };

    // ============================================
    // 2. FORGOT PASSWORD FLOW (from forgot-password.js)
    // ============================================
    function initializeForgotPassword() {
        const sendOtpForm = document.getElementById('sendOtpForm');
        const verifyOtpForm = document.getElementById('verifyOtpForm');
        const resetPasswordForm = document.getElementById('resetPasswordForm');
        
        // Only initialize if we're on forgot password page
        if (!sendOtpForm && !verifyOtpForm && !resetPasswordForm) return;

        let currentEmail = '';
        let isOtpVerified = false;
        let otpTimer;

        // Send OTP form
        if (sendOtpForm) {
            sendOtpForm.addEventListener('submit', async function(e) {
                e.preventDefault();
                
                const email = document.getElementById('Email').value;
                if (!email || !email.match(/^[^\s@]+@[^\s@]+\.[^\s@]+$/)) {
                    showError('Vui lòng nhập email hợp lệ');
                    return;
                }

                showLoading();
                currentEmail = email;

                const formData = new FormData();
                formData.append('Email', email);

                try {
                    const response = await fetch('/Account/SendOtp', {
                        method: 'POST',
                        headers: {
                            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                        },
                        body: formData
                    });

                    const result = await response.json();
                    if (result.success) {
                        showSuccess(result.message);
                        if (typeof window.showSlide === 'function') {
                            window.showSlide(2);
                        }
                        startOtpTimer();
                    } else {
                        showError(result.message);
                    }
                } catch (error) {
                    console.error('Error:', error);
                    showError('Đã có lỗi xảy ra. Vui lòng thử lại.');
                } finally {
                    hideLoading();
                }
            });
        }

        // Verify OTP form
        if (verifyOtpForm) {
            verifyOtpForm.addEventListener('submit', async function(e) {
                e.preventDefault();
                showLoading();

                const otp = document.getElementById('Otp').value;
                if (!otp || otp.length !== 6) {
                    showError('Vui lòng nhập mã OTP 6 số');
                    hideLoading();
                    return;
                }

                const formData = new FormData();
                formData.append('email', currentEmail);
                formData.append('otp', otp);

                try {
                    const response = await fetch('/Account/VerifyOtp', {
                        method: 'POST',
                        headers: {
                            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                        },
                        body: formData
                    });

                    const result = await response.json();
                    if (result.success) {
                        showSuccess(result.message);
                        isOtpVerified = true;
                        if (typeof window.showSlide === 'function') {
                            window.showSlide(3);
                        }
                    } else {
                        showError(result.message);
                    }
                } catch (error) {
                    console.error('Error:', error);
                    showError('Đã có lỗi xảy ra. Vui lòng thử lại.');
                } finally {
                    hideLoading();
                }
            });
        }

        // Reset password form
        if (resetPasswordForm) {
            resetPasswordForm.addEventListener('submit', async function(e) {
                e.preventDefault();
                if (!isOtpVerified) {
                    showError('Vui lòng xác thực OTP trước.');
                    return;
                }

                const newPassword = document.getElementById('NewPassword').value;
                const confirmPassword = document.getElementById('ConfirmPassword').value;

                if (!newPassword || newPassword.length < 8) {
                    showError('Mật khẩu phải có ít nhất 8 ký tự');
                    return;
                }

                if (newPassword !== confirmPassword) {
                    showError('Mật khẩu xác nhận không khớp');
                    return;
                }

                showLoading();

                const formData = new FormData();
                formData.append('email', currentEmail);
                formData.append('newPassword', newPassword);
                formData.append('confirmPassword', confirmPassword);

                try {
                    const response = await fetch('/Account/ResetPassword', {
                        method: 'POST',
                        headers: {
                            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                        },
                        body: formData
                    });

                    const result = await response.json();
                    if (result.success) {
                        showSuccess(result.message);
                        setTimeout(() => {
                            window.location.href = '/Account/Login';
                        }, 2000);
                    } else {
                        showError(result.message);
                    }
                } catch (error) {
                    console.error('Error:', error);
                    showError('Đã có lỗi xảy ra. Vui lòng thử lại.');
                } finally {
                    hideLoading();
                }
            });
        }

        // Resend OTP button
        const resendOtpBtn = document.getElementById('resendOtp');
        if (resendOtpBtn) {
            resendOtpBtn.addEventListener('click', async function(e) {
                e.preventDefault();
                this.style.display = 'none';
                if (sendOtpForm) {
                    await sendOtpForm.dispatchEvent(new Event('submit'));
                }
            });
        }

        // Helper functions
        function showLoading() {
            const submitButtons = document.querySelectorAll('input[type="submit"]');
            submitButtons.forEach(button => {
                button.disabled = true;
                button.value = 'Đang xử lý...';
            });
        }

        function hideLoading() {
            const submitButtons = document.querySelectorAll('input[type="submit"]');
            submitButtons.forEach(button => {
                button.disabled = false;
                button.value = button.getAttribute('data-original-text');
            });
        }

        function showError(message) {
            const errorDiv = document.getElementById('error-message');
            if (errorDiv) {
                errorDiv.textContent = message;
                errorDiv.style.display = 'block';
                setTimeout(() => {
                    errorDiv.style.display = 'none';
                }, 3000);
            }
        }

        function showSuccess(message) {
            const successDiv = document.getElementById('success-message');
            if (successDiv) {
                successDiv.textContent = message;
                successDiv.style.display = 'block';
                setTimeout(() => {
                    successDiv.style.display = 'none';
                }, 3000);
            }
        }

        function startOtpTimer() {
            let timeLeft = 300; // 5 minutes in seconds
            const timerDisplay = document.getElementById('otpTimer');
            if (!timerDisplay) return;
            
            clearInterval(otpTimer);
            otpTimer = setInterval(() => {
                const minutes = Math.floor(timeLeft / 60);
                const seconds = timeLeft % 60;
                timerDisplay.textContent = `${minutes}:${seconds.toString().padStart(2, '0')}`;
                
                if (timeLeft <= 0) {
                    clearInterval(otpTimer);
                    timerDisplay.textContent = 'Hết hạn';
                    const resendBtn = document.getElementById('resendOtp');
                    if (resendBtn) {
                        resendBtn.style.display = 'block';
                    }
                }
                timeLeft--;
            }, 1000);
        }

    }
})();
