namespace Application.Orders.Validation;

public class PremiumCustomerValidation : OrderValidationBase
{
    public override bool ValidateOrder(Order order)
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
