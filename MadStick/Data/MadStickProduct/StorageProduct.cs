namespace MadStick.Models.DataModel { 
    public class StorageProduct
    {
        public int StorageProductId { get; set; }
        public MadStickProduct Product { get; set; }
        public int MadStickProductId { get; set; }
        public int AmountLeft { get; set; }
        public StorageUnit StorageUnit { get; set; }
        public int StorageUnitId { get; set; }
    }
}
