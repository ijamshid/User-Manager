using UserManager.Domain.Enums;

namespace UserManager.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; } = Status.Active;
        public Role Role { get; set; }
  
    }
}
