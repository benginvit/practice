using Application.Common;
using Application.Events;
using Application.Orders;
using Application.Orders.Events;
using Application.Orders.Validation;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOrderValidationFactory, OrderValidationFactory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderReportService, OrderReportService>();
builder.Services.AddScoped<IEventAggregator, EventAggregator>();
builder.Services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedEmailHandler>();
builder.Services.AddScoped<IEventHandler<OrderPlacedEvent>, OrderPlacedLogHandler>();

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
