document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('newsletterForm');
    const submitButton = document.getElementById('newsletterSubmit');
    const messageDiv = document.getElementById('newsletterMessage');

    if (form) {
        form.addEventListener('submit', async function(e) {
            e.preventDefault();
            
            // Disable submit button
            submitButton.disabled = true;
            submitButton.textContent = 'Đang xử lý...';
            
            const email = document.getElementById('newsletterEmail').value;
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

            try {
                const response = await fetch('/Newsletter/Subscribe', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                        'RequestVerificationToken': token
                    },
                    body: `email=${encodeURIComponent(email)}`
                });

                const result = await response.json();
                
                messageDiv.textContent = result.message;
                messageDiv.style.display = 'block';
                messageDiv.className = 'newsletter-message ' + (result.success ? 'success' : 'error');

                if (result.success) {
                    form.reset();
                }

                // Hide message after 5 seconds
                setTimeout(() => {
                    messageDiv.style.display = 'none';
                }, 5000);
            } catch (error) {
                messageDiv.textContent = 'Đã có lỗi xảy ra. Vui lòng thử lại sau.';
                messageDiv.style.display = 'block';
                messageDiv.className = 'newsletter-message error';
            } finally {
                // Re-enable submit button
                submitButton.disabled = false;
                submitButton.textContent = 'Gửi';
            }
        });
    }
}); 