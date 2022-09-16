using APIEventos.Core.Interfaces;
using APIEventos.Core.Models;
using APIEventos.Core.Services;
using APIEventos.Filters;
using APIEventos.Infra.Data.Repository;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyCors", policy =>
    {
        policy.WithOrigins("https://localhost:7216");//incluir novas origens caso crie um front
        policy.WithMethods("GET", "POST", "PUT", "DELETE");//incluir novos métodos caso necessário
        policy.AllowAnyHeader();//definir os Headers também caso necessário
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//repositories and services
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();

//filters
builder.Services.AddScoped<CheckReservationQuantityActionFilter>();
builder.Services.AddScoped<CityEventExistsActionFilter>();
builder.Services.AddScoped<CheckMinRangeActionFilter>();
builder.Services.AddScoped<CheckMaxRangeActionFilter>();
builder.Services.AddScoped<CheckNullDateHourEventActionFilter>();
builder.Services.AddScoped<CheckEmptyLocalActionFilter>();
builder.Services.AddScoped<CheckEmptyTitleActionFilter>();



//tentativa
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddMvc(options =>
{
    options.Filters.Add<GeneralExceptionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("PolicyCors");

app.MapControllers();

app.Run();
