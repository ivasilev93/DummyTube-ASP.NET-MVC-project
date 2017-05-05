using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DummyTube.Models.EntityModels
{
    public class Video
    {
        public Video()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required, MinLength(2)]
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string YoutubeId { get; set; }

        [Required, MaxLength(250)]
        public string Description { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Category Category { get; set; }
    }
}
