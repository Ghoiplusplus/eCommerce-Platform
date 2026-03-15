using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using UserService.JwtConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication();
builder.Services.AddDbContext<OrderContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(builder.Environment.IsDevelopment() ? "Development" : "DefaultConnection"));
});

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
