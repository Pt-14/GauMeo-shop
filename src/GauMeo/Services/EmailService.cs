using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GauMeo.Services
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(string toEmail, string otp);
        Task SendWelcomeEmailAsync(string toEmail, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendOtpEmailAsync(string toEmail, string otp)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var smtpServer = smtpSettings["Server"];
                var smtpPortStr = smtpSettings["Port"];
                var smtpPort = int.Parse(smtpPortStr ?? "587");
                var smtpUsername = smtpSettings["Username"] ?? string.Empty;
                var smtpPassword = smtpSettings["Password"];

                using var message = new MailMessage();
                message.From = new MailAddress(smtpUsername, "GauMeo Shop");
                message.To.Add(toEmail);
                message.Subject = "Mã OTP đặt lại mật khẩu - GauMeo Shop";
                message.Body = $@"
                    <h2>Xin chào,</h2>
                    <p>Mã OTP của bạn để đặt lại mật khẩu là: <strong style='font-size: 20px; color: #4CAF50;'>{otp}</strong></p>
                    <p>Mã này sẽ hết hạn sau 5 phút.</p>
                    <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
                    <br>
                    <p>Trân trọng,</p>
                    <p>GauMeo Shop</p>";
                message.IsBodyHtml = true;

                await SendEmailAsync(message);
                _logger.LogInformation($"OTP email sent successfully to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending OTP email: {ex.Message}");
                throw new Exception("Không thể gửi email. Vui lòng kiểm tra cấu hình SMTP hoặc thử lại sau.", ex);
            }
        }

        public async Task SendWelcomeEmailAsync(string toEmail, string welcomeMessage)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var smtpUsername = smtpSettings["Username"] ?? string.Empty;

                using var message = new MailMessage();
                message.From = new MailAddress(smtpUsername, "GauMeo Shop");
                message.To.Add(toEmail);
                message.Subject = "Chào mừng bạn đến với GauMeo Shop! 🐾";
                message.Body = welcomeMessage;
                message.IsBodyHtml = true;

                await SendEmailAsync(message);
                _logger.LogInformation($"Welcome email sent successfully to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending welcome email: {ex.Message}");
                throw new Exception("Không thể gửi email chào mừng. Vui lòng thử lại sau.", ex);
            }
        }

        private async Task SendEmailAsync(MailMessage message)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var smtpServer = smtpSettings["Server"];
            var smtpPortStr = smtpSettings["Port"];
            var smtpPort = int.Parse(smtpPortStr ?? "587");
            var smtpUsername = smtpSettings["Username"] ?? string.Empty;
            var smtpPassword = smtpSettings["Password"];

            using var client = new SmtpClient(smtpServer);
            client.Port = smtpPort;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            
            if (smtpPort == 465)
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                _logger.LogInformation("Using SSL/TLS on port 465");
            }
            else if (smtpPort == 587)
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                _logger.LogInformation("Using STARTTLS on port 587");
            }

            await client.SendMailAsync(message);
        }
    }
} 