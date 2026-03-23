using Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Order order)
    {
        var result = await _orderService.PlaceOrderAsync(order);
        if (!result.Success)
            return BadRequest(result.Message);
        return Ok(result);
    }
}
