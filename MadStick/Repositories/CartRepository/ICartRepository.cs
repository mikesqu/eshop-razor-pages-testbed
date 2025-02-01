using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;

namespace MadStick.Repositories
{
    public interface ICartRepository
    {
        Task AddCart(CartDto dto);
        bool CartExists(int id);
        Task<IList<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(int cartId);
        Task RemoveCartAsync(int id);
        Task UpdateCart(CartDto cart, int id);
    }
}