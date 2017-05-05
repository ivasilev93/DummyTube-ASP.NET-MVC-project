using AutoMapper;
using DummyTube.Data.UnitOfWork;
using DummyTube.Models.EntityModels;
using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Video;
using DummyTube.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Services
{
    public class FeedService : IFeedService
    {
        private IDummyTubeData data;

        public FeedService(IDummyTubeData data)
        {
            this.data = data;
        }

        public IEnumerable<VideoViewModel> GetHistory(string activeUser)
        {
            IEnumerable<Video> videoFeed = this.data.Users.All()
                .First(x => x.UserName == activeUser).History.Take(15);

            return Mapper
                .Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(videoFeed);
        }

        public IEnumerable<UserInfoViewModel> GetSubscribtions(string activeUser)
        {
            IEnumerable<User> subsFeed = this.data.Users.All()
                .First(x => x.UserName == activeUser).Subscriptions.Take(15);

            return Mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(subsFeed);
        }
    }
}
