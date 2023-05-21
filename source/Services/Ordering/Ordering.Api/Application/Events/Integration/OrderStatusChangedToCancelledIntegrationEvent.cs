namespace Ordering.Api.Application.Events.Integration
{
    public record OrderStatusChangedToCancelledIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedToCancelledIntegrationEvent(int orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }

        public int OrderId { get; }
        public string OrderStatus { get; }
        public string BuyerName { get; }
    }
}