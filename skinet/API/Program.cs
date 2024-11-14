using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Services are something which inject to the other classes of our application
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Registering the service Product Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();        


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

//Seeding data to perform migration creating the database in sql server by using the json data
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}
app.Run();
