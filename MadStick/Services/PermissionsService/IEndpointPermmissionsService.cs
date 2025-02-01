using MadStickWebAppTester.Data.UserEntity;
using System.Security.Claims;

namespace MadStickWebAppTester.Services.PermissionsService
{
    public interface IEndpointPermmissionsService
    {
        Task<bool> IsEndpointAccessAllowed(string endpoint, ClaimsPrincipal user);
        Task<bool> IsEndpointModificationAllowed(string endpoint, ClaimsPrincipal user);
        Task<bool> CanAccessCart(ClaimsPrincipal user, Cart resource);
    }
}
