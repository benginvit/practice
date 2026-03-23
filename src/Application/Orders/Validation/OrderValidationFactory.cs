namespace Application.Orders.Validation;

public class OrderValidationFactory : IOrderValidationFactory
{
    public virtual IOrderValidation Create(string customerType)
    {
        return customerType switch
        {
            "Premium" => new PremiumCustomerValidation(),
            _ => new StandardCustomerValidation()
        };
    }
}
