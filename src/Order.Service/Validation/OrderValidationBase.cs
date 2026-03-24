using OrderModel = Order.Domain.Order;

namespace Order.Service.Validation;

public abstract class OrderValidationBase : IOrderValidation
{
    public virtual bool ValidateOrder(OrderModel order)
    {
        if (order == null)
        {
            return false;
        }

        if (order.Items.Count == 0)
        {
            return false;
        }

        return true;
    }
}
