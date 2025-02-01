using MadStick.VM;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Pages.Carts;
using MadStickWebAppTester.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MadStick.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly MadStickContext _context;

        public UserService(ILogger<UserService> logger, MadStickContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task EditUserAsync(ApplicationUser appUser)
        {
            
            ApplicationUser u = _context.Users.Where(u => u.Id == appUser.Id).FirstOrDefault();

            _context.Attach(u).State = EntityState.Modified;
            
            u.Cart = appUser.Cart;
            u.Claims = appUser.Claims;
            u.EndpointPermissions = appUser.EndpointPermissions;

            await _context.SaveChangesAsync();
        }

        public IList<IdentityUserClaim<string>> GetAllUserClaimsAsync()
        {
            return _context.UserClaims.ToList();
        }

        public async Task<IList<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public ApplicationUser GetUserAsync(string id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}