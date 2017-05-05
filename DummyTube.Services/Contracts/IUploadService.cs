using DummyTube.Models.BindingModels.Video;
using DummyTube.Models.ViewModels.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Services.Contracts
{
    public interface IUploadService
    {
        void UploadVideo(VideoBindingModel vbm, string activeUser);

        VideoViewModel ConvertToViewModel(VideoBindingModel videoBindingModel);
    }
}
