using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MadStickWebAppTester.Pages.Users
{
    [Authorize("CanModifyUsers")]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        
        public EditModel(IUserService userService)
        {
            _userService = userService;
        }
        
        [BindProperty]
        public ApplicationUser AppUser { get; set; } = null!;
        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser = _userService.GetUserAsync(id);

            if (AppUser == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.EditUserAsync(AppUser);

            return RedirectToPage("./Index");

        }
    }
}
