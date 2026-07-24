using AxiaVeiculosApplication.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AxiaVeiculosWebApi.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()?
                    .Error;

                var problemDetails = CriarProblemDetails(exception);

                context.Response.StatusCode = problemDetails.Status
                    ?? StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
            });
        });

        return app;
    }

    private static ProblemDetails CriarProblemDetails(Exception? exception)
    {
        return exception switch
        {
            ValidationException validationException => CriarValidationProblemDetails(validationException),
            VeiculoNaoEncontradoException notFoundException => new ProblemDetails
            {
                Title = "Recurso não encontrado.",
                Detail = notFoundException.Message,
                Status = StatusCodes.Status404NotFound
            },
            _ => new ProblemDetails
            {
                Title = "Erro inesperado.",
                Detail = "Não foi possivel processar a requisição.",
                Status = StatusCodes.Status500InternalServerError
            }
        };
    }

    private static ValidationProblemDetails CriarValidationProblemDetails(ValidationException validationException)
    {
        var errors = validationException.Errors
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.ErrorMessage).ToArray());

        return new ValidationProblemDetails(errors)
        {
            Title = "Erro de validação.",
            Status = StatusCodes.Status400BadRequest
        };
    }
}
