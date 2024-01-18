using CoreBlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoreBlogApp.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
