using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManager.Infrastructure.Repositories.Interfaces;

namespace UserManager.Application.UserMediator.GetUsers;

public class GetUsersQuery : IRequest<IList<UserDto>>;

public class GetUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetUsersQuery,IList<UserDto>>
{
    public async Task<IList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetAll()
            .Select(r => new UserDto()
            {
                Id = r.Id,
                Username = r.Username,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Email = r.Email,
                LastLoginDate = r.LastLoginDate,
                Status = r.Status
            }).ToListAsync(cancellationToken: cancellationToken);

        return result;
    }
}
