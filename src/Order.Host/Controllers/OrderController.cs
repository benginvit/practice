using Microsoft.AspNetCore.Mvc;
using Order.Domain;
using OrderModel = Order.Domain.Order;

namespace Order.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderFacade _orderFacade;

    public OrderController(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order = await _orderFacade.GetOrderByIdAsync(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(OrderModel order)
    {
        var result = await _orderFacade.PlaceOrderAsync(order);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result);
    }

    [HttpGet("{orderId}/summary")]
    public async Task<IActionResult> GetOrderSummaryAsync(int orderId)
    {
        var ordersummary = await _orderFacade.GetOrderSummaryAsync(orderId);
        return Ok(ordersummary);
    }
}
