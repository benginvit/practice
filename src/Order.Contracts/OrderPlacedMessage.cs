namespace Order.Contracts;

// NSB-meddelande – ett enkelt POCO som skickas via NServiceBus.
// Separerat från domänmodellen (Order) för att undvika koppling mellan lagern.
public class OrderPlacedMessage
{
    public int OrderId { get; init; }
    public int CustomerId { get; init; }
    public string CustomerEmail { get; init; } = string.Empty;
    public List<string> Items { get; init; } = [];
    public DateTime CreatedAt { get; init; }
}
