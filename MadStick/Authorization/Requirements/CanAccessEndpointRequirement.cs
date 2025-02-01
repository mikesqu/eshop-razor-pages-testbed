using Microsoft.AspNetCore.Authorization;

namespace MadStickWebAppTester.Authorization.Requirements
{
    public class CanAccessEndpointRequirement : IAuthorizationRequirement
    {
        public string Endpoint { get; set; }
        public CanAccessEndpointRequirement(string endpoint)
        {
            Endpoint = endpoint;
        }
    }
}
