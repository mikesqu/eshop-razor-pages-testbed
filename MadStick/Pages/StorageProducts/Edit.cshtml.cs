
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MadStick.Models.DataModel;
using MadStickWebAppTester.Model;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Pages.StorageProducts
{
    [Authorize("CanModifyStorageProducts")]
    public class EditModel : PageModel
    {
        private readonly IStorageService _storageService;
        private readonly IProductService _productService;
        
        public EditModel(IStorageService storageService,IProductService productService)
        {
            _storageService = storageService;
            _productService = productService;
        }

        public ViewListsModel ViewLists { get; set; } = new ViewListsModel();

        [BindProperty]
        public StorageProductDto StorageProductDto { get; set; } 
        public StorageProduct StorageProduct { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StorageProduct = await _storageService.GetStorageProductAsync(id);

            if (StorageProduct == null)
            {
                return NotFound();
            }

            IList<MadStickProduct> products = await _productService.GetAllProductsAsync();
            IList<StorageUnit> storageUnits = await _storageService.GetAllStorageUnitsAsync();
            
            ViewLists.Products = GetViewList(products);
            ViewLists.StorageUnits = GetViewList(storageUnits);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPost(int? id)
        {
            if (!ModelState.IsValid || id == null)
            {
                return Page();
            }

            await _storageService.EditStorageProduct(StorageProductDto,id.Value);

            return RedirectToPage("./Index");
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

        private bool StorageProductExists(int id)
        {
            return _storageService.StorageProductExists(id);
        }
    }
}
