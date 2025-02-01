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
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Extensions;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.Products
{
    [Authorize("CanModifyProducts")]
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MadStickProductDto MadStickProductDto { get; set; } = null!;
        public MadStickProduct MadStickProduct { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _productService.AddProductAsync(MadStickProductDto);

            return RedirectToPage("./Index", new {pageIndex = 1});
        }
    }
}
