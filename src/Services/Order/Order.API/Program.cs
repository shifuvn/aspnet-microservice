using Order.API.ExtensionMethods;
using Order.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataContext<OrderDataContext>(builder.Configuration);

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddEventBus(builder.Configuration);


var app = builder.Build();
app.MigrateDatabase<OrderDataContext>((context, action) => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();