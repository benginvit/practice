namespace Order.Contracts;

public record PlaceOrderResult(
    bool Success,
    string Message
);
