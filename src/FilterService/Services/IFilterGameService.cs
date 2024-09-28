using FilterService.Models;

namespace FilterService.Services;

public interface IFilterGameService
{
    Task<List<GameFilterItem>> SearchAsync(GameFilterItem gameFilterItem);
}