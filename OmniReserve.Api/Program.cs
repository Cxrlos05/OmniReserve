
using OmniReserve.Application;
using OmniReserve.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Registrar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar Application
builder.Services.AddApplication();

// Registrar Infrastructure con IConfiguration
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Middleware global de excepciones
app.UseMiddleware<
    OmniReserve.Api.Middlewares.GlobalExceptionHandlingMiddleware>();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
