using MadStickWebAppTester.Data.UserEntity;

namespace MadStickWebAppTester.Dto
{
    public class CartDto
    {
        public ApplicationUser? User { get; set; }
        public IList<CartProduct>? Products { get; set; }
    }
}
