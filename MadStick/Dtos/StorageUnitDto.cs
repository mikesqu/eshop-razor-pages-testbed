using MadStick.Models.DataModel;

namespace MadStickWebAppTester.Dto
{
    public class StorageUnitDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public IList<StorageProduct>? StorageProducts { get; set; }
    }
}