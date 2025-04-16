namespace AdministracionApi.Security.Endpoints.DeleteUser;

public class DeleteUserEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(SecurityRoutes.DeleteUser, async (ISender sender) =>{
                    await sender.Send(new DeleteUserCommand(Unit.Value));
                    return Results.NoContent();
                }
            )
            .WithName("DeleteUser")
            .WithTags(SecurityRoutes.Tag)
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("DeleteUser")
            .WithDescription("DeleteUser");
    }
}