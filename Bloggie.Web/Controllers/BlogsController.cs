using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories.BlogPostRepo;
using Bloggie.Web.Repositories.LikesRepo;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostsRepository blogRepository;
        private readonly IBlogPostLikeRepository likePostRepository;

        public BlogsController(IBlogPostsRepository blogRepository, 
            IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogRepository = blogRepository;
            this.likePostRepository = blogPostLikeRepository;
        }


        [HttpGet, Route("blog/details/{urlHandle}")]
        public async Task<IActionResult> Index(string urlHandle)
        {

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
                };
            }

            return View(model);
        }
    }
}
