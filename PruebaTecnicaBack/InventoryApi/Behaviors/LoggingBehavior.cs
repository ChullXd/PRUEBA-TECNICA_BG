namespace AdministracionApi.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var requestId = Guid.NewGuid();
        var requestData = JsonSerializer.Serialize(request);
        logger.LogInformation("[START] Handling {RequestName} [{RequestId}] with data: {RequestData}",
            requestName, requestId, requestData);
        var stopwatch = Stopwatch.StartNew();
        var response = await next();
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            var responseData = JsonSerializer.Serialize(response);
            if (elapsedMilliseconds > 3000)
                logger.LogWarning("[PERFORMANCE] {RequestName} [{RequestId}] took {ElapsedMilliseconds} ms",
                    requestName, requestId, elapsedMilliseconds);
            logger.LogInformation("[END] Handled {RequestName} [{RequestId}] with response: {ResponseData} took {ElapsedMilliseconds} ms",
                requestName, requestId, responseData, elapsedMilliseconds);
        return response;
    }
}