using MediatR;
using saja.Common;
using saja.Interfaces;

namespace saja.Queries;

public class GetModelByUsernameQueryHandler<T1, T, T2> : IRequestHandler<T1, T> 
    where T : class, IUserModel 
    where T1 : GetModelByUsernameQuery<T>
    where T2 : IUserModelRepository<T>
{
    private readonly T2 _userModelRepository;

    protected GetModelByUsernameQueryHandler(T2 userModelRepository)
    {
        _userModelRepository = userModelRepository;
    }

    public async Task<T> Handle(T1 request, CancellationToken cancellationToken)
    {
        var user = await _userModelRepository.GetModelUserByUsername(request.Username, cancellationToken);
        if(user is null)
            throw new UserNotFoundException<T>(request.Username);

        return user;
    }
}