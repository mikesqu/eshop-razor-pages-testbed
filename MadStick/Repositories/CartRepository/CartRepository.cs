using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;
using Microsoft.EntityFrameworkCore;

namespace MadStick.Repositories {
    public class CartRepository : ICartRepository {
        private readonly ILogger<CartRepository> _logger;
        private readonly MadStickContext _context;
        public CartRepository(ILogger<CartRepository> logger, MadStickContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddCart(CartDto dto)
        {
            if(dto == null)
                throw new ArgumentNullException($"{nameof(CartDto)} cannot be null");

            if(dto.User == null)
                throw new ArgumentNullException("User property cannot be null");

            Cart newCart = new Cart() {
                User = dto.User,
                Products = dto.Products
            };

            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();
        }

        public bool CartExists(int id)
        {
            return _context.Carts.Any(c => c.CartId == id);
        }

        public async Task<IList<Cart>> GetAllCartsAsync()
        {
            return await _context.Carts.Include(c => c.User).ToListAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId)
                ?? throw new ArgumentException($"{nameof(Cart)} wasn't found by argument id: {cartId}");
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task RemoveCartAsync(int id)
        {
            Cart cartToRemove = _context.Carts.Where(c => c.CartId == id).FirstOrDefault()
                ?? throw new ArgumentException($"{nameof(Cart)} with an id: {id}, cannot be removed because it doesn't exist");

            _context.Carts.Remove(cartToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCart(CartDto cart, int id)
        {
            if(cart == null) 
                throw new ArgumentNullException($"{nameof(CartDto)} cannot be null");

            Cart c = _context.Carts.FirstOrDefault(c => c.CartId == id)
                ?? throw new ArgumentException($"{nameof(Cart)} with an id: {id}, cannot be updated because it doesn't exist");

            _context.Attach(c).State = EntityState.Modified;

            c.User = cart.User;
            c.Products = cart.Products;

            await _context.SaveChangesAsync();
        }
    }
}