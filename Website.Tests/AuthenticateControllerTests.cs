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

        #region ConfirmMailForAuthorization with empty viewModel
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

        #region ConfirmMailForAuthorization with nonvalid mail address

        /// <summary>
        /// HttpPost ConfirmMailForAuthorization with nonvalid mail address
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

        #region ConfirmMailForAuthorization with right mailAddress format and get error with non existent user

        /// <summary>
        /// HttpPost ConfirmMailForAuthorization with right mailAddress got error with non existent user
        /// </summary>
        [Fact]
        public void ConfirmMailForAuthorization_with_rightMailAddress_returns_ViewResult_with_errorNonExistentUser()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel();
            viewModel.MailAddress = "test@mail.ru";

            mockService.Setup(s => s.IsUserExistAsync(viewModel.MailAddress)).Returns(() => null);

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

        #region ConfirmMailForAuthorization with right mailAddress format and get redirect to password input site

        /// <summary>
        /// HttpPost ConfirmMailForAuthorization with right mailAddress got error with non existent user
        /// </summary>
        [Fact]
        public void ConfirmMailForAuthorization_with_rightMailAddress_returns_ViewResult_for_passwordConfirm()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel
            {
                MailAddress = "test@mail.ru"
            };
            //mockService.Setup(s => s.IsUserExistAsync(viewModel.MailAddress)).Returns(() =>
            //{
            //    return new Task<User>(() => new User());
            //});

            // Act
            var result = controller.ConfirmMailForAuthorization(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<AuthorizationViewModel>(viewResult.ViewData.Model);
            Assert.Equal(viewModel, model);

            var modelState = viewResult.ViewData.ModelState;

            Assert.False(modelState.IsValid);
        }

        #endregion

        #endregion

        #region ConfirmMailForRegistration
        
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

        
        #region ConfirmMailForAuthorization with nonvalid mail address

        /// <summary>
        /// ConfirmMailForAuthorization with nonvalid mail address
        /// </summary>
        [Fact]  
        public void ConfirmMailForRegistration_withNonValidMailAddress_returns_ViewResult_errorWithNonValidMailAddress()
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

        #endregion

        #region EnterPassword for authorization

        [Fact]
        public void EnterPassword_with_noMail_returns_ConfirmMailForAuthorization()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new AuthorizationViewModel();

            // Act
            var result = controller.EnterPassword(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);

            Assert.IsType<ConfirmMailViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName=="ConfirmMailForAuthorization");
        }
        #endregion

        #region Enter user info for registration
        
        [Fact]
        public void EnterUserInfo_with_noMail_returns_ConfirmMailForRegistration()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new RegistrationViewModel();

            // Act
            var result = controller.EnterUserInfo(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);

            Assert.IsType<ConfirmMailViewModel>(viewResult.Model);
            Assert.True(viewResult.ViewName=="ConfirmMailForRegistration");
        }

        #endregion
    }
}
