using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadStick.Pages.Shared.Components
{
    public class UserViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MadStickContext _dbContext;

        public UserViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, MadStickContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int showPicture)
        {
            if (HttpContext.User == null)
            {
                return View();
            }else if (User.Identity.IsAuthenticated == false)
            {
                return View("Unauthenticated","empty blahg");
            }

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            user.Cart = _dbContext.Carts.Where(c => c.UserId == user.Id).FirstOrDefault();
            return View("Authenticated",user);
            
        }
    }
}
