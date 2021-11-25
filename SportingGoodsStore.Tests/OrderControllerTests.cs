using Microsoft.AspNetCore.Mvc;
using Moq;
using SportingGoodsStore.Controllers;
using SportingGoodsStore.EF;
using SportingGoodsStore.Models;
using Xunit;

namespace SportingGoodsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void CanNotCheckoutEmptyCart()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            // Act
            ViewResult result = target.Checkout(order) as ViewResult;

            // Assert
            mock.Verify(m=>m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CannotCheckoutInvalidShippingDetails()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            // Act
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // Assert
            mock.Verify(m=>m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CanCheckoutAndSubmitOrder()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);

            // Act
            RedirectToPageResult result = target.Checkout(new Order()) as RedirectToPageResult;

            // Assert
            mock.Verify(m=>m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("/Completed", result.PageName);
        }
    }
}