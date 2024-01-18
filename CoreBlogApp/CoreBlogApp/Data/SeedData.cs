using CoreBlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoreBlogApp.Data
{
    public class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { TagText = "Web Programlama", TagUrl = "web-programlama", TagColor = TagColors.warning },
                        new Tag { TagText = "Backend", TagUrl = "backend", TagColor = TagColors.info },
                        new Tag { TagText = "Frontend", TagUrl = "frontend", TagColor = TagColors.success },
                        new Tag { TagText = "Fullstack", TagUrl = "fullstack", TagColor = TagColors.secondary },
                        new Tag { TagText = "PHP", TagUrl = "php", TagColor = TagColors.primary }
                        );
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "merttasar", Name = "Mert Taşar", Email = "info@merttasar.com", Password = "123456", Image = "p1.jpg" },
                        new User { UserName = "example", Name = "Example People", Email = "info@example.com", Password = "123456", Image = "p2.jpg" }
                        );
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post { Title = "Asp.net core", Description = "Asp.net core dersleri.", Content = "Asp.net core dersleri", PostUrl = "aspnet-core", Image = "1.jpg", IsActive = true, PublishedOn = DateTime.Now.AddDays(-10), Tags = context.Tags.Take(3).ToList(), UserId = 1, Comments = new List<Comment> { new Comment { CommentText = "Iyi bir kurs", PublishedOn = new DateTime(), UserId = 1, }, new Comment { CommentText = "Çok faydalandığım bir kurs", PublishedOn = new DateTime().AddHours(10), UserId = 2 } } },
                        new Post { Title = "PHP", Description = "PHP dersleri", Content = "PHP dersleri", PostUrl = "php", Image = "2.jpg", IsActive = true, PublishedOn = DateTime.Now.AddDays(-20), Tags = context.Tags.Take(2).ToList(), UserId = 1 },
                        new Post { Title = "Django", Description = "Django dersleri", Content = "Django dersleri", PostUrl = "django", Image = "3.jpg", IsActive = true, PublishedOn = DateTime.Now.AddDays(-5), Tags = context.Tags.Take(4).ToList(), UserId = 2 },
                        new Post { Title = "React", Description = "React dersleri", Content = "React dersleri", PostUrl = "react", Image = "3.jpg", IsActive = true, PublishedOn = DateTime.Now.AddDays(-30), Tags = context.Tags.Take(4).ToList(), UserId = 2 },
                        new Post { Title = "Angular", Description = "Angular dersleri", Content = "Angular dersleri", PostUrl = "angular", Image = "3.jpg", IsActive = true, PublishedOn = DateTime.Now.AddDays(-40), Tags = context.Tags.Take(4).ToList(), UserId = 2 },
                        new Post { Title = "Web Tasarım", Description = "Web tasarım dersleri", Content = "Web tasarım dersleri", PostUrl = "web-tasarim", Image = "3.jpg", IsActive = true, PublishedOn = DateTime.Now.AddDays(-45), Tags = context.Tags.Take(4).ToList(), UserId = 2 }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
