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
        #region ConfirmMailForAuthorization

        #region HttpGet ConfirmMailForAuthorization with empty viewModel
        /// <summary>
        /// HttpGet ConfirmMailForAuthorization with empty viewModel
        /// </summary>
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

            // Expected to have an error on required field
            var modelState = viewResult.ViewData.ModelState;
            Assert.False(modelState.IsValid);

            var errorsCount = viewResult.ViewData.ModelState.ErrorCount;
            Assert.Equal(1, errorsCount);

            //var fieldName = "MailAddress";
            //var modelStateEntry = viewResult.ViewData.ModelState[fieldName];
        }
        #endregion

        #region HttpPost ConfirmMailForAuthorization with wrong mailAddress

        /// <summary>
        /// HttpPost ConfirmMailForAuthorization with wrong mailAddress 
        /// </summary>
        [Fact]  
        public void ConfirmMailForAuthorization_withNonValidMailAddress_returns_ViewResult_errorWithNonValidMailAddress()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel();
            viewModel.MailAddress = "test";

            // Act
            var result = controller.ConfirmMailForAuthorization(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<ConfirmMailViewModel>(viewResult.ViewData.Model);
            Assert.Equal(viewModel, model);

            var modelState = viewResult.ViewData.ModelState;

            Assert.False(modelState.IsValid);
        }
        #endregion

        #region HttpPost ConfirmMailForAuthorization with right mailAddress format and get error with non existent user

        /// <summary>
        /// HttpPost ConfirmMailForAuthorization with right mailAddress got error with non existent user
        /// </summary>
        [Fact]
        public void ConfirmMailForAuthorization_with_rightMailAddress_returns_ViewResult_with_errorNonExistentUser()
        {

        }

        #endregion

        #endregion

        [Fact]
        public void ConfirmMailForRegistration_return_ViewResult()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel();

            // Act
            var result = controller.ConfirmMailForRegistration(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            
            // Expected to have an error on required field
            var errorsCount = viewResult.ViewData.ModelState.ErrorCount;
            Assert.Equal(1, errorsCount);
        }
    }
}
