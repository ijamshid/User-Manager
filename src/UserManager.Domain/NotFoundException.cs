namespace UserManager.Domain
{
    public class NotFoundException(string message) : Exception(message);
}
