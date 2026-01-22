using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GauMeo.Services;
using GauMeo.Models;
using GauMeo.Models.Orders;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace GauMeo.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, IOrderService orderService, UserManager<ApplicationUser> userManager, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var (userId, sessionId) = GetUserAndSessionId();
            var cartItems = await _cartService.GetCartItemsAsync(userId, sessionId);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            try
            {
                var (userId, sessionId) = GetUserAndSessionId();
                
                var success = await _cartService.AddToCartAsync(
                    userId, 
                    sessionId, 
                    request.ProductId, 
                    request.Quantity, 
                    request.SelectedVariants
                );

                if (success)
                {
                    var cartCount = await _cartService.GetCartItemCountAsync(userId, sessionId);
                    return Json(new { success = true, cartCount = cartCount, message = "Đã thêm sản phẩm vào giỏ hàng!" });
                }

                return Json(new { success = false, message = "Không thể thêm sản phẩm vào giỏ hàng!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product to cart");
                return Json(new { success = false, message = "Có lỗi xảy ra khi thêm sản phẩm vào giỏ hàng!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemRequest request)
        {
            try
            {
                var success = await _cartService.UpdateCartItemAsync(request.CartItemId, request.Quantity);
                
                if (success)
                {
                    var (userId, sessionId) = GetUserAndSessionId();
                    var cartTotal = await _cartService.GetCartTotalAsync(userId, sessionId);
                    var cartCount = await _cartService.GetCartItemCountAsync(userId, sessionId);
                    
                    return Json(new { success = true, cartTotal = cartTotal, cartCount = cartCount });
                }

                return Json(new { success = false, message = "Không thể cập nhật sản phẩm!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart item");
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật giỏ hàng!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCartItem([FromBody] RemoveCartItemRequest request)
        {
            try
            {
                var success = await _cartService.RemoveFromCartAsync(request.CartItemId);
                
                if (success)
                {
                    var (userId, sessionId) = GetUserAndSessionId();
                    var cartTotal = await _cartService.GetCartTotalAsync(userId, sessionId);
                    var cartCount = await _cartService.GetCartItemCountAsync(userId, sessionId);
                    
                    return Json(new { success = true, cartTotal = cartTotal, cartCount = cartCount });
                }

                return Json(new { success = false, message = "Không thể xóa sản phẩm!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cart item");
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa sản phẩm!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MergeCart([FromBody] MergeCartRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, message = "Người dùng chưa đăng nhập" });
                }

                var success = await _cartService.MergeCartAsync(request.SessionId, userId);
                if (success)
                {
                    var cartCount = await _cartService.GetCartItemCountAsync(userId, null);
                    return Json(new { success = true, cartCount = cartCount });
                }

                return Json(new { success = false, message = "Không thể hợp nhất giỏ hàng" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error merging cart");
                return Json(new { success = false, message = "Có lỗi xảy ra" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            try
            {
                var (userId, sessionId) = GetUserAndSessionId();
                var count = await _cartService.GetCartItemCountAsync(userId, sessionId);
                return Json(new { success = true, count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart count");
                return Json(new { success = false, count = 0 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var (userId, sessionId) = GetUserAndSessionId();
                var success = await _cartService.ClearCartAsync(userId, sessionId);
                
                if (success)
                {
                    return Json(new { success = true, message = "Đã xóa hết tất cả sản phẩm!" });
                }

                return Json(new { success = false, message = "Không thể xóa giỏ hàng!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart");
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa giỏ hàng!" });
            }
        }

        public async Task<IActionResult> Checkout()
        {
            var (userId, sessionId) = GetUserAndSessionId();
            var cartItems = await _cartService.GetCartItemsAsync(userId, sessionId);
            
            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng trống, không thể tiến hành thanh toán.";
                return RedirectToAction("Index");
            }

            var subTotal = cartItems.Sum(item => item.GetSubTotal());
            var shippingFee = subTotal >= 700000 ? 0 : 30000; // Free ship for orders >= 700k

            // Create checkout view model
            var model = new CheckoutViewModel
            {
                CartItems = cartItems,
                SubTotal = subTotal,
                ShippingFee = shippingFee,
                TotalAmount = subTotal + shippingFee
            };

            // Pre-fill customer info if user is logged in
            if (User.Identity.IsAuthenticated && userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    model.CustomerName = user.FullName ?? user.UserName ?? "";
                    model.CustomerEmail = user.Email ?? "";
                    model.CustomerPhone = user.PhoneNumber ?? "";
                    model.ShippingAddress = user.Address ?? "";
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Reload cart items for the view
                    var (userId, sessionId) = GetUserAndSessionId();
                    model.CartItems = await _cartService.GetCartItemsAsync(userId, sessionId);
                    model.SubTotal = model.CartItems.Sum(item => item.GetSubTotal());
                    model.ShippingFee = model.SubTotal >= 700000 ? 0 : 30000; // Free ship logic
                    model.TotalAmount = model.SubTotal + model.ShippingFee;
                    return View(model);
                }

                var (currentUserId, currentSessionId) = GetUserAndSessionId();
                
                // Create order request
                var orderRequest = new OrderCreateRequest
                {
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerEmail = model.CustomerEmail,
                    ShippingAddress = model.ShippingAddress,
                    BillingAddress = model.BillingAddress,
                    PaymentMethod = model.PaymentMethod,
                    Notes = model.Notes,
                    ShippingFee = model.ShippingFee,
                    DiscountAmount = 0 // TODO: Implement promotion logic
                };

                // Create order
                var orderId = await _orderService.CreateOrderFromCartAsync(currentUserId, currentSessionId, orderRequest);
                
                TempData["SuccessMessage"] = "Đặt hàng thành công! Chúng tôi sẽ liên hệ với bạn sớm nhất.";
                return RedirectToAction("OrderSuccess", new { id = orderId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                TempData["ErrorMessage"] = ex.Message;
                
                // Reload cart items for the view
                var (userId, sessionId) = GetUserAndSessionId();
                model.CartItems = await _cartService.GetCartItemsAsync(userId, sessionId);
                model.SubTotal = model.CartItems.Sum(item => item.GetSubTotal());
                model.ShippingFee = model.SubTotal >= 700000 ? 0 : 30000; // Free ship logic
                model.TotalAmount = model.SubTotal + model.ShippingFee;
                return View(model);
            }
        }

        public IActionResult Payment()
        {
            return View();
        }

        public async Task<IActionResult> OrderSuccess(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng");
            }

            return View(order);
        }

        private (string? userId, string? sessionId) GetUserAndSessionId()
        {
            string? userId = null;
            string? sessionId = null;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                sessionId = HttpContext.Session.GetString("CartSessionId");
                if (string.IsNullOrEmpty(sessionId))
                {
                    sessionId = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("CartSessionId", sessionId);
                }
            }

            return (userId, sessionId);
        }
    }

    // Request DTOs
    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public Dictionary<string, string>? SelectedVariants { get; set; }
    }

    public class UpdateCartItemRequest
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class RemoveCartItemRequest
    {
        public int CartItemId { get; set; }
    }

    public class MergeCartRequest
    {
        public string SessionId { get; set; }
    }

    public class CheckoutViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100, ErrorMessage = "Họ tên không được quá 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
        public string CustomerPhone { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? CustomerEmail { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        [StringLength(500, ErrorMessage = "Địa chỉ không được quá 500 ký tự")]
        public string ShippingAddress { get; set; } = string.Empty;
        
        public string? BillingAddress { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string PaymentMethod { get; set; } = "COD";
        
        public string? Notes { get; set; }
        
        public decimal SubTotal { get; set; }
        public decimal ShippingFee { get; set; } = 30000;
        public decimal TotalAmount { get; set; }
    }
} 