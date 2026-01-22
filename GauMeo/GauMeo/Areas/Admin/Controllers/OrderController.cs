using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GauMeo.Data;
using GauMeo.Models.Orders;
using GauMeo.Services;

namespace GauMeo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // GET: Admin/Order
        public async Task<IActionResult> Index(string status = "", int page = 1, int pageSize = 20)
        {
            try
            {
                ViewData["Title"] = "Quản lý đơn hàng";
                ViewData["CurrentStatus"] = status;
                ViewData["CurrentPage"] = page;

                var orders = await GetOrdersByStatusAsync(status, page, pageSize);
                var statistics = await _orderService.GetOrderStatisticsAsync();

                ViewBag.Statistics = statistics;
                ViewBag.TotalPages = Math.Ceiling((double)statistics.TotalOrders / pageSize);
                
                return View(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading orders");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách đơn hàng";
                return View(new List<Order>());
            }
        }

        // GET: Admin/Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đơn hàng";
                    return RedirectToAction(nameof(Index));
                }

                ViewData["Title"] = $"Chi tiết đơn hàng #{order.OrderNumber}";
                return View(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading order {OrderId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải thông tin đơn hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Admin/Order/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status, string? adminNotes = null)
        {
            try
            {
                var success = await _orderService.UpdateOrderStatusAsync(id, status, adminNotes);
                
                if (success)
                {
                    TempData["SuccessMessage"] = $"Đã cập nhật trạng thái đơn hàng thành: {GetStatusDisplayName(status)}";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể cập nhật trạng thái đơn hàng";
                }
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order {OrderId} status to {Status}", id, status);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật trạng thái đơn hàng";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        // POST: Admin/Order/UpdatePaymentStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePaymentStatus(int id, string paymentStatus)
        {
            try
            {
                var success = await _orderService.UpdatePaymentStatusAsync(id, paymentStatus);
                
                if (success)
                {
                    TempData["SuccessMessage"] = $"Đã cập nhật trạng thái thanh toán thành: {GetPaymentStatusDisplayName(paymentStatus)}";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể cập nhật trạng thái thanh toán";
                }
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order {OrderId} payment status to {PaymentStatus}", id, paymentStatus);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật trạng thái thanh toán";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        // POST: Admin/Order/Cancel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, string cancelReason)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy đơn hàng";
                    return RedirectToAction(nameof(Index));
                }

                // Kiểm tra xem có thể hủy đơn hàng không
                if (!CanCancelOrder(order.Status))
                {
                    TempData["ErrorMessage"] = $"Không thể hủy đơn hàng ở trạng thái '{GetStatusDisplayName(order.Status)}'";
                    return RedirectToAction(nameof(Details), new { id });
                }

                var adminNotes = $"Đơn hàng đã bị hủy bởi Admin. Lý do: {cancelReason}";
                var success = await _orderService.UpdateOrderStatusAsync(id, "Cancelled", adminNotes);
                
                if (success)
                {
                    TempData["SuccessMessage"] = "Đã hủy đơn hàng thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể hủy đơn hàng";
                }
                
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling order {OrderId}", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi hủy đơn hàng";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        // GET: Admin/Order/Statistics
        public async Task<IActionResult> Statistics()
        {
            try
            {
                ViewData["Title"] = "Thống kê đơn hàng";
                var statistics = await _orderService.GetOrderStatisticsAsync();
                return View(statistics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading order statistics");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải thống kê";
                return RedirectToAction(nameof(Index));
            }
        }

        // API endpoint for AJAX calls
        [HttpGet]
        public async Task<JsonResult> GetOrdersJson(string status = "", int page = 1, int pageSize = 10)
        {
            try
            {
                var orders = await GetOrdersByStatusAsync(status, page, pageSize);
                var orderData = orders.Select(o => new
                {
                    id = o.Id,
                    orderNumber = o.OrderNumber,
                    customerName = o.CustomerName,
                    customerPhone = o.CustomerPhone,
                    totalAmount = o.TotalAmount,
                    status = o.Status,
                    statusDisplay = GetStatusDisplayName(o.Status),
                    paymentStatus = o.PaymentStatus,
                    paymentStatusDisplay = GetPaymentStatusDisplayName(o.PaymentStatus),
                    createdAt = o.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    itemCount = o.OrderItems?.Count ?? 0
                });

                return Json(new { success = true, data = orderData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading orders for JSON response");
                return Json(new { success = false, message = "Có lỗi xảy ra" });
            }
        }

        // Helper methods
        private async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status, int page, int pageSize)
        {
            // Lấy trực tiếp từ database qua service, không dùng vòng lặp for
            return await _orderService.GetAllOrdersAsync(
                string.IsNullOrEmpty(status) ? null : status,
                page,
                pageSize
            );
        }

        private async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            // Không còn dùng nữa, xóa code vòng lặp for
            return await _orderService.GetAllOrdersAsync();
        }

        private bool CanCancelOrder(string status)
        {
            // Logic: Chỉ có thể hủy đơn hàng ở trạng thái Pending hoặc Confirmed
            return status switch
            {
                "Pending" or "Confirmed" => true,
                _ => false
            };
        }

        private string GetStatusDisplayName(string status)
        {
            return status switch
            {
                "Pending" => "Chờ xử lý",
                "Confirmed" => "Đã xác nhận",
                "Processing" => "Đang xử lý",
                "Shipping" => "Đang giao hàng",
                "Delivered" => "Đã giao hàng",
                "Cancelled" => "Đã hủy",
                _ => status
            };
        }

        private string GetPaymentStatusDisplayName(string paymentStatus)
        {
            return paymentStatus switch
            {
                "Pending" => "Chờ thanh toán",
                "Paid" => "Đã thanh toán",
                "Failed" => "Thanh toán thất bại",
                "Refunded" => "Đã hoàn tiền",
                _ => paymentStatus
            };
        }
    }
} 