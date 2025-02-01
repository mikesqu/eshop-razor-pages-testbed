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
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.StorageUnits
{
    [Authorize("CanViewStorageUnits")]
    public class DetailsModel : PageModel
    {
        private readonly IStorageService _storageService;

        public DetailsModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public StorageUnitDto StorageUnitDto { get; set; } = default!;
        public StorageUnit StorageUnit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StorageUnit = await _storageService.GetStorageUnitAsync(id);

            if (StorageUnit == null)
            {
                return NotFound();
            }
            
            return Page();
        }
    }
}
