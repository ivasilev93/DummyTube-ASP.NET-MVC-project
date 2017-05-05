using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Services.Contracts
{
    public interface IFeedService
    {
        IEnumerable<VideoViewModel> GetHistory(string activeUser);

        IEnumerable<UserInfoViewModel> GetSubscribtions(string activeUser);
    }
}
