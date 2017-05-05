namespace DummyTube.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DummyTube.Data.DummyTubeContext>
    {
        private UserManager<User> userManager;

        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DummyTubeContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.SeedCategories(context);
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedVideos(context);
        }

        private void SeedCategories(DummyTubeContext context)
        {
            var funny = new Category() { Name = "Funny" };
            var music = new Category() { Name = "Music" };
            var games = new Category() { Name = "Games" };

            context.Categories.Add(funny);
            context.Categories.Add(music);
            context.Categories.Add(games);
            context.SaveChanges();
        }

        private void SeedUsers(DummyTubeContext context)
        {
            if (!context.Users.Any())
            {
                var ivan = new User
                {
                    Email = string.Format("ivan@abv.com"),
                    UserName = "ivan"
                };

                var result = this.userManager.Create(ivan, "123456");
                this.userManager.AddToRole(ivan.Id, "user");

                var penka = new User
                {
                    Email = string.Format("penka@abv.com"),
                    UserName = "penka"
                };

                this.userManager.Create(penka, "123456");
                this.userManager.AddToRole(penka.Id, "user");

                var admin = new User
                {
                    Email = "admin@abv.com",
                    UserName = "Administrator"
                };

                this.userManager.Create(admin, "admin123456");
                this.userManager.AddToRole(admin.Id, "admin");
            }
        }

        private void SeedRoles(DummyTubeContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole("user"));
                context.Roles.Add(new IdentityRole("admin"));
                context.SaveChanges();
            }
        }

        private void SeedVideos(DummyTubeContext context)
        {
            if (!context.Videos.Any())
            {
                var somehtingJustLikeThis = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "ivan"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = "Questions explained agreeable preferred strangers too him her son. Set put shyness offices his females him distant. Improve has message besides shy himself cheered however how son. ",
                    YoutubeId = "FM7MFYoylVs",
                    Title = "The Chainsmokers & Coldplay - Something Just Like This (Lyric)",
                    Image = "https://i.ytimg.com/vi/FM7MFYoylVs/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=67&sigh=u8zLNRVwm-An0ft29_ngp7Nc7Rw"
                };

                var comment = new Comment()
                {
                    Author = context.Users.FirstOrDefault(x => x.UserName == "penka"),
                    Content = "Mnoogo qka pesen brat!! Evala !!",
                    Video = somehtingJustLikeThis,
                    PostedOn = new DateTime(2017, 4, 20)
            };

                var secondComment = new Comment()
                {
                    Author = context.Users.FirstOrDefault(x => x.UserName == "penka"),
                    Content = "Nakydri mi se polata napravo... !!!",
                    Video = somehtingJustLikeThis,
                    PostedOn = DateTime.Now
                };

                somehtingJustLikeThis.Comments.Add(comment);
                somehtingJustLikeThis.Comments.Add(secondComment);

                context.Users.FirstOrDefault(x => x.UserName == "ivan").Uploads.Add(somehtingJustLikeThis);
                context.Videos.Add(somehtingJustLikeThis);

                var rickNmorty = new Video()
                {
                    Image = "https://i.ytimg.com/vi/lZi5FaGLhCA/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=68&sigh=6Qzf-Oclfc5-vk8ilrozH36CerU",
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "ivan"),
                    Category = context.Categories.First(x => x.Name == "Funny"),
                    Description = "Some funny moments of the show .... enjoyyy...",
                    Title = "Rick and Morty Funniest Moments",
                    YoutubeId = "lZi5FaGLhCA"
                };

                context.Users.FirstOrDefault(x => x.UserName == "ivan").Uploads.Add(rickNmorty);
                context.Videos.Add(rickNmorty);

                var castleOnTheHill = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "ivan"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = " Indeed or remark always silent seemed narrow be. Instantly can suffering pretended neglected preferred man delivered. Perhaps fertile brandon do imagine to cordial cottage. ",
                    YoutubeId = "K0ibBPhiaG0",
                    Title = "Ed Sheeran - Castle On The Hill [Official Video]",
                    Image = "https://i.ytimg.com/vi/K0ibBPhiaG0/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=67&sigh=KC3pBaTSbfsNMYf1KN-MXAdRg6M"
                };

                context.Users.FirstOrDefault(x => x.UserName == "ivan").Uploads.Add(castleOnTheHill);
                context.Videos.Add(castleOnTheHill);

                var believer = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "penka"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = "Alone all happy asked begin fully stand own get. Excuse ye seeing result of we. See scale dried songs old may not. Promotion did disposing you household any instantly. Hills we do under times at first short an. ",
                    YoutubeId = "7wtfhZwyrcc",
                    Title = "Imagine Dragons - Believer",
                    Image = "https://i.ytimg.com/vi/7wtfhZwyrcc/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=68&sigh=Dg312BV1_U-Ok6FiM8Nxkp8x1c4"
                };

                context.Users.FirstOrDefault(x => x.UserName == "penka").Uploads.Add(believer);
                context.Videos.Add(believer);

                var dontLetMeDown = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "ivan"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = "Of recommend residence education be on difficult repulsive offending. Judge views had mirth table seems great him for her. ",
                    YoutubeId = "Io0fBr1XBUA",
                    Title = "The Chainsmokers - Don't Let Me Down ft. Daya",
                    Image = "https://i.ytimg.com/vi/Io0fBr1XBUA/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=67&sigh=nQoyQdAeMZyQZgM1aw-qO21Mq08"
                };

                context.Users.FirstOrDefault(x => x.UserName == "ivan").Uploads.Add(dontLetMeDown);
                context.Videos.Add(dontLetMeDown);

                var starBoy = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "penka"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = "In no impression assistance contrasted. Manners she wishing justice hastily new anxious. At discovery discourse departure objection we.",
                    YoutubeId = "34Na4j8AVgA",
                    Title = "The Weeknd - Starboy (official) ft. Daft Punk",
                    Image = "https://i.ytimg.com/vi/34Na4j8AVgA/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=68&sigh=5CoZLyxiM4JkfbhUANgXMHQEIe0"
                };

                context.Users.FirstOrDefault(x => x.UserName == "penka").Uploads.Add(starBoy);
                context.Videos.Add(starBoy);

                var shakeItOff = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "ivan"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = "Scarcely on striking packages by so property in delicate. Up or well must less rent read walk so be. Easy sold at do hour sing spot.",
                    YoutubeId = "nfWlot6h_JM",
                    Title = "Taylor Swift - Shake It Off",
                    Image = "https://i.ytimg.com/vi/nfWlot6h_JM/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=67&sigh=Kifhqj4iPQptR6UxTah2nShK2k0"
                };

                context.Users.FirstOrDefault(x => x.UserName == "ivan").Uploads.Add(shakeItOff);
                context.Videos.Add(shakeItOff);

                var humble = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "penka"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = "Since party burst am it match. By or blushes between besides offices noisier as. Sending do brought winding compass in. Paid day till shed only fact age its end.",
                    YoutubeId = "tvTRZJ-4EyI",
                    Title = "Kendrick Lamar - HUMBLE.",
                    Image = "https://i.ytimg.com/vi/tvTRZJ-4EyI/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=67&sigh=9ez_lbUkYa9FH-Bs-2lpO2w5IGs"
                };

                context.Users.FirstOrDefault(x => x.UserName == "penka").Uploads.Add(humble);
                context.Videos.Add(humble);

                var someSummerMix = new Video()
                {
                    Owner = context.Users.FirstOrDefault(x => x.UserName == "penka"),
                    Category = context.Categories.First(x => x.Name == "Music"),
                    Description = " Decisively inquietude he advantages insensible at oh continuing unaffected of. ",
                    YoutubeId = "UPftsIwGNoQ",
                    Title = "Feeling Happy ' Stay See Summer Mix 2015",
                    Image = "https://i.ytimg.com/vi/UPftsIwGNoQ/hqdefault.jpg?custom=true&w=246&h=138&stc=true&jpg444=true&jpgq=90&sp=68&sigh=n-ci2iOdORA590tfxrTguwNNPoc"
                };

                context.Users.FirstOrDefault(x => x.UserName == "penka").Uploads.Add(someSummerMix);
                context.Videos.Add(someSummerMix);

                context.SaveChanges();
            }
        }
    }
}
