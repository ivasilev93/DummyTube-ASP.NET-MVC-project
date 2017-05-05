using System;
using System.Web.Mvc;
using DummyTube.Models.BindingModels.Comment;
using DummyTube.Services.Contracts;
using DummyTube.Hubs;

namespace DummyTube.Controllers
{
    public class HomeController : Controller
    {
        private IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        [Route]
        public ActionResult Index()
        {
            var feedVm = this.service.GetVideoFeed();
            
            return View(feedVm);
        }

        [HttpGet]
        [Route("{videoId}")]
        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "~/Views/Shared/Errors/InvalidVideoError.cshtml")]
        public ActionResult Watch(string videoId)
        {
           var videoDetailsVm = this.service
                .VideoDetailsVm(videoId);

            if (User.Identity.IsAuthenticated)
            {
                this.service.AddToUserHistory(videoId, User.Identity.Name);
                //var hub = new ActivityHub();
                //hub.WatchedVideo(videoDetailsVm.Title, User.Identity.Name);
            }

            return View(videoDetailsVm);
        }

        [HttpGet]
        [Route("search")]
        public ActionResult Search(string item)
        {
            var feedVm = this.service.SearchForItem(item);
            return View(feedVm);
        }
    }
}