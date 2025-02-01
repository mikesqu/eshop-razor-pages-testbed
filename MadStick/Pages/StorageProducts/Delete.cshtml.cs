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
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.StorageProducts
{
    [Authorize("CanModifyStorageProducts")]
    public class DeleteModel : PageModel
    {
        private readonly IStorageService _storageService;

        public DeleteModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [BindProperty]
        public StorageProductDto StorageProductDto { get; set; } = default!;
        public StorageProduct StorageProduct { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StorageProduct = await _storageService.GetStorageProductAsync(id);

            if (StorageProduct == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await _storageService.DeleteStorageProduct(id);

            return RedirectToPage("./Index");
        }
    }
}
