using ThapMuoi.Models.Auth;
using ThapMuoi.Models.Core;

namespace ThapMuoi.Interfaces.Auth
{
    public interface IIdentityService
    {
        Task<User> Authenticate(AuthRequest model);
        Task<dynamic> LoginAsync(AuthRequest model);
   
    }
}