using saja.Interfaces;

namespace saja.Common;

public class UserNotFoundException<T> : Exception where T : IUserModel
{
    public UserNotFoundException(object key) : base($"User {typeof(T).FullName}, ({key}) not found")
    {
    }
}