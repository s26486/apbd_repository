using Lab4.Controllers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your WarehouseController
builder.Services.AddSingleton<WarehouseController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Replace the weather forecast endpoint with your WarehouseController endpoint
app.MapPost("/addInventory", (WarehouseController controller, InventoryRequest inventoryRequest) =>
    {
        // Call the AddProductToWarehouse method of your WarehouseController
        IActionResult result = controller.AddProductToWarehouse(inventoryRequest);
        return result;
    })
    .WithName("AddInventory")
    .WithOpenApi();

app.Run();