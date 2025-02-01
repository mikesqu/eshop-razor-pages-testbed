using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Model;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStick.Models.DataModel;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.CartProducts
{
    [Authorize("CanModifyCartProducts")]
    public class CreateModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CreateModel(ICartService cartService,IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [BindProperty]
        public CartProductDto CartProductDto { get; set; } = default!;
        public CartProduct CartProduct { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            IList<CartProduct> carts = await _cartService.GetAllCartProductsAsync();

            IList<MadStickProduct> products = await _productService.GetAllProductsAsync();

            ViewData["CartId"] = new SelectList(carts, "CartId", "CartId");
            ViewData["MadStickProductId"] = new SelectList(products, "MadStickProductId", "MadStickProductId");
            return Page();
        }

        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _cartService.AddCartProductAsync(CartProductDto);

            return RedirectToPage("./Index");
        }
    }
}
