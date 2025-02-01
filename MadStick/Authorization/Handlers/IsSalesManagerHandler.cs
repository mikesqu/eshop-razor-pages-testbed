using MadStickWebAppTester.Authorization.Requirements;
using MadStickWebAppTester.Services.PermissionsService;
using Microsoft.AspNetCore.Authorization;

namespace MadStickWebAppTester.Authorization.Handlers
{
    public class IsSalesManagerHandler : IAuthorizationHandler
    {
        private readonly IEndpointPermmissionsService _permService;

        public IsSalesManagerHandler(IEndpointPermmissionsService permService)
        {
            _permService = permService;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            IList<IAuthorizationRequirement> pendingRequirements = context.PendingRequirements.ToList();

            foreach (IAuthorizationRequirement req in pendingRequirements)
            {
                IAuthorizationRequirement tempR = req;
                if (tempR is CanAccessEndpointRequirement rAccess)
                {
                    if (context.User.HasClaim(c => c.Type == "IsSalesManager"))
                    {
                        if (await _permService.IsEndpointAccessAllowed(rAccess.Endpoint,context.User))
                        {
                            context.Succeed(rAccess);
                        }
                    }
                }
                else if (tempR is CanModifyEndpointRequirement rModify)
                {
                    if (context.User.HasClaim(c => c.Type == "IsSalesManager"))
                    {
                        if (await _permService.IsEndpointModificationAllowed(rModify.Endpoint,context.User))
                        {
                            context.Succeed(rModify);
                        }
                    }
                }
            }
        }
    }
}
