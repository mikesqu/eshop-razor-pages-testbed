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
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.Products
{
    [Authorize("CanModifyProducts")]
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        public EditModel(IProductService productService)
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

            if (MadStickProduct == null)
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
                await _productService.UpdateProduct(MadStickProductDto,id.Value);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MadStickProductExists(id.Value))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index", new {pageIndex = 1});
        }

        private bool MadStickProductExists(int id)
        {
          return _productService.ProductExists(id);
        }
    }
}
