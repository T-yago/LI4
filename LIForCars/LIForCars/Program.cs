using LIForCars.Data;
using LIForCars.Data.Components;
using LIForCars.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MyLIForCarsDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyLIForCarsDBConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<ICoworkerRepository, CoworkerRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();