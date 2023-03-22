using AutoMapper;
using MediatR;
using saja.Interfaces;

namespace saja.Commands.CreateUserModel;

public abstract class CreateUserModelCommand<T> : IMapWithUserModel, IRequest<T> where T : IUserModel
{
    public abstract void Mapping(Profile profile);
}