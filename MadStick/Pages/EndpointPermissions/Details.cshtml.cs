using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;

namespace MadStickWebAppTester.Pages.EndpointPermissions
{
    public class DetailsModel : PageModel
    {
        private readonly MadStickWebAppTester.Data.MadStickContext _context;

        public DetailsModel(MadStickWebAppTester.Data.MadStickContext context)
        {
            _context = context;
        }

      public EndpointPermission EndpointPermission { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EndpointPermissions == null)
            {
                return NotFound();
            }

            var endpointpermission = await _context.EndpointPermissions.FirstOrDefaultAsync(m => m.EndpointPermissionId == id);
            if (endpointpermission == null)
            {
                return NotFound();
            }
            else 
            {
                EndpointPermission = endpointpermission;
            }
            return Page();
        }
    }
}
