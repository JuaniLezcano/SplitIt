using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace SplitIt.Application.Behaviors;

public class RequestResponseLoggingBehavior<TRequest, TResponse>(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var uniqueId = Guid.NewGuid().ToString()[..8];

        // Request Logging
        // Serialize the request
        logger.LogInformation("[{Id}] Iniciando Request: {Name}", uniqueId, requestName);

        var timer = Stopwatch.StartNew();
        TResponse response;
        // Response logging
        try
        {
            // Ejecuta el siguiente paso en el pipeline (Validación -> Handler)
            response = await next();
        }
        catch (Exception ex)
        {
            // Log de error si algo explota
            logger.LogError(ex, "[{Id}] Error detectado en {Name}: {Message}", uniqueId, requestName, ex.Message);
            throw; // Re-lanzamos para que el Middleware Global de la API lo maneje
        }
        finally
        {
            timer.Stop();
        }

        // 2. Log de finalización con tiempo de respuesta
        logger.LogInformation("[{Id}] Finalizado {Name} en {Elapsed}ms",
            uniqueId, requestName, timer.ElapsedMilliseconds);


        return response;
    }
}