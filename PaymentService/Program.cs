using MongoDB.Driver;
using PaymentService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(new MongoClient(
    builder.Configuration.GetConnectionString(
        builder.Environment.IsDevelopment() ? "Development" : "DefaultConnection"
    )));
builder.Services.AddSingleton<YookassaService>();
builder.Services.AddSerilog((services, loggConf) => loggConf
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

var app = builder.Build();

app.MapControllers();

app.Run();
