using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.CartProducts
{
    [Authorize("CanModifyCartProducts")]
    public class DeleteModel : PageModel
    {
        private readonly ICartService _cartService;

        public DeleteModel(ICartService cartService)
        {
            _cartService = cartService;
        }

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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _cartService.RemoveCartProductAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
