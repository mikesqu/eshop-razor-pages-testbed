using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStick.Models.DataModel;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

        public MadStickProductDto MadStickProductDto { get; set; } = null!;
        public MadStickProduct MadStickProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(string slugName)
        {
            MadStickProduct = await _productService.GetProductAsync(slugName);

            return Page();
        }
    }
}
