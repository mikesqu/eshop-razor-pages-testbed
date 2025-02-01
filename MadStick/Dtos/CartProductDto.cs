using MadStick.Models.DataModel;
using MadStickWebAppTester.Data.UserEntity;

namespace MadStickWebAppTester.Dto
{
    public class CartProductDto
    {
        public int AmountInBasket { get; set; }
        public MadStickProduct? Product { get; set; }
        public Cart? Cart { get; set; }

    }
}
