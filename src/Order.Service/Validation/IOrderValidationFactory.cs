namespace Order.Service.Validation;

public interface IOrderValidationFactory
{
    IOrderValidation Create(string customerType);
}
