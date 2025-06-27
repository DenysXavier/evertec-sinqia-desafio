using API.Infrastructure;
using API.Infrastructure.Impl;
using API.Services;
using API.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(config =>
{
    config.UseUtcTimestamp = true;
});

// Db Context
builder.Services.AddDbContext<CalculadoraDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services
    .AddScoped<ICotacaoRepository, CotacaoRepository>()
    .AddScoped<ICotacaoService, CotacaoService>()
    .AddScoped<IInvestimentoPosFixadoService, InvestimentoPosFixadoService>();

builder.Services.AddControllers();
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
