using CoreBlogApp.Entity;

namespace CoreBlogApp;

public interface IUserRepository
{
    IQueryable<User> Users { get; }
    void CreateUser(User user);
}
