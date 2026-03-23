using Application.Common;
using Application.Events;
using Application.Orders.Events;

namespace Infrastructure;

public class OrderPlacedEmailHandler : IEventHandler<OrderPlacedEvent>
{
    private readonly IEmailService _emailService;

    public OrderPlacedEmailHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task HandleAsync(OrderPlacedEvent @event)
    {
        await _emailService.SendEmailAsync(
            @event.Order.CustomerEmail,
            "Order Placed",
            $"Your order with ID {@event.Order.Id} has been placed successfully.");
    }
}
