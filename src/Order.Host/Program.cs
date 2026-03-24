using NServiceBus;
using Order.Infra;
using Order.Infra.Db;
using Order.NSB.Infra;
using Order.Service;
using Order.Service.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOrderFacade, OrderFacade>();
builder.Services.AddScoped<IOrderValidationFactory, OrderValidationFactory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderReportService, OrderReportService>();

// NSB ersätter den in-process EventAggregator.
// Application-lagret märker ingen skillnad – det använder fortfarande IEventAggregator.
builder.Services.AddScoped<IEventAggregator, NsbEventAggregator>();

// Konfigurera NServiceBus-endpoint
// UseNServiceBus är en extension på IHostBuilder, inte WebApplicationBuilder.
builder.Host.UseNServiceBus(_ =>
{
    var endpointConfig = new EndpointConfiguration("OrderService");

    // Learning Transport = ingen extern infrastruktur behövs (bra för träning & dev).
    // Meddelanden sparas som filer i en lokal mapp (.learningtransport).
    var transport = endpointConfig.UseTransport<LearningTransport>();

    // Serialisering med System.Text.Json
    endpointConfig.UseSerialization<SystemJsonSerializer>();

    // Låt NSB hantera sin egen DI-registrering (MessageHandlers hittas automatiskt)
    endpointConfig.EnableInstallers();

    return endpointConfig;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
