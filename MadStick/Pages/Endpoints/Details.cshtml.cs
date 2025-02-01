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
    public class DetailsModel : PageModel
    {
        private readonly MadStickWebAppTester.Data.MadStickContext _context;

        public DetailsModel(MadStickWebAppTester.Data.MadStickContext context)
        {
            _context = context;
        }

        public Data.UserEntity.Endpoint Endpoint { get; set; } = null;
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
    }
}
