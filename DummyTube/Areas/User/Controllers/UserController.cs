using DummyTube.Models.BindingModels.Comment;
using DummyTube.Services.Contracts;
using System;
using System.Web.Mvc;

namespace DummyTube.Controllers
{
    [RoutePrefix("user")]
    public class UserController : Controller
    {
        private IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{username}")]
        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "~/Views/Shared/Errors/InvalidUsernameError.cshtml")]
        public ActionResult ShowProfile(string username)
        {
            var userinfoVM = this.service.UserProfileByUsername(username);
            if (User.Identity.IsAuthenticated)
            {
                userinfoVM.Subscribed = this.service.IsSubscribedToUser(username, User.Identity.Name);
            }

            return View("~/Areas/User/Views/User/ShowProfile.cshtml", userinfoVM);
        }

        [HttpPost]
        [Authorize]
        [Route("PostComment")]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(CommentBindingModel cbm)
        {
            this.service.PostComment(cbm, User.Identity.Name);
            var commentsVM = this.service.GetCommentsVms(cbm.YoutubeId);

            return this.PartialView("~/Views/Shared/Partials/CommentsPartial.cshtml", commentsVM);
        }

        [HttpPost]
        [Route("subscribe")]
        [Authorize]
        public ActionResult Subscribe(string id)
        {
            this.service.SubscribeToUser(id, User.Identity.Name);
            var userinfoVM = this.service.UserProfileById(id);

            return this.PartialView("~/Views/Shared/Partials/SubscribeButtonPartial.cshtml", userinfoVM);
        }

        [HttpPost]
        [Route("unsubscribe")]
        [Authorize]
        public ActionResult Unsubscribe(string id)
        {
            this.service.UnsubscribeFromUser(id, User.Identity.Name);

            var userinfoVM = this.service.UserProfileById(id);
            userinfoVM.Subscribed = false;

            return this.PartialView("~/Views/Shared/Partials/SubscribeButtonPartial.cshtml", userinfoVM);
        }
    }
}