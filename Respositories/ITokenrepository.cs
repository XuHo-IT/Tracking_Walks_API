using Microsoft.AspNetCore.Identity;

namespace NZWalk.API.Respositories
{
    public interface ITokenrepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);

    }
}
