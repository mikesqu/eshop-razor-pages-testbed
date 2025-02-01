using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;

namespace MadStick.Repositories
{
    public interface ICartProductRepository
    {
        Task AddCartProduct(CartProductDto cartProduct);
        bool CartProductExists(int id);
        Task<IList<CartProduct>> GetAllCartProductsAsync();
        Task<CartProduct> GetCartProduct(int id);
        Task RemoveCartProduct(int id);
        Task UpdateCartProduct(CartProductDto cartProduct, int id);
    }
}