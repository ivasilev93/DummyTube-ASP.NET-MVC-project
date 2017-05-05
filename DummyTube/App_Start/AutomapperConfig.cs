using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DummyTube.Models.EntityModels;
using DummyTube.Models.ViewModels.Video;
using DummyTube.Models.ViewModels.Comment;
using DummyTube.Models.BindingModels.Video;
using DummyTube.Models.ViewModels.Account;

namespace DummyTube.App_Start
{
    public class AutomapperConfig
    {
        public static void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Video, VideoViewModel>()
                .ForMember(dto => dto.Owner,
                           x => x.MapFrom(v => v.Owner.UserName));

                cfg.CreateMap<Comment, CommentViewModel>()
                .ForMember(dto => dto.Author,
                            x => x.MapFrom(v => v.Author.UserName))
                .ForMember(dto => dto.VideoYoutubeId,
                            x => x.MapFrom(v => v.Video.YoutubeId));

                cfg.CreateMap<Video, VideoDetailsViewModel>()
                .ForMember(dto => dto.Owner,
                           x => x.MapFrom(v => v.Owner.UserName))
                .ForMember(dto => dto.Comments,
                            x => x.MapFrom(v => v.Comments));

                cfg.CreateMap<VideoBindingModel, Video>();

                cfg.CreateMap<VideoBindingModel, VideoViewModel>();

                cfg.CreateMap<User, UserInfoViewModel>()
                .ForMember(dto => dto.Name,
                    x => x.MapFrom(v => v.UserName))
                .ForMember(dto => dto.VideoCount,
                    x => x.MapFrom(v => v.Uploads.Count));

                cfg.CreateMap<User, ShowProfileViewModel>()
                 .ForMember(dto => dto.User,
                            x => x.MapFrom(u => u.UserName))
                 .ForMember(dto => dto.Videos,
                            x => x.MapFrom(v => v.Uploads));
            });
        }
    }
}