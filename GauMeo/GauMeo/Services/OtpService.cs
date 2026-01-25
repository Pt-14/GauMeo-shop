using Microsoft.Extensions.Caching.Memory;

namespace GauMeo.Services
{
    public interface IOtpService
    {
        string GenerateOtp();
        void StoreOtp(string email, string otp);
        bool ValidateOtp(string email, string otp);
    }

    public class OtpService : IOtpService
    {
        private readonly IMemoryCache _cache;
        private const int OTP_LENGTH = 6;
        private const int OTP_EXPIRY_MINUTES = 5;

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void StoreOtp(string email, string otp)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(OTP_EXPIRY_MINUTES));

            _cache.Set($"OTP_{email}", otp, cacheOptions);
        }

        public bool ValidateOtp(string email, string otp)
        {
            if (_cache.TryGetValue($"OTP_{email}", out string? storedOtp) && storedOtp != null)
            {
                return storedOtp == otp;
            }
            return false;
        }
    }
} 