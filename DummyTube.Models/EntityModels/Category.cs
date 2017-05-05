using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.EntityModels
{
    public class Category
    {
        public Category()
        {
            this.Videos = new HashSet<Video>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}
