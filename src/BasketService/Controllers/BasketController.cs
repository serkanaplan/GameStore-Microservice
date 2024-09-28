using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BasketService.Repository;
using BasketService.Model;

namespace BasketService.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController(IBasketRepository basketRepository) : ControllerBase
{
    private readonly IBasketRepository _basketRepository = basketRepository;


    [HttpPost]
    [Authorize]
    public async Task<ActionResult> AddBasketItem(BasketModel model) => Ok(await _basketRepository.AddBasket(model));


    [HttpGet("BasketItems")]
    [Authorize]
    public async Task<ActionResult> GetListItems() => Ok(await _basketRepository.GetBasketItems());


    [HttpGet("BasketItem/{index}")]
    [Authorize]
    public async Task<ActionResult> GetBasketItem([FromRoute] long index) => Ok(await _basketRepository.GetBasketItem(index));


    [HttpDelete("{index}")]
    [Authorize]
    public async Task<ActionResult> RemoveBasketItem([FromRoute] long index)
    => Ok(await _basketRepository.RemoveBasketItem(index));


    [HttpPost("Checkout")]
    [Authorize]
    public async Task<ActionResult> Checkout() => Ok(await _basketRepository.Checkout());


    [HttpPut]
    [Authorize]
    public async Task<ActionResult> CouponCodeImplement(long index, string couponCode)
    => Ok(await _basketRepository.ImplementCoupon(index, couponCode));
}