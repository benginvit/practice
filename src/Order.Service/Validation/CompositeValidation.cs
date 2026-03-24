using OrderModel = Order.Domain.Order;

namespace Order.Service.Validation;

public class CompositeOrderValidation : IOrderValidation
{
    private readonly IEnumerable<IOrderValidation> _validators;

    public CompositeOrderValidation(IEnumerable<IOrderValidation> validators)
    {
        _validators = validators;
    }

    public bool ValidateOrder(OrderModel order)
    {
        foreach (var val in _validators)
        {
            if (!val.ValidateOrder(order))
                return false;
        }

        return true;

    }
}
