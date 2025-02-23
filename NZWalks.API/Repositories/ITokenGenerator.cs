using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenGenerator
    {
        String GenerateToken(IdentityUser userIdentity, List<String> roles);
    }
}
