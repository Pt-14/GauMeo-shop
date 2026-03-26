/**
 * Account JavaScript
 * Contains: Forgot password flow
 */

(function() {
    'use strict';

    // Wait for DOM to be ready
    document.addEventListener('DOMContentLoaded', function() {
        initializeForgotPassword();
    });

    // ============================================
    // 1. FORGOT PASSWORD FLOW (from forgot-password.js)
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
