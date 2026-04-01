namespace Order.Domain;

public record PlaceOrderResult(
    bool Success,
    string Message
);
