using CoreBlogApp.Entity;

namespace CoreBlogApp.Data.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);

    }
}
