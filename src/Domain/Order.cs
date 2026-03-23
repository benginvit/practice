
public record Order(
    int Id,
    int CustomerId,
    string CustomerEmail,
    List<string> Items,
    string CustomerType = "Standard"
);