using Microsoft.EntityFrameworkCore;
using saja.Interfaces;

namespace saja;

public class UserModelBaseRepository<T, T1> : IUserModelRepository<T> 
    where T : class, IUserModel
    where T1 : IUserModelDbContext<T>
{
    protected readonly T1 DbContext;

    protected UserModelBaseRepository(T1 dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetModelUserById(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Users.SingleOrDefaultAsync(user => user.UserId == id, cancellationToken);
    }

    public async Task<T?> GetModelUserByUsername(string username, CancellationToken cancellationToken)
    {
        return await DbContext.Users.SingleOrDefaultAsync(user => user.Username == username, cancellationToken);
    }

    public async Task AddModelUser(T modelUser, CancellationToken cancellationToken)
    {
        await DbContext.Users.AddAsync(modelUser, cancellationToken);
    }

    public async Task UpdateModelUser(T modelUser, CancellationToken cancellationToken)
    {
        var user = await GetModelUserById(modelUser.UserId, cancellationToken);
        
        if (user is null)
            return;
        
        user.Username = modelUser.Username;
        user.PasswordHash = modelUser.PasswordHash;
    }

    public async Task DeleteModelUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await GetModelUserById(id, cancellationToken);
        
        if (user is null)
            return;
        
        DbContext.Users.Remove(user);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}