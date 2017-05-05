using DummyTube.Models.BindingModels.Comment;
using DummyTube.Models.BindingModels.Video;
using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Services.Contracts
{
    public interface IAdminService
    {
        IEnumerable<UserInfoViewModel> GetUsers(string activeUser);

        void DeleteVideo(DeleteVideoBindingModel dvbm);

        void DeleteComment(DeleteCommentBindingModel dcbm);

        IEnumerable<CommentViewModel> GetCommentsVms(string videoId);

        void DeleteUser(string id);
    }
}
