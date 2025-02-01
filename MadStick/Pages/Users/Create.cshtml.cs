using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MadStickWebAppTester.Pages.Users
{
    [Authorize("CanModifyUsers")]
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
