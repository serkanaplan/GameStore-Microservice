using FilterService.Consumer;
using FilterService.Extensions;
using FilterService.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddElastic(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IFilterGameService,FilterGameService>();

builder.Services.AddMassTransit(opt => {
    opt.AddConsumersFromNamespaceContaining<GameCreatedFilterConsumer>();
    opt.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search",false));
    opt.UsingRabbitMq((context,cfg) => {
        cfg.Host(builder.Configuration["RabbitMQ:Host"],"/",host => {
            host.Username(builder.Configuration.GetValue("RabbitMQ:Username","guest"));
            host.Username(builder.Configuration.GetValue("RabbitMQ:Password","guest"));
        });
        cfg.ReceiveEndpoint("filter-game-created",e => {
            e.UseMessageRetry(r=>r.Interval(5,5));
            e.ConfigureConsumer<GameCreatedFilterConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
