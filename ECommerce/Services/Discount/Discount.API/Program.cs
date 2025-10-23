using Common.Logging;
using Discount.API.Services;
using Discount.Application.Mapper;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Infrastructure.Extenstions;
using Discount.Infrastructure.Repositories;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// mapping Configuration 
builder.Services.AddAutoMapper(typeof(DiscountProfile).Assembly);

builder.Services.AddMediatR(
    cnf => cnf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
                                              Assembly.GetAssembly(typeof(GetDiscountQuery))));

builder.Services.AddScoped<IDiscountRepo, DiscountRepo>();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MigrateDataBase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountServices>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communcation with grpc endpoints must be with grpc clients");
    });
});

app.Run();
