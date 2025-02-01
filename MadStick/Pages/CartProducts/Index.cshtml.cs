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

namespace MadStickWebAppTester.Pages.CartProducts
{
    [Authorize("CanViewCartProducts")]
    public class IndexModel : PageModel
    {
        private readonly ICartService _cartService;
        private readonly ILogger _logger;
        public IndexModel(ICartService cartService, ILogger logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        public IList<CartProduct> CartProducts { get; set; } = default!;

        public async Task OnGetAsync(string returnUrl)
        {
            _logger.LogInformation("returnUrl: {returnUrl}",returnUrl);
            CartProducts = await _cartService.GetAllCartProductsAsync();

        }
    }
}
