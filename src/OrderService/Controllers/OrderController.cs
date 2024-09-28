using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Repository;

namespace OrderService.Controllers;


[ApiController]
[Route("[controller]")]
public class OrderController(IOrderRepository orderRepository) : ControllerBase
{
    private readonly IOrderRepository _orderRepository = orderRepository;


    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetMyOrder() => Ok(await _orderRepository.GetOrderByUserId());
}