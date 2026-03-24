namespace Order.Specs;

using Moq;
using Order.Domain;
using Order.Service;
using Order.Service.Validation;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _repoMock = new();
    private readonly Mock<IEventAggregator> _eventAggregatorMock = new();

    private OrderService CreateService(bool validationResult)
    {
        var validationMock = new Mock<IOrderValidation>();
        validationMock.Setup(v => v.ValidateOrder(It.IsAny<Order>())).Returns(validationResult);
        var factory = new Mock<IOrderValidationFactory>();
        factory.Setup(f => f.Create(It.IsAny<string>())).Returns(validationMock.Object);
        return new OrderService(_repoMock.Object, factory.Object, _eventAggregatorMock.Object);
    }

    [Fact]
    public async Task PlaceOrderAsync_ValidOrder_ReturnsSuccess()
    {
        // Arrange
        var service = CreateService(true);
        var order = new Order(1, 123, "test@test.com", ["Item1"]);

        // Act
        var result = await service.PlaceOrderAsync(order);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task PlaceOrderAsync_InvalidOrder_ReturnsFailure()
    {
        // Arrange
        var service = CreateService(false);
        var order = new Order(1, 123, "test@test.com", ["Item1"]);

        // Act
        var result = await service.PlaceOrderAsync(order);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task ValidatePremium_MoreThen100_ShouldReturnFalse()
    {
        // Arrange
        var val = new PremiumCustomerValidation();
        var items = Enumerable.Range(1, 100).Select(i => $"Item{i}").ToList();
        var order = new Order(1, 123, "test@test.com", items);

        // Act
        var result = val.ValidateOrder(order);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateGetOrder_idLessThanZero_ShouldThrow()
    {
        // Arrange
        var service = CreateService(true);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.GetOrderByIdAsync(-1));
    }

    [Fact]
    public async Task ValidatePremium_LessThen100_ShouldReturnTrue()
    {
        // Arrange
        var val = new PremiumCustomerValidation();
        var items = Enumerable.Range(1, 99).Select(i => $"Item{i}").ToList();
        var order = new Order(1, 123, "test@test.com", items);

        // Act
        var result = val.ValidateOrder(order);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public void Factory_PremiumType_ReturnsPremiumValidation()
    {
        var factory = new OrderValidationFactory();
        var result = factory.Create("Premium");
        Assert.IsType<PremiumCustomerValidation>(result);
    }

    [Fact]
    public void CompositeValidation_AllValidationsPass_ReturnsTrue()
    {
        // Arrange
        var validationMock = new Mock<IOrderValidation>();
        validationMock.Setup(v => v.ValidateOrder(It.IsAny<Order>())).Returns(true);
        var validationMock2 = new Mock<IOrderValidation>();
        validationMock2.Setup(v => v.ValidateOrder(It.IsAny<Order>())).Returns(true);

        var composite = new CompositeOrderValidation(new[] { validationMock.Object, validationMock2.Object });
        Assert.True(composite.ValidateOrder(new Order(1, 123, "test@test.com", [])));
    }

    [Fact]
    public void CompositeValidation_SomeValidationsFail_ReturnsFalse()
    {
        // Arrange
        var validationMock = new Mock<IOrderValidation>();
        validationMock.Setup(v => v.ValidateOrder(It.IsAny<Order>())).Returns(true);
        var validationMock2 = new Mock<IOrderValidation>();
        validationMock2.Setup(v => v.ValidateOrder(It.IsAny<Order>())).Returns(false);

        var composite = new CompositeOrderValidation(new[] { validationMock.Object, validationMock2.Object });
        Assert.False(composite.ValidateOrder(new Order(1, 123, "test@test.com", [])));
    }
}
