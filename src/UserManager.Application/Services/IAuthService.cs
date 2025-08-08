namespace UserManager.Application.Services;

public interface IAuthService
{
    Task<string> GetToken(int userId, string username);
}