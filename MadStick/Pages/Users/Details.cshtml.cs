using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MadStickWebAppTester.Pages.Users
{
    public class DetailsModel : PageModel
    {
        [Authorize("CanViewUsers")]
        public void OnGet()
        {
        }
    }
}
