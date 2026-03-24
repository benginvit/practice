using OrderModel = Order.Domain.Order;

namespace Order.Service.Validation;

public interface IOrderValidation
{
    bool ValidateOrder(OrderModel order);
}
