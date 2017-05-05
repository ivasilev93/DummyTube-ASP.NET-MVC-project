using DummyTube.Models.ViewModels.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.ViewModels.Account
{
    public class ShowProfileViewModel
    {
        public string Id { get; set; }

        public string User { get; set; }

        public IEnumerable<VideoViewModel> Videos { get; set; }

        public bool Subscribed { get; set; }
    }
}
