using System.Security.Claims;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Entities;
using OrderService.Models;
using OrderService.Services.GrpcFolder;

namespace OrderService.Services
{
    public class PaymentService(IConfiguration _configuration, GrpcMyGameClient myGameClient, ApplicationDbContext context, IHttpContextAccessor contextAccessor) : IPaymentService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly string UserId = contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        private readonly GrpcMyGameClient _myGameClient = myGameClient;


        public async Task<BaseResponse> PayMyGames(PaymentForm model)
        {
            BaseResponse baseResponse = new();
            var result = await GetOrderByUserId(UserId);
            decimal price = 0;
            foreach (var item in result) price += item.Price;

            Options options = new()
            {
                ApiKey = Key.ApiKey,
                SecretKey = Key.SecretKey,
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };

            CreatePaymentRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Price = price.ToString(),
                PaidPrice = price.ToString(),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "B67832",
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };

            PaymentCard paymentCard = new()
            {
                CardHolderName = model.CardHolderName,
                CardNumber = model.CardNumber,
                ExpireMonth = model.ExpireMonth,
                ExpireYear = model.ExpireYear,
                Cvc = model.Cvc,
                RegisterCard = 0
            };
            request.PaymentCard = paymentCard;

            Buyer buyer = new()
            {
                Id = "BY789",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = "email@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = "2015-10-05 12:43:35",
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };
            request.Buyer = buyer;

            Address shippingAddress = new()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = [];
            foreach (var item in result)
            {
                BasketItem firstBasketItem = new()
                {
                    Id = item.GameId.ToString(),
                    Name = item.GameName,
                    Category1 = "Games",
                    Category2 = "Games",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = item.Price.ToString()
                };
                basketItems.Add(firstBasketItem);
            }


            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
            if (payment.Status == "success")
            {
                foreach (var item in result)
                {
                    var isPaid = await PaidGameOrder(result);
                    Console.WriteLine(UserId);
                    Console.WriteLine(item.GameId);
                    var checkResult = _myGameClient.SaveMyGame(UserId, item.GameId.ToString());
                    if (!checkResult || !isPaid)
                    {
                        baseResponse.IsSuccess = false;
                        return baseResponse;
                    }
                }
                baseResponse.IsSuccess = true;
                return baseResponse;

            }
            baseResponse.Message = payment.ErrorMessage;
            baseResponse.IsSuccess = false;
            return baseResponse;
        }

        private async Task<List<Order>> GetOrderByUserId(string userId)
        => await _context.Orders.Where(x => x.UserId == Guid.Parse(userId) && !x.IsPaid).ToListAsync();

        private async Task<bool> PaidGameOrder(List<Order> orders)
        {
            foreach (var item in orders)
            {
                var result = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == item.OrderId);
                result.IsPaid = true;
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}