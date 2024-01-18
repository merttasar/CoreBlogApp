using CoreBlogApp.Data.Abstract;
using CoreBlogApp.Entity;

namespace CoreBlogApp.Data.Concrete
{
    public class EfCommentRepository : ICommentRepository
    {
        private BlogContext _blogContext;
        public EfCommentRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public IQueryable<Comment> Comments => _blogContext.Comments;

        public void CreateComment(Comment comment)
        {
            _blogContext.Comments.Add(comment);
            _blogContext.SaveChanges();
        }
    }
}
