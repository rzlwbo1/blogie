using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories.BlogPostRepo;
using Bloggie.Web.Repositories.CommentRepo;
using Bloggie.Web.Repositories.LikesRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostsRepository blogRepository;
        private readonly IBlogPostLikeRepository likePostRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogCommentRepository blogComment;

        public BlogsController(IBlogPostsRepository blogRepository, 
            IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IBlogCommentRepository blogComment)
        {
            this.blogRepository = blogRepository;
            this.likePostRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogComment = blogComment;
        }


        [HttpGet, Route("blog/details/{urlHandle}")]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            int totalLikes = 0;
            BlogDetailsViewModel model = new BlogDetailsViewModel();

            if (string.IsNullOrWhiteSpace(urlHandle))
            {
                return NotFound();
            }

            var blog = await blogRepository.GetByUrlHandleAsync(urlHandle);

            if (blog != null)
            {
                totalLikes = await likePostRepository.GetTotalLikesAsync(blog.Id);

                if (signInManager.IsSignedIn(User))
                {
                    // get like for this blog for this user
                    IEnumerable<BlogPostsLike> likesByBlog = await likePostRepository.GetLikesByBlog(blog.Id);

                    var userId = userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var likeFromUser = likesByBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));

                        // kalo likeFromUser is null maka liked nya false, berarti blom like
                        liked = likeFromUser != null; 
                    }

                }

                model = new BlogDetailsViewModel
                {
                    Id = blog.Id,
                    PageTitle = blog.PageTitle,
                    Heading = blog.Heading,
                    Content = blog.Content,
                    ShortDescription = blog.ShortDescription,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    UrlHandle = blog.UrlHandle,
                    PublishDate = blog.PublishDate,
                    Author = blog.Author,
                    Visible = blog.Visible,
                    Tags = blog.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                };
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(BlogDetailsViewModel blogDetails)
        {
            if (signInManager.IsSignedIn(User))
            {
                var newComment = new BlogPostsComment
                {
                    BlogPostsId = blogDetails.Id,
                    Description = blogDetails.CommentDesc,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };

                await blogComment.AddCommentAsync(newComment);
                return RedirectToAction("Index", "Blogs", new { urlHandle = blogDetails.UrlHandle});
            }

            return Forbid();
        }
    }
}
