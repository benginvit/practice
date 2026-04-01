using OrderModel = Order.Domain.Order;

namespace Order.Domain;

public record OrderSummary(OrderModel Order, List<OrderModel> History);
