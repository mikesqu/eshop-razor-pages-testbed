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
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.Carts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public CreateModel(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;

        }

        [BindProperty]
        public CartDto CartDto { get; set; } = default!;
        public Cart Cart { get; set; }

        public async Task<IActionResult> OnGet()
        {
            IList<ApplicationUser> users = await _userService.GetAllUsersAsync();

            ViewData["UserId"] = new SelectList(users, "Id", "Id");

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _cartService.AddNewCartAsync(CartDto);

            return RedirectToPage("./Index");
        }
    }
}
