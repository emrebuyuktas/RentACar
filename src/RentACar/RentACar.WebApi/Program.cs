using Core.CrossCuttingConcerns.Exceptions.Extensions;
using RentACar.Application;
using RentACar.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

#region Externel Services
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379");
builder.Services.AddHttpContextAccessor();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsProduction()) app.UseCustomExceptionHandler();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
