using CoreBlogApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace CoreBlogApp.Models
{
    public class PostCreateViewModel
    {
        public int PostId { get; set; }
        [Required]
        [Display(Name = "Postun Başlığı")]
        public string? Title { get; set; }
        [Required]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "İçerik")]
        public string? Content { get; set; }
        [Required]
        [Display(Name = "Url")]
        public string? Url { get; set; }
        public bool IsActive { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
