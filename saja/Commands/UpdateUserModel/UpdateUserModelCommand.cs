using AutoMapper;
using MediatR;
using saja.Interfaces;

namespace saja.Commands.UpdateUserModel;

public abstract class UpdateUserModelCommand : IMapWithUserModel, IRequest
{
    public abstract void Mapping(Profile profile);
}