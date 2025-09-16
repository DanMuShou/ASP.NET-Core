using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Resources;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR.Commands.User;
using DanMu.Pan.MediatR.Queries.User;
using DanMu.Pan.Repository.User;
using DanMuPan.API.Controllers.User;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DanMuPan.API.Controllers.User.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly Mock<PathHelper> _mockPathHelper;
        private readonly Mock<UserInfoToken> _mockUserInfoToken;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockPathHelper = new Mock<PathHelper>();
            _mockUserInfoToken = new Mock<UserInfoToken>();

            _userController = new UserController(
                _mockMediator.Object,
                _mockWebHostEnvironment.Object,
                _mockPathHelper.Object,
                _mockUserInfoToken.Object
            );
        }

        // ... existing code ...
        [TestMethod]
        public async Task AddUser_WithValidCommand_ReturnsCreatedResult()
        {
            // Arrange
            var addUserCommand = new AddUserCommand
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "password123",
                PhoneNumber = "1234567890",
                Address = "123 Test St",
                IsActive = true,
                IsAdmin = false,
            };

            var userDto = new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "johndoe",
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Address = "123 Test St",
                IsActive = true,
                IsAdmin = false,
            };

            var serviceResponse = ServiceResponse<UserDto>.ReturnResultWith201(userDto);
            _mockMediator
                .Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);

            // Act
            var result = await _userController.AddUser(addUserCommand);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = (CreatedAtActionResult)result;
            Assert.AreEqual("GetUser", createdResult.ActionName);
            Assert.AreEqual(userDto.Id, ((UserDto)createdResult.Value).Id);
        }

        [TestMethod]
        public async Task AddUser_WithInvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var addUserCommand = new AddUserCommand();

            var serviceResponse = ServiceResponse<UserDto>.ReturnFailed(400, "Invalid user data");
            _mockMediator
                .Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);

            // Act
            var result = await _userController.AddUser(addUserCommand);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var badRequestResult = (ObjectResult)result;
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAllUsers_ReturnsOkResult()
        {
            // Arrange
            var userList = new List<UserDto>
            {
                new UserDto { Id = Guid.NewGuid(), UserName = "user1" },
                new UserDto { Id = Guid.NewGuid(), UserName = "user2" },
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userList);

            // Act
            var result = await _userController.GetAllUsers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userList, okResult.Value);
        }

        [TestMethod]
        public async Task GetUser_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDto = new UserDto { Id = userId, UserName = "testuser" };

            var serviceResponse = ServiceResponse<UserDto>.ReturnResultWith200(userDto);
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);

            // Act
            var result = await _userController.GetUser(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userDto, okResult.Value);
        }

        [TestMethod]
        public async Task GetUser_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var serviceResponse = ServiceResponse<UserDto>.Return404();
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);

            // Act
            var result = await _userController.GetUser(userId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task GetUsers_WithValidResource_ReturnsOkResult()
        {
            // Arrange
            var userResource = new UserResource();
            var userList = new List<UserDto>
            {
                new UserDto { Id = Guid.NewGuid(), UserName = "user1" },
                new UserDto { Id = Guid.NewGuid(), UserName = "user2" },
            };

            var expectedUserList = new UserList(userList, 10, 0, 5);

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUserList);

            // Need to mock HttpContext to test header addition
            var httpContext = new DefaultHttpContext();
            _userController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            // Act
            var result = await _userController.GetUsers(userResource);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedUserList, okResult.Value);
            Assert.IsTrue(httpContext.Response.Headers.ContainsKey("X-Pagination"));
        }

        [TestMethod]
        public async Task GetRecentlyRegisteredUsers_ReturnsOkResult()
        {
            // Arrange
            var userList = new List<UserDto>
            {
                new UserDto { Id = Guid.NewGuid(), UserName = "recentuser1" },
                new UserDto { Id = Guid.NewGuid(), UserName = "recentuser2" },
            };

            _mockMediator
                .Setup(m =>
                    m.Send(
                        It.IsAny<GetRecentlyRegisteredUserQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(userList);

            // Act
            var result = await _userController.GetRecentlyRegisteredUsers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(userList, okResult.Value);
        }

        [TestMethod]
        public async Task UserLogin_WithValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var userLoginCommand = new UserLoginCommand
            {
                UserName = "testuser",
                Password = "password123",
            };

            var userAuthDto = new UserAuthDto { UserName = "testuser", ProfilePhoto = "photo.jpg" };

            var serviceResponse = ServiceResponse<UserAuthDto>.ReturnResultWith200(userAuthDto);
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UserLoginCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);

            var httpContext = new DefaultHttpContext
            {
                Connection = { RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1") },
            };

            _userController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            _mockPathHelper.Setup(p => p.UserProfilePath).Returns("/uploads/profiles/");

            // Act
            var result = await _userController.UserLogin(userLoginCommand);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(okResult.Value, typeof(UserAuthDto));
            var returnedUser = (UserAuthDto)okResult.Value;
            Assert.AreEqual("/uploads/profiles/photo.jpg", returnedUser.ProfilePhoto);
        }

        [TestMethod]
        public async Task UserLogin_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var userLoginCommand = new UserLoginCommand
            {
                UserName = "testuser",
                Password = "wrongpassword",
            };

            var serviceResponse = ServiceResponse<UserAuthDto>.ReturnFailed(
                401,
                "Invalid credentials"
            );
            _mockMediator
                .Setup(m => m.Send(It.IsAny<UserLoginCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);

            var httpContext = new DefaultHttpContext();
            _userController.ControllerContext = new ControllerContext { HttpContext = httpContext };

            // Act
            var result = await _userController.UserLogin(userLoginCommand);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var unauthorizedResult = (ObjectResult)result;
            Assert.AreEqual(401, unauthorizedResult.StatusCode);
        }
    }
}
