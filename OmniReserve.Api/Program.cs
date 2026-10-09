
using OmniReserve.Application;
using OmniReserve.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Registrar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar dependencias de Application
builder.Services.AddApplication();

// Registrar dependencias de Infrastructure
builder.Services.AddInfrastructure();

var app = builder.Build();

// Middleware global de excepciones
app.UseMiddleware<
    OmniReserve.Api.Middlewares.GlobalExceptionHandlingMiddleware>();

// Configurar Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
