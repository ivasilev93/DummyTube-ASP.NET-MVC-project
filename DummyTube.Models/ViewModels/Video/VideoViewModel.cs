using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DummyTube.Models.CustomValidationAttributes;

namespace DummyTube.Models.ViewModels.Video
{
    public class VideoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required"),
       MinLength(2, ErrorMessage = "Title min 2 letters length")]
        public string Title { get; set; }

        [ImageUrl(ErrorMessage = "Please enter a valid image url")]
        [Display(Name = "Video Image(Thumbnail) Url")]
        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Valid YoutubeId is required"), MinLength(11), MaxLength(11)]
        public string YoutubeId { get; set; }

        [Required(ErrorMessage = "Description is required"), MaxLength(250, ErrorMessage = "Description max length 250 letters")]
        public string Description { get; set; }

        public string Owner { get; set; }
    }
}
