using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.BlogPostRepo;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostsRepository blogRepository;

        public BlogsController(IBlogPostsRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }


        [HttpGet, Route("blog/details/{urlHandle}")]
        public async Task<IActionResult> Index(string urlHandle)
        {
           
            if (string.IsNullOrWhiteSpace(urlHandle))
            {
                return NotFound();
            }

            var blog = await blogRepository.GetByUrlHandleAsync(urlHandle);
            return View(blog);
        }
    }
}
