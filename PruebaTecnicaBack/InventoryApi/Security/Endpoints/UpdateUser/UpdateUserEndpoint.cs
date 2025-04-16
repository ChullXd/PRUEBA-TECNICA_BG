namespace AdministracionApi.Security.Endpoints.UpdateUser;

public class UpdateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(SecurityRoutes.UpdateUser, async (ISender sender, [AsParameters] UpdateUserCommand command) =>
                Results.Ok((await sender.Send(command)).AuthenticationResponse)
            )
            .AllowAnonymous()
            .WithName("UpdateUser")
            .WithTags(SecurityRoutes.Tag)
            .Produces<AuthenticationResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("UpdateUser")
            .WithDescription("UpdateUser");
    }
}