using Microsoft.AspNetCore.Mvc;
using Moq;
using Website.Controllers;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;
using Xunit;

namespace Website.Tests
{
    public class AuthenticateControllerTests
    {
        [Fact]
        public void ConfirmMailForAuthorization_returns_ViewResult()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel();
            // Act
            var result = controller.ConfirmMailForAuthorization(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<ConfirmMailViewModel>(viewResult.ViewData.Model);
            Assert.Equal(viewModel, model);

            // Expected to have error on required field
            var errorsCount = viewResult.ViewData.ModelState.ErrorCount;
            Assert.Equal(1, errorsCount);

            var fieldName = "MailAddress";
            var modelStateEntry = viewResult.ViewData.ModelState[fieldName];
            //Assert.Equal(false, modelStateEntry);
        }

        [Fact]
        public void test()
        {
            Assert.Equal(1, 1);
        }
    }
}
