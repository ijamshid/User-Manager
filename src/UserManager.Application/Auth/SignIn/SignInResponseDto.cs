namespace UserManager.Application.Auth.SignIn;

public class SignInResponseDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
    public int UserId { get; set; }  


    public int ExpiresIn { get; set; }
}