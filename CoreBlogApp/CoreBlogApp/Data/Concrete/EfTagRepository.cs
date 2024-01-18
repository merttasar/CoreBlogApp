using CoreBlogApp.Data;
using CoreBlogApp.Data.Abstract;
using CoreBlogApp.Entity;

namespace CoreBlogApp;

public class EfTagRepository : ITagRepository
{
     private BlogContext _blogContext;
        public EfTagRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public IQueryable<Tag> Tags => _blogContext.Tags;

        public void CreateTag(Tag tag)
        {
            _blogContext.Tags.Add(tag);
            _blogContext.SaveChanges();
        }

}
