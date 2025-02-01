using MadStick.Models.DataModel;

namespace MadStickWebAppTester.Dto
{
    public class StorageProductDto
    {
        public MadStickProduct? Product { get; set; }
        public int AmountLeft { get; set; }
        public StorageUnit? StorageUnit { get; set; }
    }
}