using MediatR;
using saja.Common;
using saja.Interfaces;

namespace saja.Commands.DeleteUserModel;

public class DeleteUserModelCommandHandler<T, T1> : IRequestHandler<DeleteUserModelCommand> 
    where T : class, IUserModel
    where T1 : IUserModelRepository<T>
{
    private readonly T1 _userModelRepository;

    protected DeleteUserModelCommandHandler(T1 userModelRepository)
    {
        _userModelRepository = userModelRepository;
    }

    public async Task Handle(DeleteUserModelCommand request, CancellationToken cancellationToken)
    {
        if (await _userModelRepository.GetModelUserById(request.UserId, cancellationToken) is null)
            throw new UserNotFoundException<T>(request.UserId);

        await _userModelRepository.DeleteModelUser(request.UserId, cancellationToken);
        await _userModelRepository.SaveChangesAsync(cancellationToken);
    }
}