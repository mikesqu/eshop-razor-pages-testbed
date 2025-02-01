using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;
using MadStickWebAppTester.Model;

namespace MadStickWebAppTester.Services
{
    public interface ICartService
    {
        //cart
        Task AddNewCartAsync(CartDto newCart);
        bool CartExists(int id);
        Task<IList<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(int id);
        Task RemoveCartAsync(int id);
        Task UpdateCart(CartDto dto,int id);
        
        //cart product
        Task<IList<CartProduct>> GetAllCartProductsAsync();
        Task UpdateCartProductAsync(CartProductDto dto,int id);
        Task AddCartProductAsync(CartProductDto dto);
        Task RemoveCartProductAsync(int id);
        Task<CartProduct> GetCartProductAsync(int value);
    }
}