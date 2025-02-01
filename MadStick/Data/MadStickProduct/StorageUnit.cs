namespace MadStick.Models.DataModel { 
    public class StorageUnit
    {
        public int StorageUnitId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public IList<StorageProduct>? StorageProducts { get; set; }
    }
}
