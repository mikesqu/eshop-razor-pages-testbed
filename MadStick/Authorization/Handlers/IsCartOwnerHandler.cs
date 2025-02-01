using MadStickWebAppTester.Authorization.Requirements;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Services.PermissionsService;
using Microsoft.AspNetCore.Authorization;

namespace MadStickWebAppTester.Authorization.Handlers
{
    public class IsCartOwnerHandler : AuthorizationHandler<CanAccessEndpointRequirement,Cart>
    {
        private readonly IEndpointPermmissionsService _permService;

        public IsCartOwnerHandler(IEndpointPermmissionsService permService)
        {
            _permService = permService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanAccessEndpointRequirement requirement, Cart resource)
        {
            if (await _permService.CanAccessCart(context.User, resource))
            {
                context.Succeed(requirement);
            }
            return;
        }




        //public async Task HandleAsync(AuthorizationHandlerContext context)
        //{
        //    IList<IAuthorizationRequirement> pendingRequirements = context.PendingRequirements.ToList();

        //    foreach (IAuthorizationRequirement req in pendingRequirements)
        //    {
        //        IAuthorizationRequirement tempR = req;
        //        if (tempR is CanAccessEndpointRequirement rAccess)
        //        {
        //            if (await _permService.IsEndpointAccessAllowed(rAccess.Endpoint, context.User))
        //            {
        //                context.Succeed(rAccess);
        //            }
        //        }
        //        else if (tempR is CanModifyEndpointRequirement rModify)
        //        {
        //            if (await _permService.IsEndpointModificationAllowed(rModify.Endpoint, context.User))
        //            {
        //                context.Succeed(rModify);
        //            }

        //        }
        //    }
        //}
    }
}
