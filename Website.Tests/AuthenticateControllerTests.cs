using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Website.Controllers;
using Services.Interfaces;
using Website.ViewModels;
using Xunit;

namespace Website.Tests
{
    public class AuthenticateControllerTests
    {
        #region ConfirmMailForAuthorization
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
            var modelState = viewResult.ViewData.ModelState;
            var errorsCount = viewResult.ViewData.ModelState.ErrorCount;
            
            Assert.Equal(viewModel, model);
            // Expected to have an error on required field
            Assert.False(modelState.IsValid);
            Assert.Equal(1, errorsCount);
        }
        [Fact]  
        public void ConfirmMailForAuthorization_withMail_test_returns_error()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel
            {
                MailAddress = "test"
            };

            // Act
            var result = controller.ConfirmMailForAuthorization(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<ConfirmMailViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;
            Assert.Equal(viewModel, model);
            Assert.False(modelState.IsValid);
        }
        [Fact]  
        public void ConfirmMailForAuthorization_withWhiteSpaceMail_returns_error()
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
            var modelState = viewResult.ViewData.ModelState;
            Assert.Equal(viewModel, model);
            Assert.False(modelState.IsValid);
            //Assert.Equal("Неверный формат почты", modelState.Values.First().Errors.First().ErrorMessage);
        }
        [Fact]
        public void ConfirmMailForAuthorization_withRightMailFormat_returns_errorNonExistentUser()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel
            {
                MailAddress = "test@mail.ru"
            };

            mockService.Setup(s => s.IsUserExistAsync(viewModel.MailAddress)).ReturnsAsync(()=>null);

            // Act
            var result = controller.ConfirmMailForAuthorization(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<ConfirmMailViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;
            Assert.Equal(viewModel, model);
            Assert.False(modelState.IsValid);
            Assert.Equal("Неверный адрес",modelState.Values.First().Errors.First().ErrorMessage);
        }
        [Fact]
        public void ConfirmMailForAuthorization_with_rightMail_returns_ViewResult_for_passwordConfirm()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel
            {
                MailAddress = "test@mail.ru"
            };
            mockService.Setup(s => s.IsUserExistAsync(viewModel.MailAddress)).ReturnsAsync(() => new());

            // Act
            var result = controller.ConfirmMailForAuthorization(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<AuthorizationViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;
            Assert.IsType<AuthorizationViewModel>(model);
            Assert.True(modelState.IsValid);
            Assert.Equal("EnterPassword",viewResult.ViewName);
        }
        #endregion
        
        #region Enter password for authorization

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
            var redirect = Assert.IsType<RedirectToActionResult>(result.Result);
            
            Assert.Equal(nameof(controller.ConfirmMailForAuthorization),redirect.ActionName);
        }
        [Fact]
        public void EnterPassword_with_wrongPassword_returns_errorWithPassword()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new AuthorizationViewModel
            {
                MailAddress = "test@mail.ru",
                Password = "1234"
            };
            mockService.Setup(s => s.ConfirmUserAsync(viewModel.MailAddress, viewModel.Password))
                .ReturnsAsync(() => null);

            // Act
            var result = controller.EnterPassword(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<AuthorizationViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;

            Assert.IsType<AuthorizationViewModel>(viewResult.Model);
            Assert.Equal("Неверный пароль", modelState.Values.First().Errors.First().ErrorMessage);
        }
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
        [Fact]  
        public void ConfirmMailForRegistration_withMail_test_returns_error()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel
            {
                MailAddress = "test"
            };

            // Act
            var result = controller.ConfirmMailForRegistration(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<ConfirmMailViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;
            Assert.Equal(viewModel, model);
            Assert.False(modelState.IsValid);
        }
        [Fact]  
        public void ConfirmMailForRegistrationn_withWhiteSpaceMail_returns_error()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel();

            // Act
            var result = controller.ConfirmMailForRegistration(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<ConfirmMailViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;
            Assert.Equal(viewModel, model);
            Assert.False(modelState.IsValid);
            //TODO: FIX IT
            //Assert.Equal("Неверный формат почты", modelState.Values.First().Errors.First().ErrorMessage);
        }
        [Fact]
        public void ConfirmMailForRegistration_withRightMailFormat_returns_enterUserInfo()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel
            {
                MailAddress = "test@mail.ru"
            };

            mockService.Setup(s => s.IsUserExistAsync(viewModel.MailAddress)).ReturnsAsync(()=>null);

            // Act
            var result = controller.ConfirmMailForRegistration(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<RegistrationViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;
            Assert.Equal(viewModel, model);
            Assert.False(modelState.IsValid);
            Assert.Equal("EnterUserInfo", viewResult.ViewName);
        }
        [Fact]
        public void ConfirmMailForRegistration_with_rightMail_returns_errorExistedUser()
        {
            // Arrange
            var mockService = new Mock<IAuthenticateService>();
            var controller = new AuthenticateController(mockService.Object);
            var viewModel = new ConfirmMailViewModel
            {
                MailAddress = "test@mail.ru"
            };
            mockService.Setup(s => s.IsUserExistAsync(viewModel.MailAddress)).ReturnsAsync(() => new());

            // Act
            var result = controller.ConfirmMailForRegistration(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<ConfirmMailViewModel>(viewResult.ViewData.Model);
            var modelState = viewResult.ViewData.ModelState;
            Assert.IsType<ConfirmMailViewModel>(model);
            Assert.False(modelState.IsValid);
            Assert.Equal("Адрес занят", modelState.Values.First().Errors.First().ErrorMessage);
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
            var redirect = Assert.IsType<RedirectToActionResult>(result.Result);
            
            Assert.Equal(nameof(controller.ConfirmMailForRegistration),redirect.ActionName);}

        #endregion
    }
}
