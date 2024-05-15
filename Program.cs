using Tennis.API.Filters;
using Tennis.API.Middlewares.MiddlewaresService.Interfaces;
using Tennis.API.Middlewares.MiddlewaresService;
using Tennis.API.Services.Encryption;
using Tennis.Configuration;
using Tennis.Services.Interfaces;
using Tennis.Services;
using Tennis.API.Services.Interfaces;
using Tennis.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTennisDbConfiguration();
builder.Services.AddEcnryptionOptions();
builder.Services.AddAuthenticationOptions();
builder.Services.ConfigureJwt();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IJugadorService, JugadorService>();
builder.Services.AddScoped<ITorneoService, TorneoService>();
builder.Services.AddScoped<CustomFilter>();
builder.Services.AddScoped<IExceptionService, ExceptionService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

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
