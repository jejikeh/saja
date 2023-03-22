using AutoMapper;
using MediatR;
using saja.Common;
using saja.Interfaces;

namespace saja.Commands.UpdateUserModel;

public class UpdateUserModelCommandHandler<T1, T, T2> : IRequestHandler<T1>
    where T : class, IUserModel
    where T1 : UpdateUserModelCommand
    where T2 : IUserModelRepository<T>
{
    private readonly T2 _userModelRepository;
    private readonly IMapper _mapper;

    protected UpdateUserModelCommandHandler(T2 userModelRepository, IMapper mapper)
    {
        _userModelRepository = userModelRepository;
        _mapper = mapper;
    }

    public async Task Handle(T1 request, CancellationToken cancellationToken)
    {
        var modelUser = _mapper.Map<T>(request);
        
        if (await _userModelRepository.GetModelUserById(modelUser.UserId, cancellationToken) is null)
            throw new UserNotFoundException<T>(modelUser.UserId);

        await _userModelRepository.UpdateModelUser(modelUser, cancellationToken);
        await _userModelRepository.SaveChangesAsync(cancellationToken);
    }
}