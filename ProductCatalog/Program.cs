using ProductCatalog.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    });

}

app.Run();
