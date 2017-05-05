using AutoMapper;
using DummyTube.Data.UnitOfWork;
using DummyTube.Models.BindingModels.Video;
using DummyTube.Models.EntityModels;
using DummyTube.Models.ViewModels.Video;
using DummyTube.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Services
{
    public class UploadService : IUploadService
    {
        private IDummyTubeData data;

        public UploadService(IDummyTubeData data)
        {
            this.data = data;
        }

        public void UploadVideo(VideoBindingModel vbm, string activeUser)
        {
            var video = Mapper.Map<VideoBindingModel, Video>(vbm);
            video.Owner = this.data.Users.All().First(x => x.UserName == activeUser);

            this.data.Videos.Add(video);
            this.data.SaveChanges();
        }

        public VideoViewModel ConvertToViewModel(VideoBindingModel videoBindingModel)
        {
            return Mapper.Map<VideoBindingModel, VideoViewModel>(videoBindingModel);
        }
    }
}
