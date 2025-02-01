using System.ComponentModel.DataAnnotations.Schema;

namespace MadStickWebAppTester.Data.UserEntity
{
    public class Cart
    {
        public int CartId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public IList<CartProduct>? Products { get; set; }

    }
}
