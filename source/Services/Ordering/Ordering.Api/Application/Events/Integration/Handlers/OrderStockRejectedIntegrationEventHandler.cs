﻿namespace Ordering.Api.Application.Events.Integration.Handlers
{
    public class OrderStockRejectedIntegrationEventHandler : IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger<OrderStockRejectedIntegrationEventHandler> logger;

        public OrderStockRejectedIntegrationEventHandler(
            IMediator mediator,
            ILogger<OrderStockRejectedIntegrationEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStockRejectedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                logger.LogInformation("[Ordering] ---> Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

                var orderStockRejectedItems = @event.OrderStockItems.FindAll(c => !c.HasStock).Select(c => c.ProductId).ToList();

                var command = new SetStockRejectedOrderStatusCommand(@event.OrderId, orderStockRejectedItems);

                logger.LogInformation("[Ordering] ---> Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})", command.GetGenericTypeName(), nameof(command.OrderId), command.OrderId, command);

                await mediator.Send(command);
            }
        }
    }
}