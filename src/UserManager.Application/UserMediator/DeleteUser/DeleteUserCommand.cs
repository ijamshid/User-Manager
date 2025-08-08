using MediatR;
using UserManager.Domain;
using UserManager.Infrastructure.Repositories.Interfaces;

namespace UserManager.Application.UserMediator.DeleteUser;

public class DeleteUserCommand(int id) : IRequest
{
    public int Id { get; } = id;
}

public class DeleteUserCommandHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id);

        if (user == null)
        {
            throw new NotFoundException("Not found");
        }

        repository.Delete(user);

        await repository.SaveChangesAsync();


    }
}