using System.ComponentModel.DataAnnotations;

namespace ThirdWay.Web.Models
{
    public class FeedViewModel
    {
        [Required(ErrorMessage = "URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        [Display(Name = "RSS/Atom feed URL")]
        public string? NewUrl { get; set; }

        public List<Data.Model.Feed>? Feeds { get; set; }
    }
}
