namespace Application.Orders.Validation;

public interface IOrderValidation
{
    bool ValidateOrder(Order order);
}
