using MadStickWebAppTester.Data.UserEntity;
using Microsoft.AspNetCore.Identity;

namespace MadStickWebAppTester.Services
{
    public interface IUserService
    {
        Task EditUserAsync(ApplicationUser appUser);
        IList<IdentityUserClaim<string>> GetAllUserClaimsAsync();
        Task<IList<ApplicationUser>> GetAllUsersAsync();
        ApplicationUser GetUserAsync(string id);
    }
}