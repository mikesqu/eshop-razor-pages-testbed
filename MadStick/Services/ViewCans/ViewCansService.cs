using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MadStickWebAppTester.Services.ViewCans
{
    public class ViewCansService  //: IViewCansService
    {
        //private readonly IAuthorizationService _authService;

        //public ViewCansService(IAuthorizationService authService)
        //{
        //    _authService = authService;
        //}

        //public List<ClaimsPagePermissions> LoadPagePermissions()
        //{
        //    List<ClaimsPagePermissions> pagePermissions = new List<ClaimsPagePermissions>();


        //    ClaimsPagePermissions pagePermissionsAdmin = new ClaimsPagePermissions();
        //    pagePermissionsAdmin.ClaimName = "IsAdmin";
        //    IDictionary<string, bool> productsPathsAdmin = new Dictionary<string, bool>();
        //    //absolute paths
        //    productsPathsAdmin.Add("/Products/Index", true);
        //    productsPathsAdmin.Add("/Products/Details", true);
        //    productsPathsAdmin.Add("/Products/Create", true);
        //    productsPathsAdmin.Add("/Products/Edit", true);
        //    productsPathsAdmin.Add("/Products/Delete", true);

        //    productsPathsAdmin.Add("/Products/Index", true);
        //    productsPathsAdmin.Add("/Products/Details", true);
        //    productsPathsAdmin.Add("/Products/Create", true);
        //    productsPathsAdmin.Add("/Products/Edit", true);
        //    productsPathsAdmin.Add("/Products/Delete", true);

        //    productsPathsAdmin.Add("/Products/Index", true);
        //    productsPathsAdmin.Add("/Products/Details", true);
        //    productsPathsAdmin.Add("/Products/Create", true);
        //    productsPathsAdmin.Add("/Products/Edit", true);
        //    productsPathsAdmin.Add("/Products/Delete", true);

        //    productsPathsAdmin.Add("/Products/Index", true);
        //    productsPathsAdmin.Add("/Products/Details", true);
        //    productsPathsAdmin.Add("/Products/Create", true);
        //    productsPathsAdmin.Add("/Products/Edit", true);
        //    productsPathsAdmin.Add("/Products/Delete", true);

        //    pagePermissionsAdmin.PagePaths = productsPathsAdmin;


        //    ClaimsPagePermissions pagePermissionsSalesManager = new ClaimsPagePermissions();
        //    pagePermissions.ClaimName = "IsSalesManager";
        //    IDictionary<string, bool> productsPathsAdmin = new Dictionary<string, bool>();
        //    //absolute paths
        //    productsPathsAdmin.Add("/Products/Index", true);
        //    productsPathsAdmin.Add("/Products/Details", true);
        //    productsPathsAdmin.Add("/Products/Create", true);
        //    productsPathsAdmin.Add("/Products/Edit", true);
        //    productsPathsAdmin.Add("/Products/Delete", true);
        //    pagePermissionsSalesManager.PagePaths = productsPathsAdmin;


        //    pagePermissions.Add(pagePermissionsAdmin);
        //    pagePermissions.Add(pagePermissionsSalesManager);

        //}

        //public LoadUnclaimedPagePermissions()
        //{
        //    ClaimsPagePermissions pagePermissionsStandardUser = new ClaimsPagePermissions();
        //    pagePermissionsStandardUser.ClaimName = "IsStandardUser";

        //    IDictionary<string, bool> productsPathsAdmin = new Dictionary<string, bool>();

        //    //absolute paths
        //    productsPathsAdmin.Add("/Products/Index", true);
        //    productsPathsAdmin.Add("/Products/Details", true);
        //    productsPathsAdmin.Add("/Products/Create", true);
        //    productsPathsAdmin.Add("/Products/Edit", true);
        //    productsPathsAdmin.Add("/Products/Delete", true);

        //    pagePermissionsStandardUser.PagePaths = productsPathsAdmin;

        //    ClaimsPagePermissions pagePermissionsUnauthenticated = new ClaimsPagePermissions();
        //    pagePermissionsUnauthenticated.ClaimName = "Unauthenticated";

        //    IDictionary<string, bool> productsPathsAdmin = new Dictionary<string, bool>();

        //    //absolute paths
        //    productsPathsAdmin.Add("/Products/Index", true);
        //    productsPathsAdmin.Add("/Products/Details", true);
        //    productsPathsAdmin.Add("/Products/Create", true);
        //    productsPathsAdmin.Add("/Products/Edit", true);
        //    productsPathsAdmin.Add("/Products/Delete", true);

        //    pagePermissionsUnauthenticated.PagePaths = productsPathsAdmin;
        //}

        //public async Task<bool> CanCreate(ClaimsPrincipal user, string pagePath)
        //{

        //    AuthorizationResult IsAdmin = await _authService.AuthorizeAsync(user, "IsAdmin");
        //    if (IsAdmin.Succeeded)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        AuthorizationResult IsSalesManager = await _authService.AuthorizeAsync(user, "IsSalesManager");
        //        if (IsSalesManager.Succeeded)
        //        {
        //            object permissions = null;
        //            //Determine if the specific page is allowed for IsSalesManager
        //            if (permissions.Users.First("SalesManager").Pages.First(pagePath).IsAllowed)
        //                return true;
        //        }
        //    }



        //}

        //public async Task<bool> CanDelete(ClaimsPrincipal user)
        //{
        //    if (policyName == "IsAdmin")
        //    {
        //        AuthorizationResult IsAdmin = await _authService.AuthorizeAsync(User, "IsAdmin");
        //    }
        //}

        //public async Task<bool> CanEdit(ClaimsPrincipal user)
        //{
        //    if (policyName == "IsAdmin")
        //    {
        //        AuthorizationResult IsAdmin = await _authService.AuthorizeAsync(User, "IsAdmin");
        //    }
        //}

        //public async Task<bool> CanViewDetails(ClaimsPrincipal user)
        //{
        //    if (policyName == "IsAdmin")
        //    {
        //        AuthorizationResult IsAdmin = await _authService.AuthorizeAsync(User, "IsAdmin");
        //    }
        //}

        //public async Task<bool> CanViewIndex(ClaimsPrincipal user)
        //{
        //    if (policyName == "IsAdmin")
        //    {
        //        AuthorizationResult IsAdmin = await _authService.AuthorizeAsync(User, "IsAdmin");
        //    }
        //}
    }
}
