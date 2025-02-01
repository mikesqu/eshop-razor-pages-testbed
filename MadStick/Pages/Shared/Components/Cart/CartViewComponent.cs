using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadStick.Pages.Shared.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MadStickContext _dbContext;

        public CartViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, MadStickContext dbContext)
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
            } else if (User.Identity.IsAuthenticated == false)
            {
                return View("Unauthenticated","empty blahg");
            }

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            Cart cart = _dbContext.Carts.Where(c => c.UserId == user.Id).Include(c => c.Products).FirstOrDefault();
            return View("Authenticated",user);
        }
    }
}
