using Microsoft.AspNetCore.Identity;

namespace NZwalks.api.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
