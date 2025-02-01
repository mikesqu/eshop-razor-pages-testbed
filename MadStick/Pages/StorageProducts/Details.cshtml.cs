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

namespace MadStickWebAppTester.Pages.StorageProducts
{
    [Authorize("CanViewStorageProducts")]
    public class DetailsModel : PageModel
    {
        private readonly IStorageService _storageService;

        public DetailsModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

      public StorageProduct StorageProduct { get; set; } = null!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StorageProduct = await _storageService.GetStorageProductAsync(id);

            if (StorageProduct == null)
            {
                return NotFound();
            }
            
            return Page();
        }
    }
}
