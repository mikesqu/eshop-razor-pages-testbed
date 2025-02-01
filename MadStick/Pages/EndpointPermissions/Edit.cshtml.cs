using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;

namespace MadStickWebAppTester.Pages.EndpointPermissions
{
    public class EditModel : PageModel
    {
        private readonly MadStickWebAppTester.Data.MadStickContext _context;

        public EditModel(MadStickWebAppTester.Data.MadStickContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EndpointPermission EndpointPermission { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EndpointPermissions == null)
            {
                return NotFound();
            }

            var endpointpermission =  await _context.EndpointPermissions.FirstOrDefaultAsync(m => m.EndpointPermissionId == id);
            if (endpointpermission == null)
            {
                return NotFound();
            }
            EndpointPermission = endpointpermission;
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EndpointPermission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndpointPermissionExists(EndpointPermission.EndpointPermissionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EndpointPermissionExists(int id)
        {
          return (_context.EndpointPermissions?.Any(e => e.EndpointPermissionId == id)).Value;
        }
    }
}
