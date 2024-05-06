//Extension methods

using System.Runtime.InteropServices;
using Lab3;

string grupaStudencka = "WSI123";

//mozemy niby wyciagac wartosc po np substringu:

// grupaStudencka.Substring(2, 3);

//ale mozemy zamiast tego zdefiniowac klase StringExtension, 
//ktora musi byc statyczna. Uzywajac wiec takiej metody - nie musimy podawac argumentu dla parametru metody get z rozsz.
grupaStudencka = grupaStudencka.GetStudentGroupNr();
//Mozemy wten sposob rozszerzyc kazda klase. 

//LINQ - Language Integrated Query - podjezyk zdefiniowany dla kolekcji - zeby je odpytywac.
var list = new List<int>() { 3, 4, 5, 3 };
//moge sie bawic w tworzenie petli, warunkow itd, ale moge uzywac juz LINQ:
var evenNumbers = list.Where(e => e % 2 == 0);
Console.Write(evenNumbers);




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}