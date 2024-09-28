using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrderService.Services.GrpcFolder;
using Microsoft.EntityFrameworkCore;
using OrderService.Repository;
using OrderService.Consumers;
using OrderService.Services;
using OrderService.Models;
using OrderService.Data;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(BaseResponse));
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["AuthorirtyServiceUrl"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.NameClaimType = "username";
});

builder.Services.AddMassTransit(opt =>
{
    opt.AddConsumersFromNamespaceContaining<CheckoutBasketConsumer>();
    opt.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
    opt.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
            host.Username(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
        });

        cfg.ReceiveEndpoint("order-created", e =>
        {
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<CheckoutBasketConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<GrpcMyGameClient>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
