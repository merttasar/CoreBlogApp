using System.ComponentModel.DataAnnotations;

namespace CoreBlogApp.Entity
{
    public enum TagColors{
        primary, danger, warning, success, secondary, info
    }
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string? TagText { get; set; }
        public string? TagUrl { get; set; }
        public TagColors? TagColor { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
