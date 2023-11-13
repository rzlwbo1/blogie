using Bloggie.Web.Models;
using Bloggie.Web.Repositories.BlogPostRepo;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostsRepository blogRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostsRepository blogRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
        }


        public async Task<IActionResult> Index()
        {
            var allBLogs = await blogRepository.GetAllAsync();

            return View(allBLogs);
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