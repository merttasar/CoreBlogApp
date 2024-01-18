using CoreBlogApp.Entity;

namespace CoreBlogApp.Data.Abstract
{
    public interface ITagRepository
    {
        IQueryable<Tag> Tags { get; }
        void CreateTag(Tag tag);
    }
}
