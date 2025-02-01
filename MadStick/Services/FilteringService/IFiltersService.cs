using MadStick.VM;

namespace MadStickWebAppTester.Services
{
    public interface IFiltersService
    {
        ProductFiltersModel GetProductFilters(string sortOrder, string currentFilter, string searchString, int? pageIndex);
    }
}