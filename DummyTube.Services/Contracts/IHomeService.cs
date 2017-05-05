using DummyTube.Models.ViewModels.Video;
using System.Collections.Generic;

namespace DummyTube.Services.Contracts
{
    public interface IHomeService
    {
        IEnumerable<VideoViewModel> GetVideoFeed();

        VideoDetailsViewModel VideoDetailsVm(string videoId);

        IEnumerable<VideoViewModel> SearchForItem(string item);

        void AddToUserHistory(string videoId, string activeUser);
    }
}
