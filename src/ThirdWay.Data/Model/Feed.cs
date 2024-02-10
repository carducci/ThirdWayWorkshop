using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThirdWay.Data.Model
{

    [Table("Feed")]
    public class Feed
    {

        [Key]
        public int Id { get; set; }

        public DateTime LastUpdated { get; set; }

        [Required]
        [StringLength(512)]
        public string Uri { get; set; }

        [Required]
        [StringLength(1024)]
        public string Url { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; } = string.Empty;

        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [StringLength(255)]
        public string Author { get; set; } = string.Empty;

    }
}
