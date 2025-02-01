using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;
using Microsoft.EntityFrameworkCore;

namespace MadStick.Repositories
{
    public class CartProductRepository : ICartProductRepository
    {
        private readonly ILogger<CartProductRepository> _logger;
        private readonly MadStickContext _context;
        public CartProductRepository(ILogger<CartProductRepository> logger, MadStickContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddCartProduct(CartProductDto cartProduct)
        {
            CartProduct newCartProduct = new CartProduct()
            {
                AmountInBasket = cartProduct.AmountInBasket,
                Product = cartProduct.Product,
                Cart = cartProduct.Cart
            };

            _context.CartProducts.Add(newCartProduct);
            await _context.SaveChangesAsync();
        }

        public bool CartProductExists(int id)
        {
            return _context.CartProducts.Any(e => e.CartProductId == id);
        }

        public async Task<IList<CartProduct>> GetAllCartProductsAsync()
        {

            IList<CartProduct> cProducts = await _context.CartProducts
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .ToListAsync();

            return cProducts;
        }

        public async Task<CartProduct> GetCartProduct(int id)
        {
            return await _context.CartProducts.FirstOrDefaultAsync(cP => cP.CartProductId == id)
                ?? throw new ArgumentException($"{nameof(CartProduct)} wasn't found by id argument: {id}");
        }

        public async Task RemoveCartProduct(int id)
        {
            CartProduct cartproduct = await _context.CartProducts.FindAsync(id);

            if (cartproduct != null)
            {
                _context.CartProducts.Remove(cartproduct);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"{nameof(CartProduct)} with an id argument: {id}, cannot be removed because it doesn't exist");
            }
        }

        public async Task UpdateCartProduct(CartProductDto cartProduct, int id)
        {
            if (cartProduct == null)
                throw new ArgumentNullException($"{nameof(CartProductDto)} cannot be null");
            
            CartProduct cP = _context.CartProducts.FirstOrDefault(cP => cP.CartProductId == id)
                ?? throw new ArgumentException($"{nameof(CartProduct)} with an id: {id}, cannot be updated because it doesn't exist");

            cP.AmountInBasket = cartProduct.AmountInBasket;
            cP.Cart = cartProduct.Cart;
            cP.Product = cartProduct.Product;

            _context.Attach(cP).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}