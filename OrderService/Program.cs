using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(
        builder.Environment.IsDevelopment() ? "Development" : "DefaultConnection"));
});

builder.Services.AddSerilog((services, loggConf) => loggConf
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    });
}

app.Run();
