using OptionStrikes.Entities;
using OptionStrikes.Helpers;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var http = new HttpClient();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/expirationTicks/{symbol:alpha}", async (string symbol) =>
{
    var request = new YahooOptionsRequest(symbol);
    var yahooResponse = await http.GetFromJsonAsync<YahooOptionsResponse>(request.Url);
    return yahooResponse?.OptionChain?.Result?[0].ExpirationTicks;
}).WithName("GetExpirationTicks");


app.MapGet("/expirationDates/{symbol:alpha}", async (string symbol) =>
{
    var request = new YahooOptionsRequest(symbol);
    var yahooResponse = await http.GetFromJsonAsync<YahooOptionsResponse>(request.Url);
    return yahooResponse?.OptionChain?.Result?[0].ExpirationDates;
}).WithName("GetExpirationDates");

app.MapGet("strikes/{symbol:alpha}", async (string symbol) =>
{
    var request = new YahooOptionsRequest(symbol);
    var yahooResponse = await http.GetFromJsonAsync<YahooOptionsResponse>(request.Url);
    return yahooResponse?.OptionChain?.Result?[0].Strikes;
}).WithName("GetStrikes");

app.MapGet("openInterest/{symbol:alpha}/{ticks:long}", async (string symbol, long ticks) => {
    var request = new YahooOptionsRequest(symbol);
    var yahooResponse = await http.GetFromJsonAsync<YahooOptionsResponse>(request.Url);
    var expirations = yahooResponse?.OptionChain?.Result?[0].ExpirationTicks.Where(tick => UnixTimeHelper.ToDateTime(tick) < DateTime.Now.AddDays(60));
    return 2;
});

app.Run();

