using Moq;
using Services.Interfaces;
using Xunit;

namespace Services.Tests
{
    public class IAuthenticateServiceTests
    {
        #region ConfirmUserAsync
        [Fact]
        public void ConfirmUserAsync_with_emptyMail_returns_Null()
        {
            // Arrange
            var mock = new Mock<IAuthenticateService>();
            var mockService = mock.Object;
           
            // Act
            var result = mockService.ConfirmUserAsync("", "1234").Result;

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void ConfirmUserAsync_with_emptyPassword_returns_Null()
        {
            // Arrange
            var mock = new Mock<IAuthenticateService>();
            var mockService = mock.Object;
           
            // Act
            var result = mockService.ConfirmUserAsync("test@mail.ru", "").Result;

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void ConfirmUserAsync_with_wrongMail_returns_Null()
        {
            // Arrange
            var mock = new Mock<IAuthenticateService>();
            var mockService = mock.Object;
           
            // Act
            var result = mockService.ConfirmUserAsync("test", "12345").Result;

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void ConfirmUserAsync_with_wrongPassword_returns_Null()
        {
            // Arrange
            var mock = new Mock<IAuthenticateService>();

            mock.Setup(s => s.ConfirmUserAsync("test@mail.ru", "123456")).ReturnsAsync(() => null);

            var mockService = mock.Object;
            
           
            // Act
            var result = mockService.ConfirmUserAsync("test@mail.ru", "123456").Result;

            // Assert
            Assert.Null(result);
        }
        #endregion
    }
}
