
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using AppValidationException =
    OmniReserve.Application.Common.Exceptions.ValidationException;

namespace OmniReserve.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        // Manejar errores de FluentValidation
        if (exception is AppValidationException validationEx)
        {
            _logger.LogWarning(
                "Error de validación: {Message}",
                validationEx.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Error de Validación",
                Detail = "Los datos enviados no son válidos."
            };

            problemDetails.Extensions.Add(
                "errors",
                validationEx.Errors);

            context.Response.StatusCode =
                StatusCodes.Status400BadRequest;

            await JsonSerializer.SerializeAsync(
                context.Response.Body,
                problemDetails,
                cancellationToken: context.RequestAborted);

            return;
        }

        // Manejar errores no contemplados
        _logger.LogError(
            exception,
            "Ocurrió un error inesperado.");

        var errorDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Error interno del servidor",
            Detail = "Ocurrió un error inesperado al procesar la solicitud."
        };

        context.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        await JsonSerializer.SerializeAsync(
            context.Response.Body,
            errorDetails,
            cancellationToken: context.RequestAborted);
    }
}
