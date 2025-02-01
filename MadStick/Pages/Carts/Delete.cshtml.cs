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

namespace MadStickWebAppTester.Pages.Carts
{
    [Authorize("CanModifyCarts")]
    public class DeleteModel : PageModel
    {
        private readonly ICartService _cartService;

        public DeleteModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        [BindProperty]
        public CartDto CartDto { get; set; } = default!;
        public Cart Cart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cart = await _cartService.GetCartByIdAsync(id.Value);
            
            if (Cart == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || ModelState.IsValid == false)
            {
                return NotFound();
            }

            await _cartService.RemoveCartAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
