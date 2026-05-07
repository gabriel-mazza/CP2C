using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Interfaces;
using ProjectBank.Repositories;
using ProjectBank.Services;

var builder = WebApplication.CreateBuilder(args);

// Dev 1
builder.Services.AddScoped<IAgenciaRepository, AgenciaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

// Dev 2
builder.Services.AddScoped<IMdrService, MdrService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BancoDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleFIAP")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
