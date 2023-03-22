using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace saja;

public abstract class AuthOptions
{
    public abstract string Issuer { get; }
    public abstract string Audience { get; }
    public abstract string Key { get; }
    public abstract DateTime Expires { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}