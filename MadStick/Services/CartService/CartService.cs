using MadStick.Repositories;
using MadStick.VM;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Pages.Carts;
using MadStickWebAppTester.Services;
using Microsoft.EntityFrameworkCore;

namespace MadStick.Services
{
    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly ICartRepository _cartRepo;
        private readonly ICartProductRepository _cartProductRepo;

        public CartService(ILogger<CartService> logger, ICartRepository cartRepo, ICartProductRepository cartProductRepo)
        {
            _logger = logger;
            _cartRepo = cartRepo;
            _cartProductRepo = cartProductRepo;
        }

        public async Task AddCartProductAsync(CartProductDto dto)
        {
            await _cartProductRepo.AddCartProduct(dto);
        }

        public async Task AddNewCartAsync(CartDto dto)
        {
            await _cartRepo.AddCart(dto);

        }

        public bool CartExists(int id)
        {
            return _cartRepo.CartExists(id);
        }

        public async Task<IList<CartProduct>> GetAllCartProductsAsync()
        {
            return await _cartProductRepo.GetAllCartProductsAsync();
        }

        public async Task<IList<Cart>> GetAllCartsAsync()
        {
            return await _cartRepo.GetAllCartsAsync();
        }


        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _cartRepo.GetCartByIdAsync(id);
        }

        public async Task RemoveCartAsync(int id)
        {
            try
            {
                await _cartRepo.RemoveCartAsync(id);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    _logger.LogError($"e: {e}", e);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task RemoveCartProductAsync(int id)
        {
            await _cartProductRepo.RemoveCartProduct(id);      
        }

        public async Task UpdateCart(CartDto cart, int id)
        {
            await _cartRepo.UpdateCart(cart,id);
        }

        public async Task UpdateCartProductAsync(CartProductDto cartProduct, int id)
        {
            await _cartProductRepo.UpdateCartProduct(cartProduct,id);
        }

        private bool CartProductExists(int id)
        {
            return _cartProductRepo.CartProductExists(id);
        }

        public async Task<CartProduct> GetCartProductAsync(int id)
        {
            return await _cartProductRepo.GetCartProduct(id);
        }
    }
}