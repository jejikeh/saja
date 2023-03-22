using AutoMapper;
using MediatR;
using saja.Interfaces;

namespace saja.Commands.CreateUserModel;

public class CreateUserModelCommandHandler<T1, T, T2> : IRequestHandler<T1, T> 
    where T : class, IUserModel
    where T1 : CreateUserModelCommand<T>
    where T2 : IUserModelRepository<T>
{
    private readonly IMapper _mapper;
    private readonly T2 _userModelRepository;

    public CreateUserModelCommandHandler(IMapper mapper, T2 userModelRepository)
    {
        _mapper = mapper;
        _userModelRepository = userModelRepository;
    }

    public async Task<T> Handle(T1 request, CancellationToken cancellationToken)
    {
        var modelUser = _mapper.Map<T>(request);
        await _userModelRepository.AddModelUser(modelUser, cancellationToken);
        await _userModelRepository.SaveChangesAsync(cancellationToken);

        return modelUser;
    }
}