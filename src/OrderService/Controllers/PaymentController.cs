using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers;


[ApiController]
[Route("[controller]")]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;


    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreatePay(PaymentForm paymentModel) => Ok(await _paymentService.PayMyGames(paymentModel));
}