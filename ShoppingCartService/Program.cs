using Serilog;
using ShoppingCart.Repository;
using System.Security.Claims;
using UserService.JwtConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication();
builder.Services.AddSingleton<RedisRepository>();
builder.Services.AddSerilog((services, loggConf) => loggConf
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var app = builder.Build();

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
