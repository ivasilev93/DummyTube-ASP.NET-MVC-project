using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.BindingModels.Comment
{
    public class DeleteCommentBindingModel
    {
        public int Id { get; set; }

        public string VideoYoutubeId { get; set; }
    }
}
