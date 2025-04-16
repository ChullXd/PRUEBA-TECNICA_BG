namespace AdministracionApi.Security.Endpoints.CreateUser;
public record CreateUserCommand(RegisterUserInfo RegisterUserInfo) : IRequest<CreateUserResult>;
public record CreateUserResult(AuthenticationResponse AuthenticationResponse);

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.RegisterUserInfo.Email).NotEmpty().WithMessage("Email is required")
           .EmailAddress().WithMessage("Invalid email format");
        RuleFor(x => x.RegisterUserInfo.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.RegisterUserInfo.Password).NotEmpty().WithMessage("Password is required")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
           .Matches("[0-9]").WithMessage("Password must contain at least one number")
           .Matches("[^A-Za-z0-9]").WithMessage("Password must contain at least one special character");
        RuleFor(x => x.RegisterUserInfo.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(x => x.RegisterUserInfo.LastName).NotEmpty().WithMessage("LastName is required");
    }
}

public class CreateUserHandler(ISecurityRepository repository): IRequestHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken) =>
        new (await repository.CreateUser(request.RegisterUserInfo, cancellationToken));
    
}