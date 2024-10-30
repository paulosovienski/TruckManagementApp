using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TruckManagement.Domain.Interfaces;
using TruckManagement.Domain.Services;
using TruckManagement.Infrastructure;
using TruckManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var checkDb = builder.Configuration.GetConnectionString("TruckDatabase");
builder.Services.AddDbContext<TruckManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TruckDatabase")));
builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ITruckService, TruckService>();

var origins = builder.Configuration.GetSection("OriginsCors").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins(origins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .SetIsOriginAllowed((host) => true));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TruckManagementDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();
