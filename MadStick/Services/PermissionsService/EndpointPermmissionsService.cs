using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MadStickWebAppTester.Services.PermissionsService
{
    internal class EndpointPermmissionsService : IEndpointPermmissionsService
    {
        private readonly MadStickContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public EndpointPermmissionsService(MadStickContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<bool> CanAccessCart(ClaimsPrincipal user, Cart cart)
        {
            ApplicationUser tempU = await _userManager.GetUserAsync(user);
            //TODO: throw error if the are more than one user with the same id \/
            ApplicationUser cUser = _dbContext.Users.Include(u => u.Cart).Where(u => u.Id == tempU.Id).First();
            if (cUser.Cart.CartId == cart.CartId)
                return true;
            else
                return false;
        }


        public async Task<bool> IsEndpointAccessAllowed(string endpoint, ClaimsPrincipal user)
        {
            string userId = _userManager.GetUserId(user);

            ApplicationUser currentUser = _dbContext.Users.Include(u => u.EndpointPermissions).ThenInclude(eP => eP.Endpoints).Where(u => u.Id == userId).First();

            if (currentUser.EndpointPermissions != null && currentUser.EndpointPermissions.Endpoints != null)
            {
                bool isAuthorized = currentUser.EndpointPermissions.Endpoints.Any((e) => 
                {
                    if (e.Value == endpoint || e.IsAllowedAccess == true)
                        return true;
                    else
                        return false;
                 });

                if (isAuthorized)
                {
                    return true;
                }

            }

            return false;
        }

        public async Task<bool> IsEndpointModificationAllowed(string endpoint, ClaimsPrincipal user)
        {
            string userId = _userManager.GetUserId(user);

            ApplicationUser currentUser = _dbContext.Users.Include(u => u.EndpointPermissions).ThenInclude(eP => eP.Endpoints).Where(u => u.Id == userId).First();

            if (currentUser.EndpointPermissions != null && currentUser.EndpointPermissions.Endpoints != null)
            {
                bool isAuthorized = currentUser.EndpointPermissions.Endpoints.Any((e) =>
                {
                    if (e.Value == endpoint || e.IsAllowedModification == true)
                        return true;
                    else
                        return false;
                });

                if (isAuthorized)
                {
                    return true;
                }

            }

            return false;
        }
    }
}