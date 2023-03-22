namespace saja.Interfaces;

public interface IUserModel
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}