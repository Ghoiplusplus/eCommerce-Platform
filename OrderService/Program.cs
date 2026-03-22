using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderService.Data;
using System.Security.Claims;
using UserService.JwtConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication();
builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(
        builder.Environment.IsDevelopment() ? "Development" : "DefaultConnection"));
});

var app = builder.Build();

// Проверка на отсутствия userID
app.Use(async (context, next) =>
{
    string? userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId.IsNullOrEmpty())
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
