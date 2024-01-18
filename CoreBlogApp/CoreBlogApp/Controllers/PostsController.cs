using System.Security.Claims;
using CoreBlogApp.Data;
using CoreBlogApp.Data.Abstract;
using CoreBlogApp.Entity;
using CoreBlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> Index(string url)
        {
            var claims = User.Claims;
            var posts = _postRepository.Posts.Where(x => x.IsActive == true);
            if (!string.IsNullOrEmpty(url))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.TagUrl == url));
            }

            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }
        public async Task<IActionResult> Details(string url)
        {
            return View(await _postRepository.Posts.Include(x => x.User).Include(x => x.Tags).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(p => p.PostUrl == url));
        }
        [HttpPost]
        public JsonResult AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);
            var entity = new Comment
            {
                CommentText = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(entity);

            return Json(new
            {
                Text,
                entity.PublishedOn,
                username,
                avatar
            });
        }
        [Authorize]
        public IActionResult Create()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _postRepository.CreatePost(
                    new Post
                    {
                        Title = model.Title,
                        Content = model.Content,
                        PostUrl = model.Url,
                        UserId = int.Parse(userId ?? ""),
                        Image = "2.jpg",
                        IsActive = false
                    });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);
            var posts = _postRepository.Posts;
            if (string.IsNullOrEmpty(role))
            {
                posts = posts.Where(x => x.UserId == userId);
            }
            return View(await posts.ToListAsync());
        }
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _postRepository.Posts.Include(x => x.Tags).FirstOrDefault(x => x.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewBag.Tags = _tagRepository.Tags.ToList();
            return View(new PostCreateViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.PostUrl,
                IsActive = post.IsActive,
                Tags = post.Tags
            });
        }
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model, int[] tagIds)
        {
            if (ModelState.IsValid)
            {
                var entityUpdate = new Post
                {
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    PostUrl = model.Url,
                    IsActive = model.IsActive
                };
                if (User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entityUpdate.IsActive = true;
                }
                _postRepository.EditPost(entityUpdate, tagIds);
                return RedirectToAction("List");
            }
            ViewBag.Tags = _tagRepository.Tags.ToList();
            return View(model);
        }
    }
}
