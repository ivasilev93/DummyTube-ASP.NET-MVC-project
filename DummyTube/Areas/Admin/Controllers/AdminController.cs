namespace DummyTube.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using DummyTube.Models.BindingModels.Video;
    using DummyTube.Models.BindingModels.Comment;
    using DummyTube.Services.Contracts;


    [RoutePrefix("admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [Route("dashboard")]
        public ActionResult Dashboard()
        {
            var usersVm = this.service.GetUsers(User.Identity.Name);
            return View("~/Areas/Admin/Views/Admin/Dashboard.cshtml", usersVm);
        }

        [HttpPost]
        [Route("DeleteVideo")]
        public ActionResult DeleteVideo(DeleteVideoBindingModel dvbm)
        {
            this.service.DeleteVideo(dvbm);
            return Redirect($"/user/{dvbm.Owner}");
        }

        [HttpPost]
        [Route("DeleteComment")]
        public ActionResult DeleteComment(DeleteCommentBindingModel dcbm)
        {
            this.service.DeleteComment(dcbm);
            var commentsVM = this.service.GetCommentsVms(dcbm.VideoYoutubeId);

            return this.PartialView("~/Views/Shared/Partials/CommentsPartial.cshtml", commentsVM);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(string id)
        {
            this.service.DeleteUser(id);
            return Redirect("/admin/dashboard");
        }
    }
}