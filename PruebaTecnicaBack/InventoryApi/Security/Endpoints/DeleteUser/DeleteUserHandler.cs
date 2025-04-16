namespace AdministracionApi.Security.Endpoints.DeleteUser;
public record DeleteUserCommand(Unit Unit) : IRequest<DeleteUserResult>;
public record DeleteUserResult(Unit Unit);
public class DeleteUserHandler(ISecurityRepository repository) : IRequestHandler<DeleteUserCommand,DeleteUserResult >
{
    public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken) =>
         new (await repository.DeleteUser(cancellationToken));
    
}