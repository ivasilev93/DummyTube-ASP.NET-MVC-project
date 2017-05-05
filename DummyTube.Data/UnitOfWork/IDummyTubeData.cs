using DummyTube.Models.EntityModels;
using DummyTube.Data.Repository;

namespace DummyTube.Data.UnitOfWork
{
    public interface IDummyTubeData
    {
        IRepository<User> Users { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Video> Videos { get; }

        IRepository<Category> Categories { get; }

        void SaveChanges();
    }
}
