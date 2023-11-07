using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPosts : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
