namespace AdministracionApi.Security.Endpoints.ChangePassword;
public record ChangePasswordCommand(string NewPassword) : IRequest<ChangePasswordResult>;
public record ChangePasswordResult(Unit Unit);

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^A-Za-z0-9]").WithMessage("Password must contain at least one special character");
    }
}

public class ChangePasswordHandler(ISecurityRepository repository): IRequestHandler<ChangePasswordCommand,ChangePasswordResult >
{
    public async Task<ChangePasswordResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) => 
        new (await repository.ChangePassword(request.NewPassword, cancellationToken));
}