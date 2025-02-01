using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MadStickWebAppTester.Data;
using MadStick.Models.DataModel;
using Microsoft.AspNetCore.Authorization;
using MadStickWebAppTester.Services.ViewCans;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MadStickWebAppTester.Model;
using MadStickWebAppTester.Services;
using System.Security.Claims;
using MadStick.VM;

namespace MadStickWebAppTester.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        private readonly IFiltersService _filtersService;

        public IndexModel(ILogger<IndexModel> logger, IProductService productService,IFiltersService filtersService)
        {
            _logger = logger;
            _productService = productService;
            _filtersService = filtersService;
        }

        public ProductsVM VM { get; set; } = new ProductsVM();
        public async Task<IActionResult> OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            if(pageIndex == null) {
                pageIndex = 1;
            }

            VM.Filters = _filtersService.GetProductFilters(sortOrder,currentFilter,searchString,pageIndex);

            VM.MadStickProducts = await _productService.GetPagedProductsAsync(searchString,sortOrder,pageIndex.Value);
            
            //TODO: set view cans based on requiremnts
            VM.ViewCans = SetViewCans(this.User);

            return Page();
        }

        private ViewCansModel SetViewCans(ClaimsPrincipal user)
        {
            ViewCansModel result = new ViewCansModel();

            //TODO: set cans based on requirements

            if(user?.Identity?.IsAuthenticated == true){
                result.CanViewIndex = true;
                result.CanViewDetail = true;
                result.CanCreate = true;
                result.CanEdit = true;
                result.CanDelete = true;
            }else{
                result.CanViewIndex = true;
                result.CanViewDetail = true;
                result.CanCreate = true;
                result.CanEdit = true;
                result.CanDelete = true;
            }

            return result;
            
        }

        public class ProductsVM
        {
            public ViewCansModel ViewCans { get; set; } = new ViewCansModel();
            public PaginatedList<MadStickProduct> MadStickProducts { get; set; }
            public ProductFiltersModel Filters { get; set; }

        }

        public class ViewCansModel
        {
            public bool CanViewIndex { get; set; } = false;
            public bool CanViewDetail { get; set; } = false;
            public bool CanCreate { get; set; } = false;
            public bool CanEdit { get; set; } = false;
            public bool CanDelete { get; set; } = false;

        }
        
    }
}
