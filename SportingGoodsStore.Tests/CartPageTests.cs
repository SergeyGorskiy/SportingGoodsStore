using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportingGoodsStore.EF;
using SportingGoodsStore.Models;
using SportingGoodsStore.Pages;
using Xunit;

namespace SportingGoodsStore.Tests
{
    public class CartPageTests
    {
        [Fact]
        public void CanLoadCart()
        {
            // Arrange
            Product p1 = new Product{ ProductId = 1, Name = "P1" };
            Product p2 = new Product{ ProductId = 2, Name = "P2" };

            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[] {p1, p2}).AsQueryable<Product>());

            Cart testCart = new Cart();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            // Act
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnGet("myUrl");

            // Assert
            Assert.Equal(2, cartModel.Cart.Lines.Count);
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        [Fact]
        public void CanUpdateCart()
        {
            // Arrange
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products)
                .Returns((new Product[]
                {
                    new Product
                    {
                        ProductId = 1, Name = "P1"
                    }
                }).AsQueryable<Product>);

            Cart testCart = new Cart();

            // Act
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnPost(1, "myUrl");

            // Assert
            Assert.Single(testCart.Lines);
            Assert.Equal("P1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Quantity);
        }
    }
}