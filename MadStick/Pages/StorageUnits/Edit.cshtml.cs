using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStick.Models.DataModel;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.StorageUnits
{
    [Authorize("CanModifyStorageUnits")]
    public class EditModel : PageModel
    {
        private readonly IStorageService _storageService;
        
        public EditModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [BindProperty]
        public StorageUnitDto StorageUnitDto { get; set; } = null!;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid || id == null)
            {
                return Page();
            }
            
            try
            {
                await _storageService.EditStorageUnit(StorageUnitDto,id.Value);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageUnitExists(id.Value))
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

        private bool StorageUnitExists(int id)
        {
          return _storageService.StorageUnitExists(id);
        }
    }
}
