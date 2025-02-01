using MadStick.Models.DataModel;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Services
{
    public interface IStorageService
    {
        //storage product
        Task AddStorageProduct(StorageProductDto storageProduct);
        Task DeleteStorageProduct(int? id);
        Task EditStorageProduct(StorageProductDto storageProduct,int id);
        Task<IList<StorageProduct>> GetAllStorageProductsAsync();
        Task<StorageProduct> GetStorageProductAsync(int? id);
        bool StorageProductExists(int id);
        //storage unit
        Task<IList<StorageUnit>> GetAllStorageUnitsAsync();
        Task AddStorageUnitAsync(StorageUnitDto storageUnit);
        Task DeleteStorageUnit(int? id);
        Task<StorageUnit> GetStorageUnitAsync(int? id);
        bool StorageUnitExists(int id);
        Task EditStorageUnit(StorageUnitDto storageUnit,int id);
    }
}