namespace AdministracionApi.Security.Endpoints.Login;
public record LoginCommand(LoginInfo LoginInfo) : IRequest<LoginResult>;
public record LoginResult(AuthenticationResponse AuthenticationResponse);

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.LoginInfo.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.LoginInfo.Password).NotEmpty().WithMessage("Password is required");
    }
}

public class LoginHandler(ISecurityRepository repository) : IRequestHandler<LoginCommand,LoginResult >
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken) =>
         new (await repository.Login(request.LoginInfo, cancellationToken));
}