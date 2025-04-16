namespace AdministracionApi.Security.Endpoints.UpdateUser;
public record UpdateUserCommand(UserInfo UserInfo) : IRequest<UpdateUserResult>;
public record UpdateUserResult(AuthenticationResponse AuthenticationResponse);

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserInfo.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        RuleFor(x => x.UserInfo.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.UserInfo.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(x => x.UserInfo.LastName).NotEmpty().WithMessage("LastName is required");
    }
}

public class UpdateUserHandle(ISecurityRepository repository): IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken) =>
         new (await repository.UpdateUser(command.UserInfo, cancellationToken));
}