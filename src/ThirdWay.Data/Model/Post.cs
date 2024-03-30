using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThirdWay.Data.Model
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1024)]
        public string Uri { get; set; } = null!;

        [Required]
        [StringLength(1024)]
        public string UriHash { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public DateTime PublishDateTime { get; set; }

        [Required]
        [ForeignKey("Feed")]
        public int FeedId { get; set; }
        public virtual Feed Feed { get; set; } = null!;

        [Required]
        [StringLength(64)]
        public string Title { get; set; } = null!;

        [StringLength(255)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        [StringLength(64)]
        public string Author { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public bool IsFavorite { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

    }
}
