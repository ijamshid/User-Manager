using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManager.Domain.Enums;
using UserManager.Infrastructure;

namespace UserManager.Application.UserMediator.UpdateUserStatus
{
    public record UpdateUserStatusCommand(int UserId, Status NewStatus) : IRequest<Unit>;

    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public UpdateUserStatusCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {request.UserId} not found.");
            }

            user.Status = request.NewStatus;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
