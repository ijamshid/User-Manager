using MediatR;
using UserManager.Domain.Entities;
using UserManager.Domain.Enums;
using UserManager.Domain.Services;
using UserManager.Infrastructure.Repositories.Interfaces;

namespace UserManager.Application.Auth.SignUp;

public class SignUpCommand(SignUpRequestDto request) : IRequest<SignUpResponseDto>
{
    public SignUpRequestDto Request { get; } = request;
}

public class SignUpCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher) : IRequestHandler<SignUpCommand, SignUpResponseDto>
{
    public async Task<SignUpResponseDto> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHasher.HashPassword(request.Password),
            Role = Role.SuperAdmin
        };

        await repository.AddAsync(user);
        await repository.SaveChangesAsync();

        return new SignUpResponseDto()
        {
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
        };
    }
}