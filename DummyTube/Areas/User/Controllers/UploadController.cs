using DummyTube.Models.BindingModels.Video;
using System.Web.Mvc;
using DummyTube.Services.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace DummyTube.Controllers
{
    [Authorize]
    [RoutePrefix("upload")]
    public class UploadController : Controller
    {
        private IUploadService service;

        public UploadController(IUploadService service) //:base(data)
        {
            this.service = service;
        }

        [Route("addvideo")]
        [HttpGet]
        public ActionResult AddVideo()
        {
            return View("~/Areas/User/Views/Upload/AddVideo.cshtml");
        }

        [HttpPost]
        [Route("addvideo")]
        [ValidateAntiForgeryToken]
        public ActionResult AddVideo(VideoBindingModel vbm)
        {
            if (ModelState.IsValid)
            {
                this.service.UploadVideo(vbm, User.Identity.Name);
                return Redirect($"/user/{User.Identity.Name}");
            }

            var videoVm = this.service.ConvertToViewModel(vbm);
            return this.View("~/Areas/User/Views/Upload/AddVideo.cshtml", videoVm);
        }
    }
}