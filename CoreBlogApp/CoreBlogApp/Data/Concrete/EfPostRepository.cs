using CoreBlogApp.Data.Abstract;
using CoreBlogApp.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CoreBlogApp.Data.Concrete
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _blogContext;
        public EfPostRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public IQueryable<Post> Posts => _blogContext.Posts;

        public void CreatePost(Post post)
        {
            _blogContext.Posts.Add(post);
            _blogContext.SaveChanges();
        }
        public void EditPost(Post post)
        {
            var entity = _blogContext.Posts.FirstOrDefault(p => p.PostId == post.PostId);
            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.PostUrl = post.PostUrl;
                entity.IsActive = post.IsActive;

                _blogContext.SaveChanges();
            }
        }
        public void EditPost(Post post, int[] tagIds)
        {
            var entity = _blogContext.Posts.Include(x => x.Tags).FirstOrDefault(p => p.PostId == post.PostId);
            if (entity != null)
            {
                entity.Title = post.Title;
                entity.Description = post.Description;
                entity.Content = post.Content;
                entity.PostUrl = post.PostUrl;
                entity.IsActive = post.IsActive;
                entity.Tags = _blogContext.Tags.Where(x => tagIds.Contains(x.TagId)).ToList();

                _blogContext.SaveChanges();
            }

        }
    }
}
