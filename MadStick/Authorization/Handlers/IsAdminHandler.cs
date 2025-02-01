using MadStickWebAppTester.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace MadStickWebAppTester.Authorization.Handlers
{
    internal class IsAdminHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            IList<IAuthorizationRequirement> pendingRequirements = context.PendingRequirements.ToList();

            foreach (IAuthorizationRequirement req in pendingRequirements)
            {
                IAuthorizationRequirement r = req;

                if (r is CanAccessEndpointRequirement)
                {
                    if (context.User.HasClaim(c => c.Type == "IsAdmin"))
                    {
                        context.Succeed(r);
                    }
                }
                else if (r is CanModifyEndpointRequirement)
                {
                    if (context.User.HasClaim(c => c.Type == "IsAdmin"))
                    {
                        context.Succeed(r);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}