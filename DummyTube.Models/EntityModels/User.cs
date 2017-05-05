using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections;

namespace DummyTube.Models.EntityModels
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Uploads = new HashSet<Video>();
            this.History = new HashSet<Video>();
            this.Subscriptions = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
        }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> Subscriptions { get; set; }

        public virtual ICollection<Video> History { get; set; }

        public virtual ICollection<Video> Uploads { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
