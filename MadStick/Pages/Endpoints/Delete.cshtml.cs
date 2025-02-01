using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;

namespace MadStickWebAppTester.Pages.Endpoints
{
    public class DeleteModel : PageModel
    {
        private readonly MadStickWebAppTester.Data.MadStickContext _context;

        public DeleteModel(MadStickWebAppTester.Data.MadStickContext context)
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

            var endpoint = await _context.Endpoints.FirstOrDefaultAsync(m => m.EndpointId == id);

            if (endpoint == null)
            {
                return NotFound();
            }
            else 
            {
                Endpoint = endpoint;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Endpoints == null)
            {
                return NotFound();
            }
            var endpoint = await _context.Endpoints.FindAsync(id);

            if (endpoint != null)
            {
                Endpoint = endpoint;
                _context.Endpoints.Remove(Endpoint);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
