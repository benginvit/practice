namespace Application.Orders;

public interface IOrderInvoiceService
{
    Task GenerateOrderInvoiceAsync(int orderId);
}
