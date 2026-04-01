namespace Order.Domain;

public interface IOrderInvoiceService
{
    Task GenerateOrderInvoiceAsync(int orderId);
}
