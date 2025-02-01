using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MadStickWebAppTester.Pages.Users
{
    [Authorize("CanModifyUsers")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MadStickContext _dbContext;

        public DeleteModel(UserManager<ApplicationUser> userManager, MadStickContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public ApplicationUser AppUser { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            AppUser = await _dbContext.Users.FindAsync(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            ApplicationUser user = _dbContext.Users.Find(id);

            if(user != null)
                await _userManager.DeleteAsync(user);

            return RedirectToPage("Index");
        }
    }
}
