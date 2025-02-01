using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MadStickWebAppTester.Data.UserEntity
{
    public class ApplicationUser : IdentityUser
    {
        
        public Cart? Cart { get; set; } 
        public IList<IdentityUserClaim<string>> Claims { get; set; }
        public EndpointPermission? EndpointPermissions { get; set; }
    }
}