using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;


        public AdminBlogPostsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var tagsList = tags.ToList();

            Tag defaultTag = new Tag()
            {
                Name = "none",
                DisplayName = "Pilih Tags",
                Id = new Guid()
            };

            tagsList.Add(defaultTag);
            tagsList.Reverse();

             var blogReq = new AddBlogPostRequest()
            {
                Tags = tagsList.Select(x => new SelectListItem { Text = x.DisplayName, 
                    Value = x.Id.ToString(), 
                    Selected = x.DisplayName.Equals("Pilih Tags") })
            };

            return View(blogReq);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Add")]
        public async Task<IActionResult> AddPost(AddBlogPostRequest addBlogPost)
        {



            TempData["msg"] = "Succes to adding post";
            return RedirectToAction("Add");
        }
    }
}
