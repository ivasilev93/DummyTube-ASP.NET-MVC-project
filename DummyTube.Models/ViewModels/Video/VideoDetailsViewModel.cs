using DummyTube.Models.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.ViewModels.Video
{
    public class VideoDetailsViewModel : VideoViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
