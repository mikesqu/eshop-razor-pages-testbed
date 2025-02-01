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

namespace MadStickWebAppTester.Pages.Carts
{
    [Authorize("CanViewCarts")]
    public class IndexModel : PageModel
    {
        private readonly ICartService _cartService;

        public IndexModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IList<Cart> Carts { get; set; } = default!;
        

        public async Task OnGetAsync()
        {
            Carts = await _cartService.GetAllCartsAsync();
        }
    }
}
