using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStick.Models.DataModel;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;

namespace MadStickWebAppTester.Pages.StorageUnits
{
    [Authorize("CanViewStorageUnits")]
    public class IndexModel : PageModel
    {
        private readonly IStorageService _storageService;

        public IndexModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public IList<StorageUnit> StorageUnits { get; set; } = default!;
        public ViewCansModel ViewCans { get; set; } = new ViewCansModel();
        public async Task OnGetAsync()
        {

            StorageUnits = await _storageService.GetAllStorageUnitsAsync();

            ViewCans.CanViewDetail = true;
            ViewCans.CanEdit = true;
            ViewCans.CanDelete = true;

        }
        public class ViewCansModel
        {
            public bool CanEdit { get; set; } = false;
            public bool CanViewDetail { get; set; } = false;
            public bool CanDelete { get; set; } = false;
        }
    }
}
