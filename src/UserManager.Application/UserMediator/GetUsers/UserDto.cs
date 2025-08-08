using UserManager.Domain.Enums;

namespace UserManager.Application.UserMediator.GetUsers;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public Status Status { get; set; } 
}