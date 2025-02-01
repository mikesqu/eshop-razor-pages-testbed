namespace MadStickWebAppTester.Data.UserEntity
{
    public class Endpoint
    {
        public int EndpointId { get; set; }
        public string Value { get; set; }
        public bool IsAllowedAccess { get; set; }
        public bool IsAllowedModification { get; set; }
        public EndpointPermission Permission { get; set; }
        public int EndpointPermissionId { get; set; }
    }
}