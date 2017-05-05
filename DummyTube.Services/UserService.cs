using AutoMapper;
using DummyTube.Data.UnitOfWork;
using DummyTube.Models.BindingModels.Comment;
using DummyTube.Models.EntityModels;
using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Comment;
using DummyTube.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DummyTube.Services
{
    public class UserService : IUserService
    {
        private IDummyTubeData data;

        public UserService(IDummyTubeData data)
        {
            this.data = data;
        }

        public ShowProfileViewModel UserProfileByUsername(string username)
        {
            var user = this.data.Users.All().FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return Mapper.Map<User, ShowProfileViewModel>(user);
        }

        public bool IsSubscribedToUser(string user, string activeUser)
        {
            return this.data.Users.All().First(x => x.UserName == activeUser).Subscriptions.Any(u => u.UserName == user);
        }

        public void PostComment(CommentBindingModel cbm, string activeUser)
        {
            var video = this.data.Videos.All().FirstOrDefault(x => x.YoutubeId == cbm.YoutubeId);

            var comment = new Comment()
            {
                Author = this.data.Users.All().First(x => x.UserName == activeUser),
                Content = cbm.Content,
                Video = video,
                PostedOn = DateTime.Now
            };

            this.data.Comments.Add(comment);
            this.data.SaveChanges();
        }

        public IEnumerable<CommentViewModel> GetCommentsVms(string videoId)
        {
            var comments = this.data.Comments.All().Where(x => x.Video.YoutubeId == videoId);
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
        }

        public void SubscribeToUser(string userId, string activeUser)
        {
            var profile = this.data.Users.All().FirstOrDefault(x => x.Id == userId);
            this.data.Users.All()
                .First(x => x.UserName == activeUser)
                .Subscriptions.Add(profile);

            this.data.SaveChanges();
        }

        public ShowProfileViewModel UserProfileById(string userId)
        {
            var userinfoVM = Mapper.Map<User, ShowProfileViewModel>(this.data.Users.All().FirstOrDefault(x => x.Id == userId));
            userinfoVM.Subscribed = true;

            return userinfoVM;
        }

        public void UnsubscribeFromUser(string userId, string activeUser)
        {
            var profile = this.data.Users.All().FirstOrDefault(x => x.Id == userId);
            this.data.Users.All()
                .First(x => x.UserName == activeUser)
                .Subscriptions.Remove(profile);

            this.data.SaveChanges();
        }
    }
}
