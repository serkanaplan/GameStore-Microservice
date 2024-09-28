using DiscountService.Models;
using DiscountService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscountService.Controllers;


[ApiController]
[Route("[controller]")]
public class DiscountController(IDiscountRepository repository) : ControllerBase
{

    private readonly IDiscountRepository _repository = repository;

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateDiscount(DiscountModel model)
    {
        var response = await _repository.CreateDiscount(model);
        return Ok(response);
    }
}