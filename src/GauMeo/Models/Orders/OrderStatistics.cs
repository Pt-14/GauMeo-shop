namespace GauMeo.Models.Orders
{
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
        public decimal PendingRevenue { get; set; }
        public decimal CompletedRevenue { get; set; }
        
        public int TotalPaidOrders { get; set; }
        public int PendingPaymentOrders { get; set; }
        public int FailedPaymentOrders { get; set; }
        
        public DateTime? LastOrderDate { get; set; }
        public decimal AverageOrderValue { get; set; }
    }
} 