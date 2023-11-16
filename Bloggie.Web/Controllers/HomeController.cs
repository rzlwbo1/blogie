using Bloggie.Web.Models;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories.BlogPostRepo;
using Bloggie.Web.Repositories.TagRepo;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostsRepository blogRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostsRepository blogRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
            this.tagRepository = tagRepository;
        }


        public async Task<IActionResult> Index()
        {
            // get all blogs
            var allBLogs = await blogRepository.GetAllAsync();

            // get all tags
            var allTags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = allBLogs,
                Tags = allTags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}