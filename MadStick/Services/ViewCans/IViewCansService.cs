using System.Security.Claims;

namespace MadStickWebAppTester.Services.ViewCans
{
    public interface IViewCansService
    {
        Task<bool> CanViewIndex(ClaimsPrincipal user);
        Task<bool> CanViewDetails(ClaimsPrincipal user);
        Task<bool> CanCreate(ClaimsPrincipal user);
        Task<bool> CanEdit(ClaimsPrincipal user);
        Task<bool> CanDelete(ClaimsPrincipal user);
    }
}