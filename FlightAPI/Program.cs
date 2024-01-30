using FlightProjApi.Models;
using FlightProjApi.Repository;
using FlightProjApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Ace52024Context>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFlightRepo<SanskritiFlight>,FlightRepo>();
builder.Services.AddScoped<IFlightServ<SanskritiFlight>,FlightServ>();
builder.Services.AddScoped<ILoginServ<SanskritiLoginDetail>,LoginServ>();
builder.Services.AddScoped<ILoginRepo<SanskritiLoginDetail>,LoginRepo>();
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