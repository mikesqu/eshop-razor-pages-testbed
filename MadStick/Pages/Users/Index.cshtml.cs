using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MadStickWebAppTester.Pages.UserManagment
{
    [Authorize("CanViewUsers")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<ApplicationUser> Users { get; private set; } = null!;
        public IList<IdentityUserClaim<string>> UserClaims { get; private set; } = null!;

        public async Task OnGet()
        {
            Users = await _userService.GetAllUsersAsync();
            UserClaims = _userService.GetAllUserClaimsAsync();

        }
    }
}
