using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Model;

namespace MadStickWebAppTester.Pages.EndpointPermissions
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
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public EndpointPermissionsVM EndpointPermission { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid || _context.EndpointPermissions == null || EndpointPermission == null)
            {
                return Page();
            }

            EndpointPermission newEndpointPermission = new EndpointPermission()
            {
                EndpointPermissionId = EndpointPermission.EndpointPermissionId,
                Endpoints = EndpointPermission.Endpoints,
                UserId = EndpointPermission.UserId,
                User = EndpointPermission.User
            };

            _context.EndpointPermissions.Add(newEndpointPermission);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
