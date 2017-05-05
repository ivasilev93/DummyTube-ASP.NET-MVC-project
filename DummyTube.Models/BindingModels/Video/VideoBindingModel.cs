using DummyTube.Models.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyTube.Models.BindingModels.Video
{
    public class VideoBindingModel
    {
        [Required(ErrorMessage = "Title is required"),
         MinLength(2, ErrorMessage = "Title min 2 letters length")]
        public string Title { get; set; }

        [ImageUrl(ErrorMessage = "Please enter a valid image url")]
        [Display(Name = "Video Image(Thumbnail) Url")]
        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }

        [Required, StringLength(11, ErrorMessage = "Valid YoutubeId is required", MinimumLength = 11)]
        public string YoutubeId { get; set; }

        [Required(ErrorMessage = "Description is required"), MaxLength(250, ErrorMessage = "Description max length 250 letters")]
        public string Description { get; set; }
    }
}
