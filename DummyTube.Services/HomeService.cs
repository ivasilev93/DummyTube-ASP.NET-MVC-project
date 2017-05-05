using AutoMapper;
using DummyTube.Data.UnitOfWork;
using DummyTube.Models.BindingModels.Comment;
using DummyTube.Models.EntityModels;
using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Comment;
using DummyTube.Models.ViewModels.Video;
using DummyTube.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DummyTube.Services
{
    public class HomeService : IHomeService
    {
        private IDummyTubeData data;

        public HomeService(IDummyTubeData data)
        {
            this.data = data;
        }

        public IEnumerable<VideoViewModel> GetVideoFeed()
        {
           IEnumerable<Video> videoFeed = this.data.Videos.All().Take(16);
           return Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(videoFeed);
        }

        public VideoDetailsViewModel VideoDetailsVm(string videoId)
        {
            var video = this.data.Videos.All().FirstOrDefault(x => x.YoutubeId == videoId);

            if (video == null)
            {
                throw new ArgumentNullException();
            }

            return Mapper.Map<Video, VideoDetailsViewModel>(video);
        }

        public IEnumerable<VideoViewModel> SearchForItem(string item)
        {
            IEnumerable<Video> videoFeed = this.data.Videos.All().Where(x => x.Title.Contains(item)).Take(15);
            return Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(videoFeed);
        }

        public void AddToUserHistory(string videoId, string activeUser)
        {
            var video = this.data.Videos.All().FirstOrDefault(x => x.YoutubeId == videoId);

            if (video == null)
            {
                throw new ArgumentNullException();
            }

            this.data.Users.All().First(x => x.UserName == activeUser).History.Add(video);
            this.data.SaveChanges();
        }
       
    }
}
