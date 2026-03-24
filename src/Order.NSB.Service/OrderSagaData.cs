using NServiceBus;

namespace Order.NSB.Service;

public class OrderSagaData : ContainSagaData
{
    public int OrderId { get; set; }
    public bool IsPaid { get; set; }
}