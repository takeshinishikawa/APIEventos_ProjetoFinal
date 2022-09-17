using APIEventos.Core.Interfaces;
using APIEventos.Core.Services;
using APIEventos.Filters;
using APIEventos.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//limpa os locais pré-configurados para logar
builder.Logging.ClearProviders();
//configura para logar apenas no Console
builder.Logging.AddConsole();

/*builder.Services.AddHttpClient("APIEvents", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["local"]);
});

builder.Services.AddHttpClient("APIReservation", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["local"]);
});*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyCors", policy =>
    {
        policy.WithOrigins(builder.Configuration["local"]);//incluir novas origens caso crie um front
        policy.WithMethods("GET", "POST", "PUT", "DELETE");//incluir novos métodos caso necessário
        policy.AllowAnyHeader();//definir os Headers também caso necessário
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Add JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["key"]);
var issuer = builder.Configuration["Issuer"];
var audience = builder.Configuration["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//include the a Bearer insert option for Swagger testing
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put *ONLY* your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

//repositories and services
builder.Services.AddScoped<ITokenService, TokenService>();
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
builder.Services.AddScoped<CheckNullDateHourObjEventActionFilter>();
builder.Services.AddScoped<CheckEmptyLocalActionFilter>();
builder.Services.AddScoped<CheckEmptyTitleActionFilter>();

//tentativa
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

builder.Services.AddMvc(options => options.Filters.Add<GeneralExceptionFilter>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseAuthentication().UseAuthorization();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseCors("PolicyCors");

app.MapControllers();

app.Run();
