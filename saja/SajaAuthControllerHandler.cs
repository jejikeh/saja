using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using saja.Commands.CreateUserModel;
using saja.Interfaces;
using saja.Queries;

namespace saja;

public class SajaAuthControllerHandler<T> where T : IUserModel
{
    private readonly Func<IMediator> _getMediator;
    private readonly ControllerBase _controllerBase;
    private readonly AuthOptions _authOptions;

    public SajaAuthControllerHandler(Func<IMediator> getMediator, ControllerBase controllerBase, AuthOptions authOptions)
    {
        _getMediator = getMediator;
        _controllerBase = controllerBase;
        _authOptions = authOptions;
    }
    
    public async Task<ActionResult<T>> Register(CreateUserModelCommand<T> command)
    {
        if (_getMediator?.Invoke() == null) 
            return _controllerBase.BadRequest("Internal server error.");
        
        var innoUserDetailsViewModel = await _getMediator.Invoke().Send(command);
        return _controllerBase.Ok(innoUserDetailsViewModel);
    }
    
    public async Task<ActionResult<string>> Login(GetModelByUsernameQuery<T> command, Func<string, bool> verifyAction)
    {
        if (_getMediator?.Invoke() == null) 
            return _controllerBase.BadRequest("Internal server error.");
        
        var userModel = await _getMediator.Invoke().Send(command);
        if (verifyAction.Invoke(userModel.PasswordHash))
            return _controllerBase.BadRequest("Register error!");
        
        return _controllerBase.Ok(GenerateToken(userModel));
    }

    private string GenerateToken(T modelUser)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, modelUser.Username),
            new Claim("UserId", modelUser.UserId.ToString()),
        };
        
        var credentials = new SigningCredentials(
            _authOptions.GetSymmetricSecurityKey(), 
            SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: claims,
            expires: _authOptions.Expires,
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}