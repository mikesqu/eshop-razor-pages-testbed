

using MadStick.VM;
using MadStickWebAppTester.Services;

namespace MadStick.Services
{
    public class FiltersService : IFiltersService
    {
        private readonly ILogger<FiltersService> _logger;
        public FiltersService(ILogger<FiltersService> logger)
        {
            _logger = logger;
        }

        public ProductFiltersModel GetProductFilters(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            ProductFiltersModel result = new ProductFiltersModel();

            result.CurrentSort = sortOrder;

            result.NameDescriptionSort = String.IsNullOrWhiteSpace(sortOrder) ? "name_description_desc" : "";
            result.PriceSort = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            result.CurrentFilter = searchString;

            return result;
        }

    }
}