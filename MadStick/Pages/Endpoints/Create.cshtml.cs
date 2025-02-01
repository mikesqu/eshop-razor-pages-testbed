using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;

namespace MadStickWebAppTester.Pages.Endpoints
{
    public class CreateModel : PageModel
    {
        private readonly MadStickWebAppTester.Data.MadStickContext _context;

        public CreateModel(MadStickWebAppTester.Data.MadStickContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["EndpointPermissionId"] = new SelectList(_context.EndpointPermissions, "EndpointPermissionId", "EndpointPermissionId");
            return Page();
        }

        [BindProperty]
        public Data.UserEntity.Endpoint Endpoint { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Endpoints == null || Endpoint == null)
            {
                return Page();
            }

            _context.Endpoints.Add(Endpoint);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
