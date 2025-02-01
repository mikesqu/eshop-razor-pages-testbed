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

namespace MadStickWebAppTester.Pages.Endpoints
{
    public class EditModel : PageModel
    {
        private readonly MadStickWebAppTester.Data.MadStickContext _context;

        public EditModel(MadStickWebAppTester.Data.MadStickContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.UserEntity.Endpoint Endpoint { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Endpoints == null)
            {
                return NotFound();
            }

            var endpoint =  await _context.Endpoints.FirstOrDefaultAsync(m => m.EndpointId == id);
            if (endpoint == null)
            {
                return NotFound();
            }
            Endpoint = endpoint;
           ViewData["EndpointPermissionId"] = new SelectList(_context.EndpointPermissions, "EndpointPermissionId", "EndpointPermissionId");
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

            _context.Attach(Endpoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndpointExists(Endpoint.EndpointId))
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

        private bool EndpointExists(int id)
        {
          return (_context.Endpoints?.Any(e => e.EndpointId == id)).Value;
        }
    }
}
