namespace Ordering.Api.Application.Events.Domain
{
    public class OrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly IBuyerRepository repository;
        private readonly ILogger<OrderStartedDomainEventHandler> logger;

        public OrderStartedDomainEventHandler(IBuyerRepository repository, ILogger<OrderStartedDomainEventHandler> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            var cardTypeId = notification.CardTypeId != 0 ? notification.CardTypeId : 1;

            var buyer = await repository.GetByIdentityAsync(notification.UserId);
            var buyerInContext = buyer is not null;

            if (buyer is null)
            {
                buyer = new Buyer(notification.UserId, notification.UserName);
            }

            buyer.VerifyOrAddPaymentMethod(DateTime.UtcNow.ToString(), notification.CardNumber,
                notification.CardSecurityNumber, notification.CardHolderName,
                notification.CardExpiration, cardTypeId, notification.Order.Id);

            buyer = buyerInContext ? repository.Update(buyer) : repository.Add(buyer);

            await repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            logger.LogInformation("[Ordering] ---> Buyer {BuyerName} and related payment method were validated or updated for order: {OrderId}",
                buyer.Id, notification.Order.Id);
        }
    }
}