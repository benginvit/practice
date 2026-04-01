using Microsoft.Extensions.Logging;
using NServiceBus;
using Order.Contracts;

namespace Order.NSB.Service;

// NSB MessageHandler – hanterar ett enskilt OrderPlacedMessage inom en transaktion.
// Enligt PDF:en: NSB-Event hanterar alltid en enskild uppgift och körs i en transaktion.
// Håll exekveringstid under 100 ms.
public class OrderPlacedEmailHandler : IHandleMessages<OrderPlacedMessage>
{
    private readonly ILogger<OrderPlacedEmailHandler> _logger;
    private readonly Order.Domain.IEmailService _emailService;

    public OrderPlacedEmailHandler(ILogger<OrderPlacedEmailHandler> logger, Order.Domain.IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Handle(OrderPlacedMessage message, IMessageHandlerContext context)
    {
        await _emailService.SendEmailAsync(
            to: message.CustomerEmail,
            subject: $"Orderbekräftelse - Order #{message.OrderId}",
            body: $"Tack för din beställning! Din order innehåller {message.Items.Count} artiklar.");
    }
}
