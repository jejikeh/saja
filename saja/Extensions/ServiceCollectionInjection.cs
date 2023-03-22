using Microsoft.Extensions.DependencyInjection;
using saja.Interfaces;

namespace saja.Extensions;

public static class ServiceCollectionInjection
{
    public static IServiceCollection AddSaja<T, T1>(this IServiceCollection serviceCollection) 
        where T : class, IUserModel 
        where T1 : class, IUserModelRepository<T>
    {
        serviceCollection.AddScoped<IUserModelRepository<T>, T1>();
        return serviceCollection;
    }
}