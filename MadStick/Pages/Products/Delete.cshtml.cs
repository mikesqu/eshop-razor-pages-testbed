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

namespace MadStickWebAppTester.Pages.Products
{
    [Authorize("CanModifyProducts")]
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public MadStickProductDto MadStickProductDto { get; set; } = null!;
        public MadStickProduct MadStickProduct { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MadStickProduct = await _productService.GetProductAsync(id.Value);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id.Value);

            return RedirectToPage("Index", new { pageIndex = 1 });
        }
    }
}
