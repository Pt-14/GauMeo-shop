using GauMeo.Data;
using GauMeo.Models;
using Microsoft.EntityFrameworkCore;

namespace GauMeo.Services
{
    public interface INewsletterService
    {
        Task<bool> SubscribeAsync(string email);
        Task<bool> IsSubscribedAsync(string email);
        Task SendWelcomeEmailAsync(string email);
    }

    public class NewsletterService : INewsletterService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly ILogger<NewsletterService> _logger;

        public NewsletterService(
            ApplicationDbContext context,
            IEmailService emailService,
            ILogger<NewsletterService> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<bool> SubscribeAsync(string email)
        {
            try
            {
                var existingSubscription = await _context.Newsletters
                    .FirstOrDefaultAsync(n => n.Email == email);

                if (existingSubscription != null)
                {
                    if (!existingSubscription.IsActive)
                    {
                        existingSubscription.IsActive = true;
                        await _context.SaveChangesAsync();
                        await SendWelcomeEmailAsync(email);
                        return true;
                    }
                    return false; // Đã đăng ký
                }

                var newsletter = new Newsletter
                {
                    Email = email,
                    SubscribedAt = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Newsletters.Add(newsletter);
                await _context.SaveChangesAsync();
                await SendWelcomeEmailAsync(email);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error subscribing to newsletter: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsSubscribedAsync(string email)
        {
            return await _context.Newsletters
                .AnyAsync(n => n.Email == email && n.IsActive);
        }

        public async Task SendWelcomeEmailAsync(string email)
        {
            try
            {
                var welcomeMessage = $@"
                    <h2>Chào mừng bạn đến với GauMeo Shop! 🐾</h2>
                    <p>Cảm ơn bạn đã đăng ký nhận tin từ GauMeo Shop.</p>
                    <p>Từ nay bạn sẽ là người đầu tiên nhận được thông tin về:</p>
                    <ul>
                        <li>Sản phẩm mới</li>
                        <li>Khuyến mãi đặc biệt</li>
                        <li>Mẹo chăm sóc thú cưng</li>
                        <li>Và nhiều thông tin hữu ích khác</li>
                    </ul>
                    <p>Hãy theo dõi email của bạn để không bỏ lỡ những ưu đãi tốt nhất từ GauMeo Shop nhé!</p>
                    <br>
                    <p>Trân trọng,</p>
                    <p>GauMeo Shop</p>";

                await _emailService.SendWelcomeEmailAsync(email, welcomeMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending welcome email: {ex.Message}");
                throw;
            }
        }
    }
} 