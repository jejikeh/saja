using Microsoft.EntityFrameworkCore;

namespace saja.Interfaces;

public interface IUserModelDbContext<T> where T : class, IUserModel
{
    public DbSet<T> Users { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}