using MadStick.Models.DataModel;
using MadStick.VM;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Dto;
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MadStick.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly MadStickContext _context;
        private readonly IConfiguration _configuration;
        public ProductService(ILogger<ProductService> logger, MadStickContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public async Task AddProductAsync(MadStickProductDto dto)
        {
            MadStickProduct p = new MadStickProduct() {
                Name = dto.Name,
                Description = dto.Description,
                SlugName = dto.SlugName,
                Price = dto.Price,
                StorageProducts = dto.StorageProducts,
                IsDeleted = dto.IsDeleted
            };

            _context.MadStickProducts.Add(p);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteProductAsync(int id)
        {
            MadStickProduct p = _context.MadStickProducts.Where(p => p.MadStickProductId == id).FirstOrDefault();

            _context.MadStickProducts.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<MadStickProduct>> GetPagedProductsAsync(string searchString, string sortOrder, int? pageIndex)
        {
            
            IQueryable<MadStickProduct> productsIQ = from p in _context.MadStickProducts
                                                     where p.IsDeleted == false
                                                     select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                productsIQ = productsIQ.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_description_desc":
                    productsIQ = productsIQ.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    productsIQ = productsIQ.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    productsIQ = productsIQ.OrderByDescending(p => p.Price);
                    break;
                default:
                    productsIQ = productsIQ.OrderBy(p => p.Name);
                    break;
            }


            var pageSize = _configuration.GetValue("PageSize", 4);

            //Example of an sqlinjection
            // string sqlInjection = "'; INSERT INTO `madstickwebapptester-2`.madstickproducts" +
            // " (Name,Description,Price,IsDeleted) VALUES ('injected2','very bad 2',4200,false) 
            // -- ";
            // string sqlParam = "ford f-450";

            // var result = _context.MadStickProducts
            // .FromSqlRaw("SELECT * FROM MadStickProducts" + 
            // " WHERE Name='" + sqlInjection + "'").ToList();


            return await PaginatedList<MadStickProduct>.CreateAsync(productsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public async Task<MadStickProduct> GetProductAsync(int id)
        {
            return await _context.MadStickProducts.Where(p => p.MadStickProductId == id).FirstAsync();
        }


        public bool ProductExists(int id)
        {
            return _context.MadStickProducts.Any(p => p.MadStickProductId == id);

            
        }

        public async Task UpdateProduct(MadStickProductDto dto, int id)
        {
            MadStickProduct p = _context.MadStickProducts.Where(p => p.MadStickProductId == id).FirstOrDefault();

            _context.Attach(p).State = EntityState.Modified;

            p.Name = dto.Name;
            p.Description = dto.Description;
            p.SlugName = dto.SlugName;
            p.Price = dto.Price;
            p.StorageProducts = dto.StorageProducts;
            p.IsDeleted = dto.IsDeleted;

            await _context.SaveChangesAsync();

        }


        public async Task<IList<MadStickProduct>> GetAllProductsAsync()
        {
            return await _context.MadStickProducts.ToListAsync();
        }

        public async Task<MadStickProduct> GetProductAsync(string slugName)
        {
            return await _context.MadStickProducts.Where(p => p.SlugName == slugName).FirstOrDefaultAsync();
        }
    }
}