using GauMeo.Models.Orders;

namespace GauMeo.Services
{
    public interface IOrderService
    {
        // Create order from cart
        Task<int> CreateOrderFromCartAsync(string? userId, string? sessionId, OrderCreateRequest request);
        
        // Get order by ID
        Task<Order?> GetOrderByIdAsync(int orderId);
        
        // Get order by order number
        Task<Order?> GetOrderByNumberAsync(string orderNumber);
        
        // Get orders by user
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId, int page = 1, int pageSize = 10);
        
        // Update order status
        Task<bool> UpdateOrderStatusAsync(int orderId, string status, string? adminNotes = null);
        
        // Update payment status
        Task<bool> UpdatePaymentStatusAsync(int orderId, string paymentStatus);
        
        // Get order statistics
        Task<OrderStatistics> GetOrderStatisticsAsync();
        
        // Validate order data
        bool ValidateOrderRequest(OrderCreateRequest request, out string errorMessage);
        
        // Get all orders for admin (with optional status filter and pagination)
        Task<List<Order>> GetAllOrdersAsync(string? status = null, int page = 1, int pageSize = 20);
    }
    
    public class OrderCreateRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string? BillingAddress { get; set; }
        public string PaymentMethod { get; set; } = "COD"; // "COD", "BankTransfer", "EWallet"
        public string? Notes { get; set; }
        public decimal ShippingFee { get; set; } = 0;
        public int? PromotionId { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
    }
    
    public class OrderStatistics
    {
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ConfirmedOrders { get; set; }
        public int ProcessingOrders { get; set; }
        public int ShippingOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int CancelledOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal MonthRevenue { get; set; }
    }
} 