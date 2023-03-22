using MediatR;
using saja.Interfaces;

namespace saja.Queries;

public class GetModelByUsernameQuery<T> : IRequest<T> where T : IUserModel
{
    public required string Username { get; set; }
}