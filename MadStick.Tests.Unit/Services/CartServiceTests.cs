using MadStick.Models.DataModel;
using MadStick.Repositories;
using MadStick.Services;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MadStick.Tests.Unit;

public class CartServiceTests
{
    //
    //move appropriat logic(page handlers shouldn't be injected with dbcontexts) from page handlers and into services and test them here
    //
    [Fact]
    public async Task AddProduct_WithValidId_ItemAddedToCart()
    {
        Mock<ILogger<CartService>> loggerMock = new Mock<ILogger<CartService>>();
        Mock<ICartRepository> cartRepoMock = new Mock<ICartRepository>();
        Mock<ICartProductRepository> cartProductRepoMock = new Mock<ICartProductRepository>();
        
        CartService sut = new CartService(loggerMock.Object,cartRepoMock.Object,cartProductRepoMock.Object);

        CartProductDto cProduct = new CartProductDto() {
            Product = new MadStickProduct() {
                MadStickProductId = 1
            },
            AmountInBasket = 2,
            Cart = new Cart() {
                CartId = 1,
            }
        };

        await sut.AddCartProductAsync(cProduct);



    }

    [Fact]
    public void UpdateCartProduct_WithValidId_Returns0()
    {
        
    }

}