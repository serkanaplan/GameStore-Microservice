using DiscountService.Models;

namespace DiscountService.Repository;

public interface IDiscountRepository
{
    Task<bool> CreateDiscount(DiscountModel model);
}