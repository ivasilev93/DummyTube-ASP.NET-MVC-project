using DummyTube.Models.BindingModels.Comment;
using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Services.Contracts
{
    public interface IUserService
    {
        ShowProfileViewModel UserProfileByUsername(string username);

        bool IsSubscribedToUser(string user, string activeUser);

        void PostComment(CommentBindingModel cbm, string activeUser);

        IEnumerable<CommentViewModel> GetCommentsVms(string videoId);

        void SubscribeToUser(string userId, string activeUser);

        void UnsubscribeFromUser(string userId, string activeUser);

        ShowProfileViewModel UserProfileById(string userId);
    }
}
