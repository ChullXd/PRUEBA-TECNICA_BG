namespace AdministracionApi.Security.Endpoints.ChangePassword;

public class ChangePasswordEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(SecurityRoutes.ChangePassword,
            async (ISender sender, [AsParameters] ChangePasswordCommand command) =>{
                await sender.Send(command);
                return Results.NoContent();
            })
            .WithName("ChangePassword")
            .WithTags(SecurityRoutes.Tag)
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("ChangePassword")
            .WithDescription("ChangePassword");
    }
}