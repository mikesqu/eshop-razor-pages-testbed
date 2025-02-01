using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MadStickWebAppTester.Data;
using MadStick.Models.DataModel;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.StorageUnits
{
    [Authorize("CanModifyStorageUnits")]
    public class CreateModel : PageModel
    {
        private readonly IStorageService _storageService;
        
        public CreateModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StorageUnitDto StorageUnitDto { get; set; } = null!;
        public StorageUnit StorageUnit { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            await _storageService.AddStorageUnitAsync(StorageUnitDto);

            return RedirectToPage("./Index");
        }
    }
}
