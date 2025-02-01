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
using Microsoft.AspNetCore.Identity;
using MadStickWebAppTester.Services;

namespace MadStickWebAppTester.Pages.Carts
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IAuthorizationService _authService;
        private readonly ICartService _cartService;

        public DetailsModel(IAuthorizationService authService, ICartService cartService)
        {
            _authService = authService;
            _cartService = cartService;
        }

        public Cart? Cart { get; set; } = null;

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

            AuthorizationResult authResult = await _authService.AuthorizeAsync(User, Cart, "CanViewCartDetails");

            if (authResult.Succeeded == false)
            {
                return new ForbidResult();
            }

            return Page();
        }
    }
}
