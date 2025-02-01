using MadStick.Models.DataModel;
using MadStickWebAppTester.Dto;

namespace MadStickWebAppTester.Data.UserEntity
{
    public class CartProduct
    {
        public int CartProductId { get; set; }
        public int AmountInBasket { get; set; }
        public int MadStickProductId { get; set; }
        public MadStickProduct Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }

    }
}