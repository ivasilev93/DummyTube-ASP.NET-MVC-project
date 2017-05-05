using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DummyTube.Models.EntityModels
{
    public class Comment
    {
        public Comment()
        {
        }

        public int Id { get; set; }

        [Required, MinLength(2)]
        public string Content { get; set; }

        public virtual Video Video { get; set; }

        public virtual User Author { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
