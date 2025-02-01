namespace MadStickWebAppTester.Data.UserEntity
{
    public class EndpointPermission
    {
        public int EndpointPermissionId { get; set; }
        public IList<Endpoint> Endpoints { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
    }
}