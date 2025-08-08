using MediatR;
using UserManager.Application.Services;
using UserManager.Domain.Services;
using UserManager.Infrastructure.Repositories.Interfaces;

namespace UserManager.Application.Auth.SignIn;

public class SignInCommand(SignInRequestDto request) : IRequest<SignInResponseDto>
{
    public SignInRequestDto Request { get; } = request;
}

public class SignInCommandHandler(
   IUserRepository userRepository,
   IAuthService authService,
   IPasswordHasher passwordHasher)
    : IRequestHandler<SignInCommand, SignInResponseDto>
{
    public async Task<SignInResponseDto> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var isPasswordValid = passwordHasher.VerifyHash(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        user.LastLoginDate = DateTime.UtcNow;
        await userRepository.SaveChangesAsync();

        var token = await authService.GetToken(user.Id,request.Email);

        return new SignInResponseDto()
        {
            AccessToken = token,
            UserId=user.Id
        };

    }
}