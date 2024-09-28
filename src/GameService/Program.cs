using Microsoft.AspNetCore.Authentication.JwtBearer;
using GameService.Repositories.ForGameImage;
using GameService.Repositories.ForCategory;
using GameService.Repositories.ForGame;
using Microsoft.EntityFrameworkCore;
using GameService.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using GameService.Consumers;
using GameService.Services;
using MassTransit;
using GameService.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<GameDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(BaseResponseModel));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IGameImageRepository, GameImageRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddMassTransit(opt =>
{
    opt.AddEntityFrameworkOutbox<GameDbContext>(x =>
    {
        x.QueryDelay = TimeSpan.FromSeconds(10);
        x.UsePostgres();
        x.UseBusOutbox();
    });

    opt.AddConsumersFromNamespaceContaining<GameCreatedFaultConsumer>();
    opt.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("game", false));
    opt.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMQ:Username", "guest"));
            host.Username(builder.Configuration.GetValue("RabbitMQ:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["AuthorirtyServiceUrl"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.NameClaimType = "username";
});

builder.Services.AddGrpc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<GrpcGameService>();
app.MapGrpcService<GrpcMyGameService>();
// app.UseStaticFiles(new StaticFileOptions{
//     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Uploads")),
//     RequestPath = "/Uploads"
// });
app.Run();
