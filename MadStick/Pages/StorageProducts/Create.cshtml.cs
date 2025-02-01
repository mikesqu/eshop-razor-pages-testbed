using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MadStick.Models.DataModel;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.StorageProducts
{
    [Authorize("CanModifyStorageProducts")]
    public class CreateModel : PageModel
    {
        private readonly IStorageService _storageService;
        private readonly IProductService _productService;

        public CreateModel(IStorageService storageService, IProductService productService)
        {
            _storageService = storageService;
            _productService = productService;
        }

        public IList<StorageProduct> StorageProducts { get; set; } = null!;

        [BindProperty]
        public StorageProductDto StorageProductDto { get; set; } = null!;
        public StorageProduct StorageProduct { get; set; }
        public ViewListsModel ViewLists { get; set; } = new ViewListsModel();

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StorageProducts = await _storageService.GetAllStorageProductsAsync();

            IList<MadStickProduct> products = await _productService.GetAllProductsAsync();
            IList<StorageUnit> storageUnits = await _storageService.GetAllStorageUnitsAsync();

            ViewLists.Products = GetViewList(products);
            ViewLists.StorageUnits = GetViewList(storageUnits);

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _storageService.AddStorageProduct(StorageProductDto);

            return RedirectToPage("Index");
        }

        public class ViewListsModel
        {
            public IEnumerable<SelectListItem> Products { get; set; }
            public IEnumerable<SelectListItem> StorageUnits { get; set; }

        }
        private IEnumerable<SelectListItem> GetViewList<T>(IList<T> list)
        {
            return GetEnumerable(list);
        }

        private static IEnumerable<SelectListItem> GetEnumerable<T>(IList<T> list)
        {
            IList<SelectListItem> result = new List<SelectListItem>();

            foreach (var item in list)
            {
                if (item is MadStickProduct)
                {
                    MadStickProduct product = item as MadStickProduct;

                    if (product != null)
                    {
                        string text = $"Name: {product.Name}-{product.MadStickProductId}";
                        result.Add(new SelectListItem() { Text = text, Value = product.MadStickProductId.ToString() });
                    }

                }
                else if (item is StorageUnit)
                {
                    StorageUnit sUnit = item as StorageUnit;
                    if (sUnit != null)
                    {
                        string text = $"Name: {sUnit.Name} Id: {sUnit.StorageUnitId}";
                        result.Add(new SelectListItem() { Text = text, Value = sUnit.StorageUnitId.ToString() });
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return result;
        }
    }
}
