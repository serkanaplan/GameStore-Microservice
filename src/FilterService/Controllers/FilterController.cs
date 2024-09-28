using FilterService.Models;
using FilterService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilterService.Controllers;


[ApiController]
[Route("[controller]")]
public class FilterController(IFilterGameService filterService) : ControllerBase
{
    private readonly IFilterGameService _filterService = filterService;


    [HttpPost]
    public async Task<ActionResult> FilterGameServ(GameFilterItem filterItem) 
    => Ok(await _filterService.SearchAsync(filterItem));
}