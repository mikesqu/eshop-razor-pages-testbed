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
    public class IndexModel : PageModel
    {
        private readonly MadStickWebAppTester.Data.MadStickContext _context;

        public IndexModel(MadStickWebAppTester.Data.MadStickContext context)
        {
            _context = context;
        }

        public IList<Data.UserEntity.Endpoint> Endpoint { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Endpoints != null)
            {
                Endpoint = await _context.Endpoints
                .Include(e => e.Permission).ToListAsync();
            }
        }
    }
}
