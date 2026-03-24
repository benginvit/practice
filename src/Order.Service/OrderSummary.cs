using OrderModel = Order.Domain.Order;

namespace Order.Service;

public record OrderSummary(OrderModel Order, List<OrderModel> History);
