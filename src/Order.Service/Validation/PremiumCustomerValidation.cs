using OrderModel = Order.Domain.Order;

namespace Order.Service.Validation;

public class PremiumCustomerValidation : OrderValidationBase
{
    public override bool ValidateOrder(OrderModel order)
    {
        if (!base.ValidateOrder(order))
        {
            return false;
        }

        if (order.Items.Count >= 100)
        {
            return false;
        }
        return true;
    }
}
