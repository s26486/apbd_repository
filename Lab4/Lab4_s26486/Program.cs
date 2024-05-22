using Lab4_s26486.Inventory;
using Lab4_s26486.Services; 
using Lab4_s26486.Repository; 

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddSingleton<IOrderRepository, OrderRepository>(); 
        builder.Services.AddSingleton<OrderService>(); 

        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<ProductService>();
        
        builder.Services.AddSingleton<IWarehouseService, WarehouseService>();
        builder.Services.AddSingleton<WarehouseService>();

        builder.Services.AddSingleton<IWarehouseService, WarehouseService>();
        
        builder.Services.AddSingleton<ProductService>();
        builder.Services.AddSingleton<InventoryService>();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}