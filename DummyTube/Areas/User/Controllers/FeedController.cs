using System.Web.Mvc;
using DummyTube.Services.Contracts;

namespace DummyTube.Controllers
{
    [RoutePrefix("feed"), Authorize]
    public class FeedController : Controller
    {
        private IFeedService service;

        public FeedController(IFeedService service) //: base(data)
        {
            this.service = service;
        }

        [HttpGet, Route("history")]
        public ActionResult History()
        {
            var feedVm = this.service.GetHistory(User.Identity.Name);

            return View("~/Areas/User/Views/Feed/History.cshtml", feedVm);
        }

        [HttpGet, Route("subscriptions")]
        public ActionResult Subscriptions()
        {
            var feedVm =this.service.GetSubscribtions(User.Identity.Name);

            return View("~/Areas/User/Views/Feed/Subscriptions.cshtml", feedVm);
        }
    }
}