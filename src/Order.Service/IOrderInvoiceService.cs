namespace Order.Service;

public interface IOrderInvoiceService
{
    Task GenerateOrderInvoiceAsync(int orderId);
}
