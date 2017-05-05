using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DummyTube.Services.Contracts;
using System.Web.Mvc;
using DummyTube.Controllers;
using TestStack.FluentMVCTesting;
using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Comment;
using System.Collections.Generic;
using DummyTube.Models.BindingModels.Comment;

namespace DummyTube.Tests.Controllers
{
    [TestClass]
    public class UserController_Tests
    {
        private Mock<IUserService> homeServiceMock;
        private UserController userController;

        [TestInitialize]
        public void Initialize()
        {
            this.homeServiceMock = new Mock<IUserService>();

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("user");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            userController = new UserController(this.homeServiceMock.Object);
            userController.ControllerContext = mock.Object;
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.homeServiceMock = null;
        }

        [TestMethod]
        public void ShowProfile_ReturnsCorrectViewModelGivenValidUsername()
        {
            //setup
            var model = new ShowProfileViewModel()
            {
                 User = "user123",
                 Subscribed = true
            };
            
            this.homeServiceMock.Setup(x => x.UserProfileByUsername("user123"))
                .Returns(model);

            //act and assert
            this.userController
               .WithCallTo(x => x.ShowProfile("user123"))
               .ShouldRenderView("~/Areas/User/Views/User/ShowProfile.cshtml")
               .WithModel<ShowProfileViewModel>(m =>
               {
                   Assert.AreEqual(m.User, model.User);
                   Assert.AreEqual(m.Subscribed, model.Subscribed);
               });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShowProfile_ShouldThrowExceptionWhenGivenInvalidUsername()
        {
            //setup
            this.homeServiceMock.Setup(x => x.UserProfileByUsername(It.IsAny<string>()))
              .Throws(new ArgumentNullException());

            //assert and act
            this.userController.ShowProfile("any");
        }

        [TestMethod]
        public void PostComment_ShouldReturnPartialWithValidCommentViewModels()
        {
            //setup
            var bindingModel = new CommentBindingModel()
            {
                Content = "something said"
            };

            var commentsEnumeration = new List<CommentViewModel>()
            {
                new CommentViewModel()
                {
                    Author = "user",
                    Content = "something said"
                },
                new CommentViewModel()
                {
                    Author = "secondUser",
                    Content = "something else"
                }
            };

            this.homeServiceMock.Setup(x => x.GetCommentsVms(It.IsAny<string>())).Returns(commentsEnumeration);

            //act and assert
            this.userController
                .WithCallTo(x => x.PostComment(bindingModel))
                .ShouldRenderPartialView("~/Views/Shared/Partials/CommentsPartial.cshtml")
                .WithModel<IEnumerable<CommentViewModel>>(comments =>
                {
                    Assert.IsNotNull(comments);
                });
        }

        [TestMethod]
        public void SubscribeMethod_ShouldReturnPartialWithValidViewModel()
        {
            //setup
            var model = new ShowProfileViewModel()
            {
                User = "user123",
                Subscribed = true
            };

            this.homeServiceMock.Setup(x => x.UserProfileById(It.IsAny<string>()))
                .Returns(model);

            //assert and act
            this.userController
                .WithCallTo(x => x.Subscribe("anyId"))
                .ShouldRenderPartialView("~/Views/Shared/Partials/SubscribeButtonPartial.cshtml")
                .WithModel<ShowProfileViewModel>(m =>
                {
                    Assert.IsNotNull(m);
                });
        }
    }
}
