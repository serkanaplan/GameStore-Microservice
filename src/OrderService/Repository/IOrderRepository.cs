using OrderService.Entities;

namespace OrderService.Repository;

public interface IOrderRepository
{
    Task<List<Order>> GetOrderByUserId();
}