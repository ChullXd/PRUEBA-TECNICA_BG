namespace AdministracionApi.Security.Endpoints.CreateUser;

public class CreateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(SecurityRoutes.CreateUser, async (ISender sender, [AsParameters] CreateUserCommand command) =>
                Results.Created($"{SecurityRoutes.Login}", (await sender.Send(command)).AuthenticationResponse)
            )
            .AllowAnonymous()
            .WithName("CreateUser")
            .WithTags(SecurityRoutes.Tag)
            .Produces<AuthenticationResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("CreateUser")
            .WithDescription("CreateUser");
    }
}