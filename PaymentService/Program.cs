using MongoDB.Driver;
using PaymentService.Services;
using Serilog;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(new MongoClient(
    builder.Environment.IsDevelopment() ? "Development" : "DefaultConnection"
    ));
builder.Services.AddSingleton<YookassaService>();
builder.Services.AddSerilog((services, loggConf) => loggConf
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var app = builder.Build();

app.MapControllers();

// TODO: перенести добавление id к запросу в api gateway
app.Use(async (context, next) =>
{
    string? userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userId))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("No GUID (middleware)");
    }
    else
    {
        context.Items["UserId"] = userId;
        await next.Invoke(context);
    }
});

app.Run();
