namespace Application.Orders.Validation;

public interface IOrderValidationFactory
{
    IOrderValidation Create(string customerType);
}
