namespace AdministracionApi.Behaviors;

public class ValidationBehavior<TRequest, TResponse> 
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = (await Task.WhenAll(validators
                .Select(v => 
                    v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken))))
            .Where(r => r.Errors.Count is not 0)
            .SelectMany(r => r.Errors).ToList();
        if(failures.Count is not 0)
            throw new ValidationException(failures);
        return await next();
    }
}