using Basket.Application.Commands;
using Basket.Application.GRPCServices;
using Basket.Application.Mappers;
using Basket.Core.Repositaries;
using Basket.Infrastructure.Repositaries;
using Common.Logging;
using Discount.GRPC.Protos;
using MassTransit;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BasketMappingProfile).Assembly);

builder.Services.AddMediatR(
    cnf => cnf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
                                              Assembly.GetAssembly(typeof(CreateShoppingCartCommand))));

builder.Services.AddScoped<IBasketRepo, BasketRepo>();
builder.Services.AddScoped<DiscountGRPCServices>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});


builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddApiVersioning(option =>
{
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});
// redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
