using GauMeo.Models;
using GauMeo.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authentication;

namespace GauMeo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        private string GenerateAvatar(string userName)
        {
            // Tạo thư mục avatars nếu chưa tồn tại
            string avatarPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars");
            if (!Directory.Exists(avatarPath))
            {
                Directory.CreateDirectory(avatarPath);
            }

            // Tạo tên file avatar với timestamp để tránh trùng lặp
            string fileName = $"avatar_{DateTime.Now.Ticks}.png";
            string fullPath = Path.Combine(avatarPath, fileName);

            // Tạo màu nền ngẫu nhiên nhưng đẹp
            Random random = new Random();
            Color[] colors = new[]
            {
                Color.FromArgb(255, 71, 105, 255),   // Xanh dương
                Color.FromArgb(255, 255, 71, 87),    // Đỏ hồng
                Color.FromArgb(255, 71, 255, 87),    // Xanh lá
                Color.FromArgb(255, 255, 184, 71),   // Cam
                Color.FromArgb(255, 184, 71, 255),   // Tím
                Color.FromArgb(255, 71, 255, 184),   // Ngọc
                Color.FromArgb(255, 255, 71, 184),   // Hồng
                Color.FromArgb(255, 71, 184, 255)    // Xanh biển
            };
            Color backgroundColor = colors[random.Next(colors.Length)];

            // Tạo hình ảnh
            using (Bitmap bitmap = new Bitmap(200, 200))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Làm mịn hình ảnh
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    // Vẽ nền tròn
                    using (SolidBrush brush = new SolidBrush(backgroundColor))
                    {
                        graphics.FillEllipse(brush, 0, 0, 200, 200);
                    }

                    // Vẽ chữ cái đầu tiên của tên người dùng
                    using (Font font = new Font("Arial", 80, FontStyle.Bold))
                    using (SolidBrush textBrush = new SolidBrush(Color.White))
                    {
                        string text = userName.Substring(0, 1).ToUpper();
                        SizeF textSize = graphics.MeasureString(text, font);
                        float x = (200 - textSize.Width) / 2;
                        float y = (200 - textSize.Height) / 2;
                        graphics.DrawString(text, font, textBrush, x, y);
                    }
                }

                // Lưu hình ảnh
                bitmap.Save(fullPath, ImageFormat.Png);
            }

            return $"/images/avatars/{fileName}";
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Kiểm tra nếu user là admin thì chuyển đến trang admin
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Tìm user bằng email
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email không tồn tại trong hệ thống.");
                    return View(model);
                }

                // Đăng nhập với username và password
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Người dùng đã đăng nhập.");
                    
                    // Kiểm tra nếu user đã có avatar trong database
                    if (!string.IsNullOrEmpty(user.Avatar))
                    {
                        // Nếu có avatar trong DB thì dùng avatar đó
                        var claims = await _userManager.GetClaimsAsync(user);
                        var avatarClaim = claims.FirstOrDefault(c => c.Type == "Avatar");
                        if (avatarClaim != null)
                        {
                            await _userManager.RemoveClaimAsync(user, avatarClaim);
                        }
                        await _userManager.AddClaimAsync(user, new Claim("Avatar", user.Avatar));
                    }
                    else
                    {
                        // Nếu chưa có avatar trong DB thì tạo avatar động
                        string avatarPath = GenerateAvatar(user.UserName);
                        var claims = await _userManager.GetClaimsAsync(user);
                        var avatarClaim = claims.FirstOrDefault(c => c.Type == "Avatar");
                        if (avatarClaim != null)
                        {
                            await _userManager.RemoveClaimAsync(user, avatarClaim);
                        }
                        await _userManager.AddClaimAsync(user, new Claim("Avatar", avatarPath));
                    }
                    
                    // Kiểm tra nếu user là admin thì luôn chuyển đến trang admin
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    
                    // Nếu không phải admin và có returnUrl hợp lệ thì redirect đến returnUrl
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    // Mặc định redirect về trang chủ
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Đăng nhập không thành công. Vui lòng kiểm tra lại email và mật khẩu.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa (bao gồm cả Google users)
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email này đã được sử dụng. Vui lòng sử dụng email khác hoặc đăng nhập nếu bạn đã có tài khoản.");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address,
                    LoginProvider = "Local", // Đặt là Local cho đăng ký thường
                    IsExternalLogin = false
                    // Không set Avatar ở đây, để null
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Người dùng đã tạo tài khoản mới.");

                    // Tạo avatar động cho lần đăng nhập đầu tiên
                    string avatarPath = GenerateAvatar(user.UserName);
                    await _userManager.AddClaimAsync(user, new Claim("Avatar", avatarPath));

                    // Thêm người dùng vào role User
                    await _userManager.AddToRoleAsync(user, "User");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Không tiết lộ rằng người dùng không tồn tại hoặc chưa được xác nhận
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                // Trong môi trường thực tế, bạn sẽ gửi email với mã đặt lại mật khẩu
                // Nhưng ở đây, chúng ta sẽ chỉ chuyển hướng đến trang xác nhận
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Người dùng đã đăng xuất.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {
            if (avatar == null || avatar.Length == 0)
            {
                return BadRequest("Vui lòng chọn ảnh để upload");
            }

            // Kiểm tra định dạng file
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!allowedTypes.Contains(avatar.ContentType.ToLower()))
            {
                return BadRequest("Chỉ chấp nhận file ảnh (JPEG, PNG, GIF)");
            }

            // Kiểm tra kích thước file (tối đa 5MB)
            if (avatar.Length > 5 * 1024 * 1024)
            {
                return BadRequest("Kích thước file không được vượt quá 5MB");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Tạo tên file mới
            string fileName = $"avatar_{user.Id}_{DateTime.Now.Ticks}{Path.GetExtension(avatar.FileName)}";
            string avatarPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars", fileName);

            // Lưu file
            using (var stream = new FileStream(avatarPath, FileMode.Create))
            {
                await avatar.CopyToAsync(stream);
            }

            // Cập nhật avatar trong database
            user.Avatar = $"/images/avatars/{fileName}";
            await _userManager.UpdateAsync(user);

            // Cập nhật claim
            var claims = await _userManager.GetClaimsAsync(user);
            var avatarClaim = claims.FirstOrDefault(c => c.Type == "Avatar");
            if (avatarClaim != null)
            {
                await _userManager.RemoveClaimAsync(user, avatarClaim);
            }
            await _userManager.AddClaimAsync(user, new Claim("Avatar", user.Avatar));

            return RedirectToAction("Profile");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var model = new ProfileViewModel
            {
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Avatar = !string.IsNullOrEmpty(user.Avatar) ? user.Avatar : User.FindFirst("Avatar")?.Value
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            user.FullName = model.FullName;
            user.Address = model.Address;
            user.DateOfBirth = model.DateOfBirth;
            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);
            // Cập nhật lại claim Avatar nếu có
            var claims = await _userManager.GetClaimsAsync(user);
            var avatarClaim = claims.FirstOrDefault(c => c.Type == "Avatar");
            if (avatarClaim != null && !string.IsNullOrEmpty(user.Avatar))
            {
                await _userManager.RemoveClaimAsync(user, avatarClaim);
                await _userManager.AddClaimAsync(user, new Claim("Avatar", user.Avatar));
            }
            ViewBag.Success = "Cập nhật thông tin thành công!";
            model.Avatar = !string.IsNullOrEmpty(user.Avatar) ? user.Avatar : User.FindFirst("Avatar")?.Value;
            return View(model);
        }

        // External Login Methods - Google OAuth

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            // Yêu cầu redirect đến external login provider (Google)
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi từ provider: {remoteError}");
                return RedirectToAction("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Lỗi tải thông tin từ external login provider.");
                return RedirectToAction("Login");
            }

            // Thử đăng nhập với external login provider
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            
            if (result.Succeeded)
            {
                _logger.LogInformation($"User đã đăng nhập với {info.LoginProvider} provider.");
                
                // Lấy user và cập nhật avatar claim
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user != null)
                {
                    await UpdateUserAvatar(user);
                    
                    // Kiểm tra role và redirect accordingly
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
                
                return LocalRedirect(returnUrl);
            }

            // Nếu user chưa có tài khoản, lấy email từ Google
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            
            if (email != null)
            {
                // Kiểm tra xem đã có user với email này chưa
                var existingUser = await _userManager.FindByEmailAsync(email);
                
                if (existingUser != null)
                {
                    // Nếu đã có user với email này, liên kết external login
                    var addLoginResult = await _userManager.AddLoginAsync(existingUser, info);
                    
                    if (addLoginResult.Succeeded)
                    {
                        // Cập nhật thông tin từ Google
                        existingUser.GoogleId = info.ProviderKey;
                        existingUser.LoginProvider = info.LoginProvider;
                        existingUser.IsExternalLogin = true;
                        
                        // Lấy tên từ Google nếu chưa có
                        if (string.IsNullOrEmpty(existingUser.FullName))
                        {
                            existingUser.FullName = info.Principal.FindFirstValue(ClaimTypes.Name);
                        }
                        
                        await _userManager.UpdateAsync(existingUser);
                        await UpdateUserAvatar(existingUser);
                        
                        // Đăng nhập user
                        await _signInManager.SignInAsync(existingUser, isPersistent: false);
                        
                        // Kiểm tra role và redirect
                        if (await _userManager.IsInRoleAsync(existingUser, "Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        foreach (var error in addLoginResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    // User chưa có tài khoản - chuyển đến trang xác nhận
                    ViewData["LoginProvider"] = info.LoginProvider;
                    ViewData["ReturnUrl"] = returnUrl;
                    
                    var model = new ExternalLoginConfirmationViewModel
                    {
                        Email = email,
                        FullName = info.Principal.FindFirstValue(ClaimTypes.Name),
                        LoginProvider = info.LoginProvider,
                        ProviderKey = info.ProviderKey,
                        ReturnUrl = returnUrl
                    };
                    
                    return View("ExternalLoginConfirmation", model);
                }
            }

            // Nếu không thể lấy email hoặc có lỗi
            ModelState.AddModelError(string.Empty, "Không thể lấy thông tin email từ Google.");
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LoginProvider"] = model.LoginProvider;
                ViewData["ReturnUrl"] = model.ReturnUrl;
                return View(model);
            }

            // Lấy thông tin external login từ Google
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi xác thực với Google.");
                return RedirectToAction("Login");
            }

            // Kiểm tra email có trùng không
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Email này đã được sử dụng bởi tài khoản khác.");
                ViewData["LoginProvider"] = model.LoginProvider;
                ViewData["ReturnUrl"] = model.ReturnUrl;
                return View(model);
            }

            // Tạo user mới với thông tin đã xác nhận
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address,
                GoogleId = info.ProviderKey,
                LoginProvider = info.LoginProvider,
                IsExternalLogin = true,
                EmailConfirmed = true // Google đã xác thực email
            };

            var createResult = await _userManager.CreateAsync(user);
            
            if (createResult.Succeeded)
            {
                // Thêm external login
                var addLoginResult = await _userManager.AddLoginAsync(user, info);
                
                if (addLoginResult.Succeeded)
                {
                    // Thêm vào role User
                    await _userManager.AddToRoleAsync(user, "User");
                    
                    // Tạo avatar và claims
                    await UpdateUserAvatar(user);
                    
                    // Đăng nhập user
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    
                    _logger.LogInformation($"User đã tạo tài khoản với {info.LoginProvider} provider.");
                    
                    string returnUrl = model.ReturnUrl ?? Url.Content("~/");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    // Nếu thêm external login thất bại, xóa user vừa tạo
                    await _userManager.DeleteAsync(user);
                    foreach (var error in addLoginResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["LoginProvider"] = model.LoginProvider;
            ViewData["ReturnUrl"] = model.ReturnUrl;
            return View(model);
        }

        // Helper method để cập nhật avatar
        private async Task UpdateUserAvatar(ApplicationUser user)
        {
            string avatarPath;
            
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                // Nếu user đã có avatar trong DB
                avatarPath = user.Avatar;
            }
            else
            {
                // Tạo avatar động
                avatarPath = GenerateAvatar(user.UserName ?? user.Email);
            }
            
            // Cập nhật claim
            var claims = await _userManager.GetClaimsAsync(user);
            var avatarClaim = claims.FirstOrDefault(c => c.Type == "Avatar");
            if (avatarClaim != null)
            {
                await _userManager.RemoveClaimAsync(user, avatarClaim);
            }
            await _userManager.AddClaimAsync(user, new Claim("Avatar", avatarPath));
        }
    }
}
