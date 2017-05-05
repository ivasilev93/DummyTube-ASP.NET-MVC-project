using AutoMapper;
using DummyTube.Data.UnitOfWork;
using DummyTube.Models.BindingModels.Comment;
using DummyTube.Models.BindingModels.Video;
using DummyTube.Models.EntityModels;
using DummyTube.Models.ViewModels.Account;
using DummyTube.Models.ViewModels.Comment;
using DummyTube.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Services
{
    public class AdminService : IAdminService
    {
        private IDummyTubeData data;

        public AdminService(IDummyTubeData data)
        {
            this.data = data;
        }

        public IEnumerable<UserInfoViewModel> GetUsers(string activeUser)
        {
            var users = this.data.Users.All().Where(x => x.UserName != activeUser);
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserInfoViewModel>>(users);
        }

        public void DeleteVideo(DeleteVideoBindingModel dvbm)
        {
            var videoToRemove = this.data.Videos.All().FirstOrDefault(x => x.Id == dvbm.Id);
            this.data.Videos.Remove(videoToRemove);
            this.data.SaveChanges();
        }

        public void DeleteComment(DeleteCommentBindingModel dcbm)
        {
            var comment = this.data.Comments.All().FirstOrDefault(x => x.Id == dcbm.Id);
            this.data.Comments.Remove(comment);
            this.data.SaveChanges();
        }

        public IEnumerable<CommentViewModel> GetCommentsVms(string videoId)
        {
            var comments = this.data.Comments.All().Where(x => x.Video.YoutubeId == videoId);
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
        }

        public void DeleteUser(string id)
        {
            try
            {
                var user = this.data.Users.All().FirstOrDefault(x => x.Id == id);
                this.data.Users.Remove(user);
                this.data.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
