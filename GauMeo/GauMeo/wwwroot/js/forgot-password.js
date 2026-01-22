let currentEmail = '';
let isOtpVerified = false;

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
    errorDiv.textContent = message;
    errorDiv.style.display = 'block';
    setTimeout(() => {
        errorDiv.style.display = 'none';
    }, 3000);
}

function showSuccess(message) {
    const successDiv = document.getElementById('success-message');
    successDiv.textContent = message;
    successDiv.style.display = 'block';
    setTimeout(() => {
        successDiv.style.display = 'none';
    }, 3000);
}

document.getElementById('sendOtpForm').addEventListener('submit', async function(e) {
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
            showSlide(2);
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

document.getElementById('verifyOtpForm').addEventListener('submit', async function(e) {
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
            showSlide(3);
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

document.getElementById('resetPasswordForm').addEventListener('submit', async function(e) {
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

let otpTimer;
function startOtpTimer() {
    let timeLeft = 300; // 5 minutes in seconds
    const timerDisplay = document.getElementById('otpTimer');
    
    clearInterval(otpTimer);
    otpTimer = setInterval(() => {
        const minutes = Math.floor(timeLeft / 60);
        const seconds = timeLeft % 60;
        timerDisplay.textContent = `${minutes}:${seconds.toString().padStart(2, '0')}`;
        
        if (timeLeft <= 0) {
            clearInterval(otpTimer);
            timerDisplay.textContent = 'Hết hạn';
            document.getElementById('resendOtp').style.display = 'block';
        }
        timeLeft--;
    }, 1000);
}

document.getElementById('resendOtp').addEventListener('click', async function(e) {
    e.preventDefault();
    this.style.display = 'none';
    await document.getElementById('sendOtpForm').dispatchEvent(new Event('submit'));
}); 