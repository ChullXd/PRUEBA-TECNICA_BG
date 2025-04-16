namespace AdministracionApi.Security.Data;

public class SecurityRepository(SignInManager<User> signInManager,
    CreateTokenService createTokenService, UserManager<User> userManager, IHttpContextAccessor contextAccessor ) : ISecurityRepository
{
    public async Task<AuthenticationResponse> Login(LoginInfo loginInfo, CancellationToken cancellationToken = default)
    {
        if (await userManager.FindByNameAsync(loginInfo.UserName) is null)
            throw new NotFoundException("User Not Found");
        var result = await signInManager.PasswordSignInAsync(
            loginInfo.UserName,
            loginInfo.Password,
            isPersistent: false,
            lockoutOnFailure: false);
        if (!result.Succeeded) throw new BadRequestException("Failed on login, check password");
        return await createTokenService.CreateTokenCreateToken(loginInfo.UserName);
    }

    public async Task<AuthenticationResponse> CreateUser(RegisterUserInfo registerUserInfo, CancellationToken cancellationToken = default)
    {
        var result = await userManager.CreateAsync(
            User.Create(
                registerUserInfo.FirstName,
                registerUserInfo.LastName,
                registerUserInfo.UserName, 
                registerUserInfo.Email ),
            registerUserInfo.Password);
        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.First().Description);
        return await createTokenService.CreateTokenCreateToken(registerUserInfo.UserName);
    }

    public async Task<AuthenticationResponse> UpdateUser(UserInfo userInfo, CancellationToken cancellationToken = default)
    {
        if (await userManager.FindByNameAsync(
                contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
                    c.Type.Contains("email"))?.Value ?? 
                string.Empty) is not {} user)
            throw new NotFoundException("User Not Found");
        user.FirstName = userInfo.FirstName;
        user.LastName = userInfo.LastName;
        user.UserName = userInfo.UserName;
        user.Email = userInfo.Email;
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new BadRequestException("An Error occurred while updating user");
        return await createTokenService.CreateTokenCreateToken(userInfo.UserName);
    }

    public async Task<Unit> ChangePassword(string newPassword, CancellationToken cancellationToken = default)
    {
        if (await userManager.FindByNameAsync(
                contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
                    c.Type.Contains("email"))?.Value ?? 
                string.Empty) is not {} user)
            throw new NotFoundException("User Not Found");
        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, newPassword);
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new BadRequestException("Ocurrio un error al cmabiar su contraseña");
        return Unit.Value;
    }

    public async Task<Unit> DeleteUser(CancellationToken cancellationToken = default)
    {
        if (await userManager.FindByNameAsync(
                contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
                    c.Type.Contains("email"))?.Value ?? 
                string.Empty) is not {} user)
            throw new NotFoundException("User Not Found");
        user.Active = false;
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new BadRequestException("An Error occurred while deleting user");
        return Unit.Value;
    }
}