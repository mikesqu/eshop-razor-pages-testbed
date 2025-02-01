using MadStick.Models.DataModel;

namespace MadStickWebAppTester.Dto
{
    public class MadStickProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? SlugName { get; set; }
        public double Price { get; set; }
        public IList<StorageProduct>? StorageProducts { get; set; }
        public bool IsDeleted { get; set; }
    }
}
