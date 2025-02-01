using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Model;
using MadStick.Models.DataModel;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.CartProducts
{
    [Authorize("CanModifyCartProducts")]
    public class EditModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public EditModel(ICartService cartService,IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [BindProperty]
        public CartProductDto CartProductDto { get; set; } = default!;
        public CartProduct CartProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CartProduct = await _cartService.GetCartProductAsync(id.Value);

            if (CartProduct == null)
            {
                return NotFound();
            }

            IList<Cart> carts = await _cartService.GetAllCartsAsync();
            IList<MadStickProduct> products = await _productService.GetAllProductsAsync();
            
            ViewData["CartId"] = new SelectList(carts, "CartId", "CartId");
            ViewData["MadStickProductId"] = new SelectList(products, "MadStickProductId", "MadStickProductId");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                await _cartService.UpdateCartProductAsync(CartProductDto,id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        private bool CartExists(int cartId)
        {
            return _cartService.CartExists(cartId);
        }
    }
}
