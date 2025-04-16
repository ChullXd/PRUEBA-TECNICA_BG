namespace AdministracionApi.Security.Endpoints.Login;

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(SecurityRoutes.Login, async (ISender sender, [AsParameters] LoginCommand command) =>
            Results.Ok((await sender.Send(command)).AuthenticationResponse)
        )
        .AllowAnonymous()
        .WithName("Login")
        .WithTags(SecurityRoutes.Tag)
        .Produces<AuthenticationResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Login")
        .WithDescription("Login");
    }
}