using NServiceBus;
using Order.Contracts;
using Order.Service.Events;

namespace Order.NSB.Infra;

// NSB-implementation av IEventAggregator.
// Application-lagret känner bara till IEventAggregator – inte NServiceBus.
// Vi byter ut den in-process EventAggregator mot denna som publiserar via NSB.
public class NsbEventAggregator : Order.Domain.IEventAggregator
{
    private readonly IMessageSession _messageSession;

    public NsbEventAggregator(IMessageSession messageSession)
    {
        _messageSession = messageSession;
    }

    public async Task PublishAsync<T>(T @event) where T : Order.Domain.IEvent
    {
        // Mappa domän-event till NSB-meddelande
        if (@event is OrderPlacedEvent orderPlaced)
        {
            var message = new OrderPlacedMessage
            {
                OrderId    = orderPlaced.Order.Id,
                CustomerId = orderPlaced.Order.CustomerId,
                CustomerEmail = orderPlaced.Order.CustomerEmail,
                Items      = orderPlaced.Order.Items,
                CreatedAt  = orderPlaced.Order.CreatedAt ?? DateTime.UtcNow
            };

            await _messageSession.Publish(message);
        }
    }
}
