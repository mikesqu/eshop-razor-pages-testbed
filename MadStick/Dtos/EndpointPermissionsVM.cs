using MadStickWebAppTester.Data.UserEntity;

namespace MadStickWebAppTester.Model
{
    public class EndpointPermissionsVM
    {
        public int EndpointPermissionId { get; set; }
        public IList<Data.UserEntity.Endpoint>? Endpoints { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
