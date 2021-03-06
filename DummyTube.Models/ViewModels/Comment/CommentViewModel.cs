﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string VideoYoutubeId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
