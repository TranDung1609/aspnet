using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using testapi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ??ng ký MongoDB client v?i c?u hình t? appsettings.json
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration["AdsMongoDbContext:ConnectionString"];
    return new MongoClient(connectionString);
});

// ??ng ký AdsMongoDbContext mà không c?n thay ??i file AdsMongoDbContext
builder.Services.AddSingleton<AdsMongoDbContext>();

// ??ng ký các d?ch v? khác
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// C?u hình HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
