using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DummyTube.Services.Contracts;
using DummyTube.Models.ViewModels.Video;
using DummyTube.Controllers;
using TestStack.FluentMVCTesting;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;

namespace DummyTube.Tests.Controllers
{
    [TestClass]
    public class HomeController_Tests
    {
        private Mock<IHomeService> homeServiceMock;
        private HomeController homeController;

        [TestInitialize]
        public void Initialize()
        {
            this.homeServiceMock = new Mock<IHomeService>();

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("user");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            this.homeController = new HomeController(this.homeServiceMock.Object);
            this.homeController.ControllerContext = mock.Object;
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.homeServiceMock = null;
        }

        [TestMethod]
        public void ReturnsCorrectVideoDetailsViewModel_WhenGivenValidYoutubeId()
        {
            //setup video view model
            var videoVm = new VideoDetailsViewModel()
            {
                Image = "https://www.someimageurl.com",
                Title = "MJ's NEW video",
                YoutubeId = "xxx111xxx"
            };

            //setup controller
            this.homeServiceMock.Setup(x => x.VideoDetailsVm("xxx111xxx")).Returns(videoVm);
           

            //assert and act
            this.homeController
                .WithCallTo(h => h.Watch(videoVm.YoutubeId))
                .ShouldRenderDefaultView()
                .WithModel<VideoDetailsViewModel>(vm =>
                {
                    Assert.AreEqual(videoVm.Image, vm.Image);
                    Assert.AreEqual(videoVm.Title, vm.Title);
                    Assert.AreEqual(videoVm.YoutubeId, vm.YoutubeId);
                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullException_WhenPassedInvalidYoutubeId()
        {
            this.homeServiceMock
                .Setup(x => x.VideoDetailsVm(It.IsAny<string>())).Throws(new ArgumentNullException());

            //assert, act
            var result = this.homeController.Watch("123");
        }

        [TestMethod]
        public void IndexMethod_ShouldReturnVideoViewModelEnumeration()
        {
            IEnumerable<VideoViewModel> feed = new List<VideoViewModel>();

            this.homeServiceMock.Setup(x => x.GetVideoFeed()).Returns(feed);

            //assert, act
            this.homeController
               .WithCallTo(h => h.Index())
               .ShouldRenderDefaultView()
               .WithModel<IEnumerable<VideoViewModel>>(vm =>
               {
                   Assert.AreSame(feed, vm);
               });
        }

        [TestMethod]
        public void SearchMethod_ReturnsSearchedItems()
        {
            IEnumerable<VideoViewModel> searchedItems = new List<VideoViewModel>();

            this.homeServiceMock.Setup(x => x.SearchForItem(It.IsAny<string>())).Returns(searchedItems);
            
            //assert, act
            this.homeController
               .WithCallTo(h => h.Search("123"))
               .ShouldRenderDefaultView()
               .WithModel<IEnumerable<VideoViewModel>>(vm =>
               {
                   Assert.AreSame(searchedItems, vm);
               });
        }
    }
}
