using Order.Contracts;

namespace Order.NSB.Service;

public class OrderPaymentSaga : Saga<OrderSagaData>,
    IAmStartedByMessages<OrderPlacedMessage>,
    IHandleMessages<PaymentReceivedMessage>,
    IHandleTimeouts<OrderPaymentSaga.PaymentTimeout>
{
    public Task Handle(PaymentReceivedMessage message, IMessageHandlerContext context)
    {
        Data.IsPaid = true;
        MarkAsComplete();
        return Task.CompletedTask;
    }

    public Task Handle(OrderPlacedMessage message, IMessageHandlerContext context)
    {
        Data.IsPaid = false;
        Data.OrderId = message.OrderId;
        return Task.CompletedTask;
    }

    public async Task Timeout(PaymentTimeout state, IMessageHandlerContext context)
    {
        await RequestTimeout(context, TimeSpan.FromHours(24), new PaymentTimeout());
        MarkAsComplete();
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSagaData> mapper)
    {
        mapper.MapSaga(saga => saga.OrderId)
            .ToMessage<PaymentReceivedMessage>(message => message.OrderId)
            .ToMessage<OrderPlacedMessage>(msg => msg.OrderId);

    }
    public class PaymentTimeout { }
}

