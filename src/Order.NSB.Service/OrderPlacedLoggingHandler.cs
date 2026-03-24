using Microsoft.Extensions.Logging;
using NServiceBus;
using Order.Contracts;

namespace Order.NSB.Service;

// NSB MessageHandler – hanterar ett enskilt OrderPlacedMessage inom en transaktion.
// Enligt PDF:en: NSB-Event hanterar alltid en enskild uppgift och körs i en transaktion.
// Håll exekveringstid under 100 ms.
public class OrderPlacedLoggingHandler : IHandleMessages<OrderPlacedMessage>
{
    private readonly ILogger<OrderPlacedLoggingHandler> _logger;

    public OrderPlacedLoggingHandler(ILogger<OrderPlacedLoggingHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderPlacedMessage message, IMessageHandlerContext context)
    {
        _logger.LogInformation(
            "Order mottagen via NSB: OrderId={OrderId}, Kund={CustomerEmail}, Antal artiklar={Count}",
            message.OrderId,
            message.CustomerEmail,
            message.Items.Count);

        // Här kan vi t.ex. skicka e-post, uppdatera en read-model, etc.
        // Varje handler har ett enda ansvar (Single Responsibility).

        return Task.CompletedTask;
    }
}
