namespace DummyTube.Data
{
    using Models.EntityModels;
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;

    public class DummyTubeContext : IdentityDbContext<User>
    {
        public DummyTubeContext()
            : base("DummyTubeContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DummyTubeContext, Configuration>());
        }

        public virtual IDbSet<Video> Videos { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }

        public static DummyTubeContext Create()
        {
            return new DummyTubeContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithRequired(x => x.Author)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Video>()
                .HasRequired(v => v.Owner)
                .WithMany(o => o.Uploads)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Video>()
                .HasMany(v => v.Comments)
                .WithRequired(c => c.Video)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}