using FluentAssertions;
using HealthcareApp.Application.Abstractions;
using HealthcareApp.Application.Abstractions.Decorators;
using HealthcareApp.Application.DTOs.Login;
using HealthcareApp.Application.DTOs.Register;
using HealthcareApp.Application.Implementations;
using HealthcareApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using System.Collections.Generic;
using System.Data;

namespace HealthcareApp.Tests.Application.Implementations
{
    public class UserAuthenticationServiceTests
    {
        private readonly IUserManagerDecorator _userManagerDecorator;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        private readonly IUserAuthenticationService _userAuthenticationService;

        public UserAuthenticationServiceTests()
        {
            _userManagerDecorator = Substitute.For<IUserManagerDecorator>();
            _tokenGeneratorService = Substitute.For<ITokenGeneratorService>();

            _userAuthenticationService = new AuthenticationService(_userManagerDecorator, _tokenGeneratorService);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnSucceededResult_WhenUserIsCreatedSuccessfully()
        {
            //Arrange

            var registerDto = new RegisterUserDTO("Anna", "Test", "test@gmail.com", "Password123!", "Patient");

            _userManagerDecorator
                .CreateAsync(Arg.Any<ApplicationUser>(), registerDto.Password)
                .Returns(IdentityResult.Success);

            _userManagerDecorator
                .AddToRoleAsync(Arg.Any<ApplicationUser>(), registerDto.Role)
                .Returns(IdentityResult.Success);

            //Act

            var result = await _userAuthenticationService.RegisterAsync(registerDto);

            //Assert

            result.IsSuccess.Should().BeTrue();

            result.Body.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnFailedResult_WhenRegistrationFails()
        {
            //Arrange

            var registerDto = new RegisterUserDTO("Anna", "Test", "test@gmail.com", "Password123!", "Patient");

            _userManagerDecorator
                .CreateAsync(Arg.Any<ApplicationUser>(), registerDto.Password)
                .Returns(IdentityResult.Failed(new IdentityError { Description = "Registration failed"}));

            //Act

            var result = await _userAuthenticationService.RegisterAsync(registerDto);

            //Assert

            result.IsSuccess.Should().BeFalse();

            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnFailedResult_WhenNonExistentRoleIsEntered()
        {

            //Arrange

            var registerDto = new RegisterUserDTO("Anna", "Test", "test@gmail.com", "Password123!", "NonExistent");

            //Act

            var result = await _userAuthenticationService.RegisterAsync(registerDto);

            //Assert

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
         public async Task LoginAsync_ShouldReturnToken_WhenLoginIsSuccessful()
         {
            //Arrange
            var loginUserDTO = new LoginUserDTO("test@gmail.com", "Password123!");

            var user = new ApplicationUser
            {
                UserName = loginUserDTO.Email
            };

            var userRoles = new List<string> { "Patient" };

            var token = "valid-jwt-token-string";

            _userManagerDecorator.
                FindByEmailAsync(loginUserDTO.Email)
                .Returns(user);

            _userManagerDecorator
                .CheckPasswordAsync(user, loginUserDTO.Password)
                .Returns(true);

            _userManagerDecorator
                .GetRolesAsync(user)
                .Returns(userRoles);

            _tokenGeneratorService
                .GenerateJwtToken(user, Arg.Any<List<string>>())
                .Returns(token);

            //Act

            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            //Assert

            result.IsSuccess.Should().BeTrue();

            result.Error.Should().BeNullOrEmpty();

            result.Body.Should().Be(token);

         }

        [Fact]
        public async Task LoginAsync_ShouldReturnFailedResult_WhenInvalidEmailIsEntered()
        {
            //Arrange

            var loginUserDTO = new LoginUserDTO("test@gmail.com", "Password123!");
            ApplicationUser? user = null;

            _userManagerDecorator
                .FindByEmailAsync(loginUserDTO.Email)
                .Returns(user);


            //Act

            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            //Assert

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
            result.Body.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnFailedResult_WhenInvalidPasswordIsEntered()
        {

            //Arrange

            var loginUserDTO = new LoginUserDTO("test@gmail.com", "Password123!");
            var user = new ApplicationUser { Email = loginUserDTO.Email };

            _userManagerDecorator
                .FindByEmailAsync(loginUserDTO.Email)
                .Returns(user);
            _userManagerDecorator
                .CheckPasswordAsync(user, loginUserDTO.Password)
                .Returns(false);


            //Act

            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            //Assert

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
            result.Body.Should().BeNullOrEmpty();
        }
    }
}