namespace saja.Interfaces;

public interface IUserModelRepository<T> where T : class, IUserModel
{
    public Task<T?> GetModelUserById(Guid id, CancellationToken cancellationToken);
    public Task<T?> GetModelUserByUsername(string username, CancellationToken cancellationToken);
    public Task AddModelUser(T modelUser, CancellationToken cancellationToken);
    public Task UpdateModelUser(T modelUser, CancellationToken cancellationToken);
    public Task DeleteModelUser(Guid id, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}