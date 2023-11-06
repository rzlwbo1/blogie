using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        } 


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Add")]
        public IActionResult SubmitTag(AddTagRequest addTagRequest)
        {
            // Manually get request from form
            //var name = Request.Form["name"]; // name = value dalam attribue name html
            //var displayName = Request.Form["displayName"];


            // model binding pake request di parameter
            //string name = addTagRequest.Name;
            //string display = addTagRequest.DisplayName;

            Tag newTag = new()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            bloggieDbContext.Tags.Add(newTag);
            bloggieDbContext.SaveChanges();

            return RedirectToAction("Add");
        }


        [HttpGet]
        public IActionResult List()
        {
            // read db conttext
            var tags = bloggieDbContext.Tags.ToList();

            return View(tags);
        }
    }
}
