using MadStick.Repositories;
using MadStickWebAppTester.Data;
using MadStickWebAppTester.Data.UserEntity;
using MadStickWebAppTester.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MadStick.Tests.Unit;

public class CartRepositoryTests
{
    private readonly MadStickContext _context;
    public CartRepositoryTests()
    {
        DbContextOptions<MadStickContext> dbContextOptions = new DbContextOptionsBuilder<MadStickContext>()
        .UseInMemoryDatabase("MadStick").Options;

        _context = new MadStickContext(dbContextOptions);
    }
    //
    //move appropriat logic(page handlers shouldn't be injected with dbcontexts) from page handlers and into services and test them here
    //

    /*
        check if new cart is added with the expected user
    */
    [Fact]
    public async Task AddCart_ProvidedWithId_DatabaseHasNewRecordWithThatId()
    {
        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        ApplicationUser user = new ApplicationUser();
        string userId = "1234";
        user.Id = userId;

        CartDto dto = new CartDto()
        {
            User = user,
            Products = null
        };

        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(AddCart_ProvidedWithId_DatabaseHasNewRecordWithThatId))
                .Options;

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            await sut.AddCart(dto);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);

            Cart actualCart = await sut.GetCartByUserIdAsync(userId);
            string newCartUserId = actualCart.UserId;
            int actualCartCount = (await sut.GetAllCartsAsync()).Count;

            Assert.Equal(userId, newCartUserId);
            Assert.Equal(1, actualCartCount);
        }
    }

    /*
        check if nulls for id are handled
    */
    [Fact]
    public async Task AddCart_ProvidedWithNullUser_ThrowsArgumentNullException()
    {
        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        CartDto dto = new CartDto()
        {
            User = null,
            Products = null
        };

        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(AddCart_ProvidedWithNullUser_ThrowsArgumentNullException))
                .Options;

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
        
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.AddCart(dto));
        }

    }

    /*
        check if new cart cannot be added with the user that already has a cart
    */
    [Fact]
    public async Task AddCart_ProvidedUserWhoAlreadyHasACart_Throws()
    {
        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        ApplicationUser user = new ApplicationUser();
        string userId = "1234";
        user.Id = userId;

        CartDto dto = new CartDto()
        {
            User = user,
            Products = null
        };


        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(AddCart_ProvidedUserWhoAlreadyHasACart_Throws))
                .Options;

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            await sut.AddCart(dto);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);

            Cart actualCart = await sut.GetCartByUserIdAsync(userId);
            string newCartUserId = actualCart.UserId;
            int actualCartCount = (await sut.GetAllCartsAsync()).Count;

            Assert.Equal(userId, newCartUserId);
            Assert.Equal(1, actualCartCount);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddCart(dto));
        }

    }

    //CartExists
    [Fact]
    public async Task CartExists_ProvidedWithExistingId_ReturnsTrue()
    {
        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        ApplicationUser user = new ApplicationUser();
        string userId = "1234";
        user.Id = userId;

        CartDto dto = new CartDto()
        {
            User = user,
            Products = null
        };

        int actualCartId = -1;

        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(CartExists_ProvidedWithExistingId_ReturnsTrue))
                .Options;

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            await sut.AddCart(dto);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);

            Cart actualCart = await sut.GetCartByUserIdAsync(userId);
            actualCartId = actualCart.CartId;
            string newCartUserId = actualCart.UserId;
            int actualCartCount = (await sut.GetAllCartsAsync()).Count;

            Assert.Equal(userId, newCartUserId);
            Assert.Equal(1, actualCartCount);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            Assert.True(sut.CartExists(actualCartId));
        }
    }

    [Fact]
    public void CartExists_WhenDBDoesnContainCarts_ReturnsFalse()
    {

        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(CartExists_WhenDBDoesnContainCarts_ReturnsFalse))
                .Options;

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            Assert.False(sut.CartExists(1));
        }
    }

    [Fact]
    public async Task CartExists_WhenDBDoesnContainCartWithId_ReturnsFalse()
    {
        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        ApplicationUser user = new ApplicationUser();
        string userId = "1234";
        user.Id = userId;

        CartDto dto = new CartDto()
        {
            User = user,
            Products = null
        };

        int nonExistingCartId = 782;

        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(CartExists_WhenDBDoesnContainCartWithId_ReturnsFalse))
                .Options;

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            await sut.AddCart(dto);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);

            Cart actualCart = await sut.GetCartByUserIdAsync(userId);

            string newCartUserId = actualCart.UserId;
            int actualCartCount = (await sut.GetAllCartsAsync()).Count;

            Assert.Equal(userId, newCartUserId);
            Assert.Equal(1, actualCartCount);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            Assert.False(sut.CartExists(nonExistingCartId));
        }

    }


    //GetAllCarts
    [Fact]
    public async Task GetAllCarts_WhenDatabaseContainsThreeCarts_ReturnsThreeCarts()
    {
        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        ApplicationUser user1 = new ApplicationUser();
        ApplicationUser user2 = new ApplicationUser();
        ApplicationUser user3 = new ApplicationUser();

        string firstUserId = "88";
        int expectedCount = 3;

        user1.Id = firstUserId;
        user2.Id = "2";
        user3.Id = "3";

        CartDto cartDto1 = new CartDto()
        {
            User = user1,
            Products = null
        };

        CartDto cartDto2 = new CartDto()
        {
            User = user2,
            Products = null
        };

        CartDto cartDto3 = new CartDto()
        {
            User = user3,
            Products = null
        };

        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllCarts_WhenDatabaseContainsThreeCarts_ReturnsThreeCarts))
                .Options;

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            await sut.AddCart(cartDto1);
        }

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            await sut.AddCart(cartDto2);
        }

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            await sut.AddCart(cartDto3);
        }

        // Use a separate instance of the context to verify correct data was saved to database
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);

            IList<Cart> actualCarts = await sut.GetAllCartsAsync();

            Assert.Equal(expectedCount, actualCarts.Count);
            Assert.Equal(actualCarts.First().UserId, firstUserId);
        }

    }

    [Fact]
    public async Task GetAllCarts_WhenDatabaseContainsNoCarts_ReturnsEmptyList()
    {
        Mock<ILogger<CartRepository>> logger = new Mock<ILogger<CartRepository>>();

        int expectedCartCount = 0;

        var options = new DbContextOptionsBuilder<MadStickContext>()
                .UseInMemoryDatabase(databaseName: nameof(GetAllCarts_WhenDatabaseContainsNoCarts_ReturnsEmptyList))
                .Options;

        // Run the test against one instance of the context
        using (var context = new MadStickContext(options))
        {
            CartRepository sut = new CartRepository(logger.Object, context);
            
            Assert.Equal(expectedCartCount, (await sut.GetAllCartsAsync()).Count);
        }

    }

    //GetCartById
    [Fact]
    public void GetCartByIdAsync_WhenDatabaseContainsCartWithId_ReturnsExpectedCart()
    {


    }

    [Fact]
    public void GetCartByIdAsync_WhenDatabaseContainsNoCarts_ThrowsException()
    {


    }

    //RemoveCart

    [Fact]
    public void RemoveCartAsync_WhenDatabaseContainsCartWithId_CartRepoThrowsExceptionWhenCalledWithGetCartById()
    {


    }

    [Fact]
    public void RemoveCartAsync_WhenDatabaseContainsNoCarts_ThrowsException()
    {


    }

    //UpdateCart

    [Fact]
    public void UpdateCart_WhenDatabaseContainsCartWithId_CartRepoContainsUpdatedCart()
    {


    }

    [Fact]
    public void UpdateCart_WhenDatabaseContainsNoCarts_ThrowsException()
    {


    }




}