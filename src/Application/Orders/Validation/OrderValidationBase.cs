namespace Application.Orders.Validation;

public abstract class OrderValidationBase : IOrderValidation
{
    public virtual bool ValidateOrder(Order order)
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
