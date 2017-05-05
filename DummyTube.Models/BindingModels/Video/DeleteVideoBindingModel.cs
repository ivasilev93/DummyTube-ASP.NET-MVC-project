using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.BindingModels.Video
{
    public class DeleteVideoBindingModel
    {
        public int Id { get; set; }

        public string Owner { get; set; }
    }
}
