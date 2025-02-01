using MadStickWebAppTester.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadStick.Models.DataModel
{
    public class MadStickProduct
    {
        public int MadStickProductId { get; set; }
        public string Name { get; set; }
        public string? SlugName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IList<StorageProduct>? StorageProducts { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}