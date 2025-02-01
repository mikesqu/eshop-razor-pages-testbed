using MadStick.Models.DataModel;
using MadStick.VM;
using MadStickWebAppTester.Dto;
using MadStickWebAppTester.Model;

namespace MadStickWebAppTester.Services
{
    public interface IProductService
    {
        Task<PaginatedList<MadStickProduct>> GetPagedProductsAsync(string searchString, string sortOrder,int? pageIndex);
        Task<IList<MadStickProduct>> GetAllProductsAsync();
        Task UpdateProduct(MadStickProductDto dto,int id);
        Task AddProductAsync(MadStickProductDto dto);
        Task<MadStickProduct> GetProductAsync(int id);
        Task<MadStickProduct> GetProductAsync(string slugName);
        Task DeleteProductAsync(int id);
        bool ProductExists(int id);
    }
}