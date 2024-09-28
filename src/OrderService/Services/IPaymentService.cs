using OrderService.Models;

namespace OrderService.Services;

public interface IPaymentService
{
    Task<BaseResponse> PayMyGames(PaymentForm model);
}