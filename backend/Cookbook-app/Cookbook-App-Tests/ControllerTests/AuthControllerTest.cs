 using NUnit.Framework;
 using Moq;
 using Cookbook_app.Models;
 using Cookbook_app.Controllers;
 using Cookbook_app.Services;
 using Cookbook_app.Models.Auth;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.AspNetCore.Mvc;


 namespace Cookbook_App_Tests;

 public class AuthControllerTests
 {

     private Mock<IJwtService> _jwtService;
     private Mock<UserManager<IdentityUser>> _userManager;
     private AuthController _controller;

     [SetUp]
     public void Setup()
     {
         _userManager = new Mock<UserManager<IdentityUser>>(
             Mock.Of<IUserStore<IdentityUser>>(),
             null,
             null,
             null,
             null,
             null,
             null,
             null,
             null
         );
         _jwtService = new Mock<IJwtService>();

         _controller = new AuthController(
             _userManager.Object,
             _jwtService.Object
         );
     }






     [Test]
          public async Task Register_WhenUserIsCreated_ReturnsOk()
         {
             var request = new RegisterUserRequest(
                 "testUser",
                 "test@gmail.com",
                 "Password123"
             );
             _userManager.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), request.Password)).ReturnsAsync(IdentityResult.Success);
                 
             var result = await _controller.Register(request);
             Assert.IsInstanceOf<OkResult>(result);
         }

     [Test]

     public async Task Login_WhenUserIsCreated_ReturnsOk()
     {     var user = new IdentityUser
         {
             UserName = "testUser",
             Email = "test@gmail.com"
         };
         
         
         _userManager
             .Setup(um => um.FindByNameAsync("testUser"))
             .ReturnsAsync(user);

         _userManager
             .Setup(um => um.CheckPasswordAsync(user, "testPassword123"))
             .ReturnsAsync(true);

         _jwtService
             .Setup(jwt => jwt.CreateToken(user))
             .Returns("fake-jwt-token");
         
         
         var result = await _controller.Login(
             new LoginRequest("testUser", "testPassword123")
         );

         Assert.IsInstanceOf<OkObjectResult>(result);
        
         
     }

     [Test]
     public async Task Register_WhenUserIsNotCreated_ReturnsBadRequest()
     {
         var request = new RegisterUserRequest(
             null, null, null);
         _userManager.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), request.Password)).ReturnsAsync(IdentityResult.Failed());
         var result = await _controller.Register(request);
         Assert.IsInstanceOf<BadRequestObjectResult>(result);
     }

     [Test]
     public async Task Login_whenUserIsValidAndPasswordIsNotCorrect_ReturnsBadRequest()
     {
         var user = new IdentityUser
         {
             UserName = "testUser",
             Email = "test@gmail.com"
         };

         _userManager
             .Setup(um => um.FindByNameAsync("testUser"))
             .ReturnsAsync(user);

         _userManager
             .Setup(um => um.CheckPasswordAsync(user, "wrongPassword"))
             .ReturnsAsync(false);

         var result = await _controller.Login(
             new LoginRequest("testUser", "wrongPassword")
         );

         Assert.IsInstanceOf<UnauthorizedResult>(result);
     }

     [Test]
     public async Task Login_whenUserIsNOtValidAndPasswordIsCorrect_ReturnsOk()
     {
         _userManager
             .Setup(um => um.FindByNameAsync("wrongUser"))
             .ReturnsAsync((IdentityUser?)null);

         var result = await _controller.Login(
             new LoginRequest("wrongUser", "testPassword123")
         );

         Assert.IsInstanceOf<UnauthorizedResult>(result);
     }
     
     
     }

 
 
 