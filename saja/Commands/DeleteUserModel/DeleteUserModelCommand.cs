using MediatR;

namespace saja.Commands.DeleteUserModel;

public class DeleteUserModelCommand : IRequest
{
    public required Guid UserId { get; set; }
}