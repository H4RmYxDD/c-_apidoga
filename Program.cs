using apidoga;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FlowerShopDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=viragbolt;Trusted_Connection=True;MultipleActiveResultSets=true")); 

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapGet("/getall", (FlowerShopDbContext dbContext ) => dbContext.Orders.ToList());
app.MapGet("/getone/{id}", (FlowerShopDbContext dbContext, int id) => dbContext.Orders.Where(x => x.Id.Equals(id)).FirstOrDefault());
app.MapPost("/new-order", (FlowerShopDbContext dbContext, Order order)=> {
    dbContext.Orders.Add(order);
    dbContext.SaveChanges();
    }
);
app.MapPut("/edit-order/{id}", (FlowerShopDbContext dbContext, int id, Order order)=> dbContext.Orders.Where(x=>x.Id.Equals(id)).ExecuteUpdate(setters=>
setters.SetProperty(e=>e.ProductName, order.ProductName)
.SetProperty(e=>e.ProductType, order.ProductType)
.SetProperty(e=>e.Quantity, order.Quantity)
.SetProperty(e=>e.Price, order.Price)
.SetProperty(e=>e.Created, order.Created)));
app.MapDelete("/delete/{id}", (FlowerShopDbContext dbContext, int id)=>dbContext.Orders.Where(x=>x.Id.Equals(id)).ExecuteDelete());
app.MapGet("/most-sold", (FlowerShopDbContext dbContext) =>
{
    var napok = dbContext.Orders
    .GroupBy(x => x.Created.Date).Select(g => new
    {
        Created = g.Key,
        Price = g.Sum(a=>a.Price*a.Quantity)
    }).OrderByDescending(b=>b.Price).FirstOrDefault();
    return napok;
});

app.Run();