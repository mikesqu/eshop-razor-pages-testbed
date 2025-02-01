using MadStick.Models.DataModel;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Dto;
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Services;
using Microsoft.EntityFrameworkCore;

namespace MadStick.Services
{
    public class StorageService : IStorageService
    {
        private readonly ILogger<StorageService> _logger;
        private readonly MadStickContext _context;
        private readonly IConfiguration _configuration;
        public StorageService(ILogger<StorageService> logger, MadStickContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public async Task AddStorageProduct(StorageProductDto dto)
        {
            StorageProduct sP = new StorageProduct()
            {
                Product = dto.Product,
                AmountLeft = dto.AmountLeft,
                StorageUnit = dto.StorageUnit
            };

            _context.StorageProducts.Add(sP);
            await _context.SaveChangesAsync();
        }

        public async Task AddStorageUnitAsync(StorageUnitDto dto)
        {
            StorageUnit sU = new StorageUnit()
            {
                Name = dto.Name,
                Location = dto.Location,
                StorageProducts = dto.StorageProducts
            };

            _context.StorageUnits.Add(sU);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteStorageProduct(int? id)
        {
            StorageProduct sP = _context.StorageProducts.Where(sP => sP.StorageProductId == id).FirstOrDefault();

            _context.StorageProducts.Remove(sP);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStorageUnit(int? id)
        {
            StorageUnit sU = _context.StorageUnits.Where(sP => sP.StorageUnitId == id).FirstOrDefault();

            _context.StorageUnits.Remove(sU);
            await _context.SaveChangesAsync();
        }

        public async Task EditStorageProduct(StorageProductDto dto, int id)
        {
            StorageProduct sP = _context.StorageProducts.Where(sP => sP.StorageProductId == id).FirstOrDefault();

            _context.Attach(sP).State = EntityState.Modified;
            
            sP.Product = dto.Product;
            sP.AmountLeft = dto.AmountLeft;
            sP.StorageUnit = dto.StorageUnit;

            await _context.SaveChangesAsync();
        }

        public async Task EditStorageUnit(StorageUnitDto dto, int id)
        {
            StorageUnit sU = _context.StorageUnits.Where(sU => sU.StorageUnitId == id).FirstOrDefault();

            _context.Attach(sU).State = EntityState.Modified;
            
            sU.Name = dto.Name;
            sU.Location = dto.Location;
            sU.StorageProducts = dto.StorageProducts;

            await _context.SaveChangesAsync();
        }

        public async Task<IList<StorageProduct>> GetAllStorageProductsAsync()
        {
            return await _context.StorageProducts.Include(sP => sP.Product).Include(sP => sP.StorageUnit).ToListAsync();
        }

        public async Task<IList<StorageUnit>> GetAllStorageUnitsAsync()
        {
            return await _context.StorageUnits.ToListAsync();
        }

        public bool StorageProductExists(int id)
        {
            return _context.StorageProducts.Any(sP => sP.StorageProductId == id);
        }

        public bool StorageUnitExists(int id)
        {
            return _context.StorageUnits.Any(sP => sP.StorageUnitId == id);
        }

        public async Task<StorageProduct> GetStorageProductAsync(int? id)
        {
            return await _context.StorageProducts.Where(sP => sP.StorageProductId == id).FirstOrDefaultAsync();
        }

        public async Task<StorageUnit> GetStorageUnitAsync(int? id)
        {
            return await _context.StorageUnits.Where(sP => sP.StorageUnitId == id).FirstOrDefaultAsync();
        }
    }
}