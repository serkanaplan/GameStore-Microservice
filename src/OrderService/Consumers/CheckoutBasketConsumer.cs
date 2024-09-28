using AutoMapper;
using Contracts;
using MassTransit;
using OrderService.Data;
using OrderService.Entities;

namespace OrderService.Consumers;
public class CheckoutBasketConsumer(IMapper mapper, ApplicationDbContext context) : IConsumer<CheckoutBasketModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly ApplicationDbContext _context = context;

    public async Task Consume(ConsumeContext<CheckoutBasketModel> context)
    {
        Console.WriteLine("Checout basket consuming with order");

        var item = _mapper.Map<Order>(context.Message);
        await _context.Orders.AddAsync(item);
        await _context.SaveChangesAsync();
    }
}