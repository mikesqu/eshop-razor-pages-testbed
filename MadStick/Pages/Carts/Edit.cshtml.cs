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
using MadStickWebAppTester.Model;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.Carts
{
    [Authorize("CanModifyCarts")]
    public class EditModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public EditModel(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
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

            IList<ApplicationUser> users = await _userService.GetAllUsersAsync();

            ViewData["UserId"] = new SelectList(users, "Id", "Id");
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
                await _cartService.UpdateCart(CartDto,id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(Cart.CartId))
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

        private bool CartExists(int id)
        {
            return _cartService.CartExists(id);
        }
    }
}
