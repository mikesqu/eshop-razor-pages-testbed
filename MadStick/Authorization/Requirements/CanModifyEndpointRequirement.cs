using Microsoft.AspNetCore.Authorization;

namespace MadStickWebAppTester.Authorization.Requirements
{
    public class CanModifyEndpointRequirement : IAuthorizationRequirement
    {
        public string Endpoint { get; set; }

        public CanModifyEndpointRequirement(string endpoint)
        {
            Endpoint = endpoint;
        }
    }
}
