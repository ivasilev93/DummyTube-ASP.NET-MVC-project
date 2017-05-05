using System.Web.Mvc;

namespace DummyTube.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();

         //   context.MapRoute(
         //       "Admin_default",
         //       "Admin/{controller}/{action}",
         //       new {controller = "Admin", action = "Dashboard" }
         //   );

         //   context.MapRoute(
         //       "Admin_delete_video",
         //       "Admin/{controller}/{action}",
         //       new { controller = "Admin", action = "DeleteVideo"}
         //   );

         //   context.MapRoute(
         //    "Admin_delete_comment",
         //    "Admin/{controller}/{action}",
         //    new { controller = "Admin", action = "DeleteComment" }
         //);
        }
    }
}